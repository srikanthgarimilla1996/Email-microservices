﻿using AutoMapper;
using Email.Services.API.Models;
using Email.Services.API.Models.Dto;

namespace Email.Services.API
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<UserDto, User>();
                config.CreateMap<User, UserDto>();
            });
            return mappingConfig;
        }
    }
}
