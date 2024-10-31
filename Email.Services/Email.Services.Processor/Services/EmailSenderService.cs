
using System.Security.Cryptography;
using System.Text;
using Email.Services.Processor.Models;
using Email.Services.Processor.Models.Dto;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Email.Services.Processor.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly OutlookDetails _outlookDetails;
        public EmailSenderService(IOptions<OutlookDetails> options)
        {
            _outlookDetails = options.Value;
        }
        public async void SendEmail(string message, List<UserDto> UsersList)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_outlookDetails.UserName),
                Subject = "Testing rabbitmq message queue - please ignore"
            };

            email.To.AddRange(UsersList.Where(us => !string.IsNullOrEmpty(us.Email))
                .Select(us => MailboxAddress.Parse(us.Email)));

            var builder = new BodyBuilder
            {
                HtmlBody = $"This is a test email sent by email service. Please ignore this {message}"
            };

            email.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_outlookDetails.Host, _outlookDetails.Port, MailKit.Security.SecureSocketOptions.StartTls);
            var decryptedPassword = Decrypt(_outlookDetails.Password, _outlookDetails.EncString);
            smtp.Authenticate(_outlookDetails.UserName, decryptedPassword);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public static string Decrypt(string input, string key)
        {
            byte[] array = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider obj = new TripleDESCryptoServiceProvider
            {
                Key = Encoding.UTF8.GetBytes(GenEncStr(key)),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            byte[] bytes = obj.CreateDecryptor().TransformFinalBlock(array, 0, array.Length);
            obj.Clear();
            return Encoding.UTF8.GetString(bytes);
        }

        public static string GenEncStr(string encstr)
        {
            byte[] bytes = Convert.FromBase64String(encstr);
            char[] array = CleanUpEncStr(Encoding.UTF8.GetString(bytes)).ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }

        public static string CleanUpEncStr(string encstr)
        {
            for (int i = 1; i < encstr.Length; i++)
            {
                encstr = encstr.Remove(i, 1);
            }

            return encstr;
        }
    }
}
