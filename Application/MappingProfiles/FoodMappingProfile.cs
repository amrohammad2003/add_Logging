
using AutoMapper;
using Contract.Dtos.FoodDtos;
using Domain.Entities;

namespace Application.MappingProfiles
{
    public class FoodMappingProfile : Profile
    {
        public FoodMappingProfile()
        {
            CreateMap<Food, FoodDto>().ReverseMap();
            CreateMap<CreateFoodDto, Food>();
            CreateMap<UpdateFoodDto, Food>();
        }
    }
}
