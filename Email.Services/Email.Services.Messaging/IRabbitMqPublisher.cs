using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Services.Messaging
{
    public interface IRabbitMqPublisher
    {
        void PublishMessage(string message);
    }
}
