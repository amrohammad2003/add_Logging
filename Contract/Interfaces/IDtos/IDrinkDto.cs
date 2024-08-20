namespace Contract.Interfaces.IDtos
{
    public interface IDrinkDto
    {
        string? Name { get; set; }
        decimal Price { get; set; }
        string? Size { get; set; }
    }
}
