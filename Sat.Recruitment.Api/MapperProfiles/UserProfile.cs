using AutoMapper;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Dto;
using Sat.Recruitment.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, UserDto>().ReverseMap();
        }
    }
}
