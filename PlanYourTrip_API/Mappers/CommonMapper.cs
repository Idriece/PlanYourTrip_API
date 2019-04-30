using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanYourTrip_API.Mappers
{
    public class CommonMapperProfile : Profile
    {
        public CommonMapperProfile()
        {
            //All Mappings Here

            //Mapping no need automatic mapper if all properties have same name
            this.CreateMap<PlanYourTripBusinessEntity.Models.Hotel, PlanYourTrip_API.Models.HotelDetail>()
                .ForMember(m => m.CityId, n => n.MapFrom(a => a.CityID));

        }
    }
}