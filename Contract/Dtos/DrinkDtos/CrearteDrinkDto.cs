using Contract.Interfaces.IDtos;

namespace Contract.Dtos.DrinkDtos
{
    public class CreateDrinkDto : IDrinkDto
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Size { get; set; }
    }
}
