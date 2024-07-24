using AutoMapper;
using BackEnd.DTOs;
using BackEnd.Models;

namespace BackEnd.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {
            CreateMap<Delivery, DeliveryDTO>().ReverseMap();
        }
    }
}
