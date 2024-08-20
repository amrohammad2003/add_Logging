using System;
using System.Collections.Generic;
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Contract.Dtos.DrinkDtos;
using Contract.Interfaces.IServices;

namespace WebResturantApplication.Controllers
{
    [ApiController]
    [Route("api/Drink")]
    public class DrinkController : ControllerBase
    {
        private readonly IDrinkService _drinkService;
        private readonly ILoggerService _logger;
        private readonly ILogger<DrinkController> _loggerDrink;



        public DrinkController(IDrinkService drinkService, ILoggerService logger, ILogger<DrinkController>
            loggerDrink)
        {
            _drinkService = drinkService;
            _logger = logger;
            _loggerDrink = loggerDrink;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                await _logger.LogInfo("Calling GetAsync for Drink");

                IEnumerable<DrinkDto> drinks = await _drinkService.GetAsync(); 
                return Ok(drinks);
            }
            catch (Exception ex)

            {
                await _logger.LogWarn("non succeed GetAsync calling for Drink");


                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                await _logger.LogInfo("Get  By this id ." + id);

                DrinkDto drink = await _drinkService.GetByIdAsync(id); 
                return Ok(drink);
            }
            catch (Exception ex)
            {

                //_logger.LogInfo(ex, ex.Message);
                await _logger.LogWarn("non succeed GetByIdAsync Calling");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDrinkDto creatDrinkDto)
        {
            try
            {
                await _logger.LogInfo("Create with this Dto ." + creatDrinkDto);


                DrinkDto drink = await _drinkService.CreateAsync(creatDrinkDto); 
                return Ok(drink);
            }
            catch (Exception ex)
            {
                await _logger.LogWarn("non succeed CreateAsync Calling  ");

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateDrinkDto updateDrinkDto
           )
        {
            try
            {

                await _logger.LogInfo( "Update with this Dto ." + updateDrinkDto);

                DrinkDto drink = await _drinkService.UpdateAsync(updateDrinkDto); 
                return Ok(drink);
            }
            catch (Exception ex)
            {

                await _logger.LogWarn("non succeed UpdateAsync Calling  ");

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _logger.LogInfo("Delete with this id ." + id);

                bool isDeleted = await _drinkService.DeleteByIdAsync(id); 
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
