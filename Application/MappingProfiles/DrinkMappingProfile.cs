using AutoMapper;
using Contract.Dtos.DrinkDtos;
using Domain.Entities;

namespace Application.MappingProfiles
{
    public class DrinkMappingProfile : Profile
    {
        public DrinkMappingProfile()
        {
            CreateMap<Drink, DrinkDto>().ReverseMap();
            CreateMap<CreateDrinkDto, Drink>();
            CreateMap<UpdateDrinkDto, Drink>();
        }
    }
}
