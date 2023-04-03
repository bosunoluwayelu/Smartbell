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
    public class RingtoneProfile : Profile
    {
        public RingtoneProfile()
        {
            CreateMap<CreateRingtoneDto, Ringtone>()
                .ForMember(destination => destination.Id, options => options.MapFrom(source => Guid.NewGuid()))
                .ForMember(destination => destination.CreatedDate, options => options.MapFrom(source => DateTime.Now))
                .ForMember(destination => destination.UpdatedDate, options => options.MapFrom(source => DateTime.Now));

            CreateMap<Ringtone, RingtoneResponseDto>();
            CreateMap<RingtoneResponseDto, Ringtone>();
        }
    }
}
