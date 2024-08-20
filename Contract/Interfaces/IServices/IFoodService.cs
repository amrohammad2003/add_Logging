using Contract.Dtos.FoodDtos;

public interface IFoodService
{
    Task<IEnumerable<FoodDto>> GetAsync();
    Task<FoodDto> GetByIdAsync(int id);
    Task<FoodDto> CreateAsync(CreateFoodDto createFoodDto);
    Task<FoodDto> UpdateAsync(UpdateFoodDto updateFoodDto);
    Task<bool> DeleteByIdAsync(int id);
}
