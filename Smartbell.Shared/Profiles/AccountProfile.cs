using AutoMapper;
using Smartbell.Shared.Dtos;
using Smartbell.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartbell.Shared.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<CreateAccountDto, ApplicationUser>();
            CreateMap<ApplicationUser, CreateAccountDto>();
            CreateMap<ApplicationUser, AccountResponseDto>();
            CreateMap<AccountResponseDto, ApplicationUser>();
        }
    }
}
