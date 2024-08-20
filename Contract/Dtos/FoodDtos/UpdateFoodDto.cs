using Contract.Interfaces.IDtos;

namespace Contract.Dtos.FoodDtos
{
    public class UpdateFoodDto: IFoodDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
    }
}
