using AutoMapper;
using Contract.Dtos.FoodDtos;
using Contract.Interfaces.IServices;
using Domain.Entities;
using Efcore.DBContext;
using Microsoft.EntityFrameworkCore; // Include this for EF Core async methods
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FoodService : IFoodService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly ILoggerService _loggerService;

        public FoodService(IMapper mapper, AppDbContext context, ILoggerService loggerService)
        {
            _mapper = mapper;
            _context = context;
            _loggerService = loggerService;
        }

        public async Task<IEnumerable<FoodDto>> GetAsync()
        {
            try
            {
                await _loggerService.LogInfo("get all foods.");
                IEnumerable<Food> foods = await _context.Foods.ToListAsync();
                return _mapper.Map<List<FoodDto>>(foods);
            }
            catch (Exception ex)
            {
                await _loggerService.LogErr(ex, "Error in getAll.");
                throw;
            }
        }

        public async Task<FoodDto> GetByIdAsync(int id)
        {
            try
            {
                await _loggerService.LogDbg(new KeyNotFoundException($" foods with Id {id}"), $"Retrieving food with Id {id}");
                Food food = await _context.Foods.FirstOrDefaultAsync(f => f.Id == id) ??
                            throw new Exception($"Food with Id {id} not found.");
                return _mapper.Map<FoodDto>(food);
            }
            catch (Exception ex)
            {
                await _loggerService.LogErr(ex, $"unvalid create with  Id {id}");
                throw;
            }
        }

        public async Task<FoodDto> CreateAsync(CreateFoodDto entity)
        {
            if (entity == null)
            {
                await _loggerService.LogWarn("CreateDrinkDto is null entity.");
                throw new Exception("Entity is null.");
            }

            if (entity.Price <= 0)
            {
                await _loggerService.LogWarn($"Create invalid price: {entity.Price}");
                throw new Exception("Price less than zero.");
            }

            try
            {
                await _loggerService.LogInfo("Creating a new food entity.");
                Food food = _mapper.Map<Food>(entity);
                await _context.Foods.AddAsync(food);
                await _context.SaveChangesAsync();
                return _mapper.Map<FoodDto>(food);
            }
            catch (Exception ex)
            {
                await _loggerService.LogErr(ex, "Error in creating a new food entity.");
                throw;
            }
        }

        public async Task<FoodDto> UpdateAsync(UpdateFoodDto entity)
        {
            if (!await IsExistsAsync(entity.Id))
            {
                await _loggerService.LogWarn($"Update Fail ,  {entity.Id} not found.");
                throw new Exception("Entity not found.");
            }

            try
            {
                await _loggerService.LogInfo($"Updating food entity with Id {entity.Id}.");
                Food foodToUpdate = await _context.Foods.FirstAsync(f => f.Id == entity.Id);
                foodToUpdate.Price = entity.Price;
                foodToUpdate.Name = entity.Name;

                _context.Foods.Update(foodToUpdate);
                await _context.SaveChangesAsync();
                return _mapper.Map<FoodDto>(foodToUpdate);
            }
            catch (Exception ex)
            {
                await _loggerService.LogErr(ex, $" update food entity with Id {entity.Id}.do not success");
                throw;
            }
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            if (!await IsExistsAsync(id))
            {
                await _loggerService.LogWarn($"Delete operation failed. Entity with Id {id} not found.");
                throw new Exception("Entity not found.");
            }

            try
            {
                await _loggerService.LogInfo($"Delet food entity with Id {id}.");
                Food food = await _context.Foods.FirstAsync(f => f.Id == id);
                _context.Foods.Remove(food);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _loggerService.LogErr(ex, $"Error in  deleting food entity with Id {id}.");
                throw;
            }
        }

        private async Task<bool> IsExistsAsync(int id)
        {
            await _loggerService.LogDbg(new KeyNotFoundException($"Check existence of food entity with Id {id}"), $"Checking existence of food entity with Id {id}.");
            return await _context.Foods.AnyAsync(f => f.Id == id);
        }
    }
}
