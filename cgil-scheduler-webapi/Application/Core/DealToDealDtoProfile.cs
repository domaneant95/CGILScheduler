using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using Domain;
using Domain.Dto;

namespace Application.Core
{
    public class DealToDealDtoProfile : Profile
    {
        public DealToDealDtoProfile()
        {
            var map = CreateMap<Deal, DealDto>();
            map.ForMember(dest => dest.text, act => act.MapFrom(src => src.Text));
            map.ForMember(dest => dest.description, act => act.MapFrom(src => src.Description));
            map.ForMember(dest => dest.reccurenceRule, act => act.MapFrom(src => src.ReccurenceRule));
            map.ForMember(dest => dest.recurrenceException, act => act.MapFrom(src => src.RecurrenceException));
            map.ForMember(dest => dest.startDate, act => act.MapFrom(src => src.DlStartDate));
            map.ForMember(dest => dest.endDate, act => act.MapFrom(src => src.DlEndDate));
        }
    }

    public class DealDtoToDealProfile : Profile
    {
        public DealDtoToDealProfile()
        {
            var map = CreateMap<DealDto, Deal>();
            map.ForMember(dest => dest.Text, act => act.MapFrom(src => src.text));
            map.ForMember(dest => dest.Description, act => act.MapFrom(src => src.description));
            map.ForMember(dest => dest.ReccurenceRule, act => act.MapFrom(src => src.reccurenceRule));
            map.ForMember(dest => dest.RecurrenceException, act => act.MapFrom(src => src.recurrenceException));
            map.ForMember(dest => dest.DlStartDate, act => act.MapFrom(src => src.startDate));
            map.ForMember(dest => dest.DlEndDate, act => act.MapFrom(src => src.endDate));
        }
    }
}