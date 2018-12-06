using AutoMapper;
using HandlingEnumsInApi.Data.Entities;
using HandlingEnumsInApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandlingEnumsInApi.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddressDto, Address>()
                .ForMember(o => o.AddressType, ex => ex.MapFrom(o => Enum.Parse(typeof(AddressType), o.AddressType))); //maps from string to enum

            CreateMap<Address, AddressResponseDto>();
        }
    }
}
