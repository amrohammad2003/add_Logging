using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Contract.Dtos.FoodDtos;
using Contract.Interfaces.IServices;
using Contract.Dtos.DrinkDtos;

namespace WebResturantApplication.Controllers
{
    [ApiController]
    [Route("api/Food")]
    public class FoodController : ControllerBase
    {
        private readonly IFoodService _foodService;
        private readonly ILoggerService _logger;

        public FoodController(IFoodService foodService, ILoggerService logger)
        {
            _foodService = foodService;
            _logger = logger;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                await _logger.LogInfo("Calling GetAsync for Food");

                IEnumerable<FoodDto> foods = await _foodService.GetAsync();
                return Ok(foods);
            }
            catch (Exception ex)
            {
                await _logger.LogWarn("non succeed GetAsync calling ");

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            try
            {
                await _logger.LogInfo("Get  By this id ." + id);

                FoodDto food = await _foodService.GetByIdAsync(id);
                return Ok(food);
            }
            catch (Exception ex)
            {
                await _logger.LogWarn("non succeed GetByIdAsync Calling  ");

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFoodDto createFoodDto)
        {
            try
            {
                await _logger.LogInfo("Create with this Dto ." + createFoodDto);

                FoodDto food = await _foodService.CreateAsync(createFoodDto);
                return Ok(food);
            }
            catch (Exception ex)
            {
                await _logger.LogWarn("non succeed CreateAsync Calling ");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateFoodDto updateFoodDto)
        {
            try
            {
                await _logger.LogInfo("Update with this Dto ." + updateFoodDto);

                FoodDto food = await _foodService.UpdateAsync(updateFoodDto);
                return Ok(food);
            }
            catch (Exception ex)
            {
                await _logger.LogWarn("non succeed UpdateAsync Calling  ");


                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _logger.LogInfo("Delete with this id ." + id);

                bool isDeleted = await _foodService.DeleteByIdAsync(id);
                return Ok(isDeleted);
            }
            catch (Exception ex)
            {
                await _logger.LogWarn("non succeed DeleteByIdAsync Calling  ");

                return BadRequest(ex.Message);
            }
        }
    }
}
