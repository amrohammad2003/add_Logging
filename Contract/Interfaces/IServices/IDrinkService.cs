using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.Dtos.DrinkDtos;

namespace Contract.Interfaces.IServices
{
    public interface IDrinkService
    {
        // Get
        Task<IEnumerable<DrinkDto>> GetAsync(); // Return Task<IEnumerable<DrinkDto>>
        Task<DrinkDto> GetByIdAsync(int id); // Return Task<DrinkDto>

        // Create
        Task<DrinkDto> CreateAsync(CreateDrinkDto entity); // Return Task<DrinkDto>

        // Update
        Task<DrinkDto> UpdateAsync(UpdateDrinkDto entity); // Return Task<DrinkDto>

        // Delete
        Task<bool> DeleteByIdAsync(int id); // Return Task<bool>
    }
}
