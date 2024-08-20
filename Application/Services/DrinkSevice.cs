using AutoMapper;
using Contract.Dtos.DrinkDtos;
using Contract.Interfaces.IServices;
using Domain.Entities;
using Efcore.DBContext;
using Microsoft.EntityFrameworkCore; // Include this for EF Core async methods

public class DrinkService : IDrinkService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;
    private readonly ILoggerService _loggerService;

    public DrinkService(IMapper mapper, AppDbContext context, ILoggerService loggerService)
    {
        _mapper = mapper;
        _context = context;
        _loggerService = loggerService;
    }

    public async Task<IEnumerable<DrinkDto>> GetAsync()
    {
        try
        {
            await _loggerService.LogInfo("get all drinks.");
            IEnumerable<Drink> drinks = await _context.Drinks.ToListAsync();
            return _mapper.Map<List<DrinkDto>>(drinks);
        }
        catch (Exception ex)
        {
            await _loggerService.LogErr(ex, "Error in getAll.");
            throw;
        }
    }

    public async Task<DrinkDto> GetByIdAsync(int id)
    {
        try
        {
            await _loggerService.LogDbg(new KeyNotFoundException($" drinks with Id {id}"), $"Retrieving drink with Id {id}");
            Drink drink = await _context.Drinks.FirstOrDefaultAsync(f => f.Id == id) ??
                          throw new Exception($"Drink with Id {id} not found.");
            return _mapper.Map<DrinkDto>(drink);
        }
        catch (Exception ex)
        {
            await _loggerService.LogErr(ex, $"unvalid create with  Id {id}");
            throw;
        }
    }

    public async Task<DrinkDto> CreateAsync(CreateDrinkDto entity)
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
            await _loggerService.LogInfo("Creating a new drink entity.");
            Drink drink = _mapper.Map<Drink>(entity);
            await _context.Drinks.AddAsync(drink);
            await _context.SaveChangesAsync();
            return _mapper.Map<DrinkDto>(drink);
        }
        catch (Exception ex)
        {
            await _loggerService.LogErr(ex, "Error in creating a new drink entity.");
            throw;
        }
    }

    public async Task<DrinkDto> UpdateAsync(UpdateDrinkDto entity)
    {
        if (!await IsExistsAsync(entity.Id))
        {
            await _loggerService.LogWarn($"Update Fail ,  {entity.Id} not found.");
            throw new Exception("Entity not found.");
        }

        try
        {
            await _loggerService.LogInfo($"Updating drink entity with Id {entity.Id}.");
            Drink drinkToUpdate = await _context.Drinks.FirstAsync(f => f.Id == entity.Id);
            drinkToUpdate.Price = entity.Price;
            drinkToUpdate.Name = entity.Name;

            _context.Drinks.Update(drinkToUpdate);
            await _context.SaveChangesAsync();
            return _mapper.Map<DrinkDto>(drinkToUpdate);
        }
        catch (Exception ex)
        {
            await _loggerService.LogErr(ex, $" update drink entity with Id {entity.Id}.do not success");
            throw;
        }
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        if (!await IsExistsAsync(id))
        {
            await _loggerService.LogWarn($" Entity with Id {id} not found.");
        }

        try
        {
            await _loggerService.LogInfo($"Delet drink entity with Id {id}.");
            Drink drink = await _context.Drinks.FirstAsync(f => f.Id == id);
            _context.Drinks.Remove(drink);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            await _loggerService.LogErr(ex, $"Error in  deleting drink entity with Id {id}.");
            throw;
        }
    }

    private async Task<bool> IsExistsAsync(int id)
    {
        await _loggerService.LogDbg(new KeyNotFoundException($"Check existence of drink entity with Id {id}"), $"Checking existence of drink entity with Id {id}.");
        return await _context.Drinks.AnyAsync(f => f.Id == id);
    }
}
