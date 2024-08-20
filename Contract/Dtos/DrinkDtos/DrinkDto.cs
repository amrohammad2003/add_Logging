using Contract.Interfaces.IDtos;

namespace Contract.Dtos.DrinkDtos
{
    public class DrinkDto : IDrinkDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Size { get; set; }

    }
}

