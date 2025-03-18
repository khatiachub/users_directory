using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using users_directory.DTO;
using users_directory.Models;
using users_directory.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            try
            {
                var users = await _unitOfWork.Users.GetAllAsync();
                if (users == null || !users.Any())
                    return NotFound(new { message = "No users found." });

                var userDtos = users.Select(user => new UserGetDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    PersonalNumber = user.PersonalNumber,
                    BirthDate = user.BirthDate,
                    City = user.City.CityName,
                    ProfileImage = user.ProfileImage.ToString(),
                    PhoneNumbers = user.PhoneNumbers.Select(p => new PhoneNumberDto
                    {
                        Type = p.Type,
                        Number = p.Number
                    }).ToList(),
                    Relationships = user.Relationships.Select(p => new PersonRelationshipDto
                    {
                        Type = p.Type,
                        RelatedPerson = p.RelatedPerson
                    }).ToList()
                }).ToList();

                return Ok(userDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching users.", error = ex.Message });
            } 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid user ID." });

            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(id);
                if (user == null)
                    return NotFound(new { message = $"User with ID {id} not found." });

                var userDto = new UserGetDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    PersonalNumber = user.PersonalNumber,
                    BirthDate = user.BirthDate,
                    City = user.City.CityName,
                    ProfileImage = user.ProfileImage.ToString(),
                    PhoneNumbers = user.PhoneNumbers.Select(p => new PhoneNumberDto
                    {
                        Type = p.Type,
                        Number = p.Number
                    }).ToList(),
                    Relationships = user.Relationships.Select(p => new PersonRelationshipDto
                    {
                        Type = p.Type,
                        RelatedPerson = p.RelatedPerson
                    }).ToList()
                };

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the user.", error = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromForm] UserDto userDto)
        {
            if (userDto == null)
                return BadRequest(new { message = "User data cannot be null." });

            if (string.IsNullOrWhiteSpace(userDto.FirstName) || string.IsNullOrWhiteSpace(userDto.LastName))
                return BadRequest(new { message = "First name and last name are required." });

            var Image = await _unitOfWork.Users.UploadProfileImage(userDto.ProfileImage);

            try
            {
                var user = new User
                {
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Gender = userDto.Gender,
                    PersonalNumber = userDto.PersonalNumber,
                    BirthDate = userDto.BirthDate,
                    CityId = userDto.CityId,
                    ProfileImage =Image.ToString(),
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
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the user.", error = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto userDto)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid user ID." });

            if (userDto == null)
                return BadRequest(new { message = "User data cannot be null." });

            try
            {
                var existingUser = await _unitOfWork.Users.GetByIdAsync(id);
                if (existingUser == null)
                    return NotFound(new { message = $"User with ID {id} not found." });

                existingUser.FirstName = userDto.FirstName;
                existingUser.LastName = userDto.LastName;
                existingUser.Gender = userDto.Gender;
                existingUser.PersonalNumber = userDto.PersonalNumber;
                existingUser.BirthDate = userDto.BirthDate;
                existingUser.CityId = userDto.CityId;
                existingUser.ProfileImage = userDto.ProfileImage.ToString();
                existingUser.PhoneNumbers = userDto.PhoneNumbers.Select(p => new PhoneNumber
                {
                    Type = p.Type,
                    Number = p.Number
                }).ToList();
                existingUser.Relationships = userDto.Relationships.Select(r => new PersonRelationship
                {
                    Type = r.Type,
                    RelatedPerson = r.RelatedPerson
                }).ToList();

                _unitOfWork.Users.Update(existingUser);
                await _unitOfWork.CompleteAsync();

                return Ok(new { message = "User updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the user.", error = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid user ID." });

            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(id);
                if (user == null)
                    return NotFound(new { message = $"User with ID {id} not found." });

                _unitOfWork.Users.Delete(user);
                await _unitOfWork.CompleteAsync();

                return Ok(new { message = "User deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the user.", error = ex.Message });
            }
        }

        [HttpGet("searchByPersonalData/{firstname}/{lastname}/{personalnumber}")]
        public async Task<IActionResult> GetByPersonalNum(string firstname, string lastname, string personalnumber)
        {
            if (string.IsNullOrWhiteSpace(firstname)|| string.IsNullOrWhiteSpace(lastname)|| string.IsNullOrWhiteSpace(personalnumber))
                return BadRequest(new { message = "Invalid user data." });

            try
            {
                var user = await _unitOfWork.Users.GetByPersonalNumAsync(firstname, lastname, personalnumber);
                if (user == null)
                    return NotFound(new { message = $"User with data {firstname} {lastname} and {personalnumber} not found." });

                var userDto = user.Select(user => new UserGetDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    PersonalNumber = user.PersonalNumber,
                    BirthDate = user.BirthDate,
                    City = user.City.CityName,
                    ProfileImage = user.ProfileImage,
                    PhoneNumbers = user.PhoneNumbers.Select(p => new PhoneNumberDto
                    {
                        Type = p.Type,
                        Number = p.Number
                    }).ToList(),
                    Relationships = user.Relationships.Select(p => new PersonRelationshipDto
                    {
                        Type = p.Type,
                        RelatedPerson = p.RelatedPerson
                    }).ToList()
                });

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the user.", error = ex.Message });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUsers([FromQuery] UserGetDto searchFilter, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                if (page <= 0 || pageSize <= 0)
                {
                    return BadRequest(new { message = "Page number and page size must be greater than zero." });
                }

                var (users, totalCount) = await _unitOfWork.Users.SearchUsersAsync(searchFilter, page, pageSize);

                var userDtos = users.Select(user => new UserGetDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    PersonalNumber = user.PersonalNumber,
                    BirthDate = user.BirthDate,
                    City = user.City.CityName,
                    ProfileImage = user.ProfileImage,
                    PhoneNumbers = user.PhoneNumbers.Select(p => new PhoneNumberDto
                    {
                        Type = p.Type,
                        Number = p.Number
                    }).ToList(),
                    Relationships = user.Relationships.Select(p => new PersonRelationshipDto
                    {
                        Type = p.Type,
                        RelatedPerson = p.RelatedPerson
                    }).ToList()
                }).ToList();

                var result = new
                {
                    Users = userDtos,
                    TotalCount = totalCount,
                    Page = page,
                    PageSize = pageSize
                };

                return Ok(userDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while searching for users.", error = ex.Message });
            }
        }


    }
}
