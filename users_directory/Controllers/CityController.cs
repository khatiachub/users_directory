using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using users_directory.DTO;
using users_directory.Models;
using users_directory.Services;

namespace users_directory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("reteiviCities")]
        public async Task<ActionResult<IEnumerable<City>>> GetAllCities()
        {
            try
            {
                var cities = await _unitOfWork.City.GetAllAsync();
                if (cities == null || !cities.Any())
                    return NotFound(new { message = "No users found." });

                var cityDtos = cities.Select(city => new City
                {
                    Id = city.Id,
                    CityName = city.CityName

                });
                return Ok(cityDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching users.", error = ex.Message });
            }
        }
        [HttpPost("addCity")]
        public async Task<IActionResult> CreateCity([FromBody] CityDto cityDto)
        {
            if (cityDto == null)
                return BadRequest(new { message = "User data cannot be null." });

            if (string.IsNullOrWhiteSpace(cityDto.CityName))
                return BadRequest(new { message = "city name are required." });


            try
            {
                var city = new City
                {
                    CityName = cityDto.CityName
                };

                await _unitOfWork.City.AddAsync(city);
                await _unitOfWork.CompleteAsync();

                return Ok(new { city });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the city.", error = ex.Message });
            }
        }


    }
}
