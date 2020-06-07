using AutoMapper;
using NextJsWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NextJsWebAPI.Models
{
    public class MappingProfile : Profile
    { 
        public MappingProfile()
        {
            CreateMap<User, AccountModel>();
            CreateMap<AccountModel, User>();
        }
    }
}
