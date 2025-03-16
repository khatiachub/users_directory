using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using users_directory.DTO;
using users_directory.Models;
using users_directory.Services;

namespace users_directory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {       
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _unitOfWork.Users.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            var user = new User
            {
                Id = userDto.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Gender = (Gender)userDto.Gender, // Convert int to enum
                PersonalNumber = userDto.PersonalNumber,
                BirthDate = userDto.BirthDate,
                CityId = userDto.CityId,
                PicturePath = userDto.PicturePath,
                PhoneNumbers = userDto.PhoneNumbers.Select(p => new PhoneNumber
                {
                    Type = p.Type,
                    Number = p.Number
                }).ToList(),
                Relationships = userDto.Relationships.Select(r => new PersonRelationship
                {
                    Type = r.Type,
                    RelatedPerson = r.RelatedPerson
                }).ToList()
            };
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            var existingUser = await _unitOfWork.Users.GetByIdAsync(id);
            if (existingUser == null) return NotFound();

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            //existingUser.Email = user.Email;
            //existingUser.PhoneNumber = user.PhoneNumber;

            _unitOfWork.Users.Update(existingUser);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null) return NotFound();

            _unitOfWork.Users.Delete(user);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

    }
}
