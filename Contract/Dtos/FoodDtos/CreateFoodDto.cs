using Contract.Interfaces.IDtos;

namespace Contract.Dtos.FoodDtos
{
    public class CreateFoodDto : IFoodDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
