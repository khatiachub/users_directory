using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
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
                    Gender = user.Gender?.Gender,  
                    PersonalNumber = user.PersonalNumber,
                    BirthDate = user.BirthDate,
                    City = user.City?.CityName, 
                    ProfileImage = user.ProfileImage?.ToString(), 
                    PhoneNumbers = user.PhoneNumbers?.Select(p => new PhoneNumberGetDto
                    {
                        Type = p.NumberType?.Type, 
                        Number = p.Number
                    }).ToList() ?? new List<PhoneNumberGetDto>(),  
                    Relationships = user.Relationships?.Select(p => new RelationshipGetDto
                    {
                        Type = p.RelatedType?.Type,  
                        RelatedPerson = p.RelatedPerson
                    }).ToList() ?? new List<RelationshipGetDto>()  
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
                    Gender = user.Gender.Gender,
                    PersonalNumber = user.PersonalNumber,
                    BirthDate = user.BirthDate,
                    City = user.City.CityName,
                    ProfileImage = user.ProfileImage?.ToString(),
                    PhoneNumbers = user.PhoneNumbers.Select(p => new PhoneNumberGetDto
                    {
                        Type = p.NumberType.Type,
                        Number = p.Number
                    }).ToList(),
                    Relationships = user.Relationships.Select(p => new RelationshipGetDto
                    {
                        Type = p.RelatedType.Type,
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
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            if (userDto == null)
                return BadRequest(new { message = "User data cannot be null." });

            if (string.IsNullOrWhiteSpace(userDto.FirstName) || string.IsNullOrWhiteSpace(userDto.LastName))
                return BadRequest(new { message = "First name and last name are required." });
            var cityExists = await _unitOfWork.City.GetByIdAsync(userDto.CityId);
            if (cityExists == null)
            {
                return BadRequest(new { message = $"City with ID {userDto.CityId} does not exist." });
            }

            try
            {
                var user = new User
                {
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    GenderId = userDto.GenderId,
                    PersonalNumber = userDto.PersonalNumber,
                    BirthDate = userDto.BirthDate,
                    CityId = userDto.CityId,
                    PhoneNumbers = userDto.PhoneNumbers.Select(p => new PhoneNumber
                    {
                        TypeId = p.TypeId,
                        Number = p.Number
                    }).ToList(),

                    Relationships = userDto.Relationships.Select(r => new PersonRelationship
                    {
                        RelatedTypeId = r.TypeId,
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
        [HttpPut("uploadImage/")]
        public async Task<IActionResult> UploadImageForUser([FromForm] UploadImageDto UploadDto)
        {
            if (UploadDto.UserId <= 0)
                return BadRequest(new { message = "Invalid user ID." });

            if (UploadDto.ProfileImage == null)
                return BadRequest(new { message = "User data cannot be null." });
            try
            {
                var existingUser = await _unitOfWork.Users.GetByIdAsync(UploadDto.UserId);
                string imagePath = await _unitOfWork.Users.UploadProfileImage(UploadDto.ProfileImage);

                if (existingUser == null)
                    return NotFound(new { message = $"User with ID {UploadDto.UserId} not found." });
                existingUser.ProfileImage = imagePath;
                _unitOfWork.Users.Update(existingUser);
                await _unitOfWork.CompleteAsync();

                return Ok(new { message = "User updated successfully." });
            }

            catch(Exception ex) {
                return StatusCode(500, new { message = "An error occurred while updating the user.", error = ex.Message });

            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto userDto)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid user ID." });

            if (userDto == null)
                return BadRequest(new { message = "User data cannot be null." });

            try
            {
                var existingUser = await _unitOfWork.Users.GetByIdAsync(id);
                if (existingUser == null) return NotFound(new { message = $"User with ID {id} not found." });
                var cityExists = await _unitOfWork.City.GetByIdAsync(userDto.CityId);
                if (cityExists==null)
                {
                    return BadRequest(new { message = $"City with ID {userDto.CityId} does not exist." });
                }

                existingUser.FirstName = userDto.FirstName;
                existingUser.LastName = userDto.LastName;
                existingUser.GenderId = userDto.GenderId;
                existingUser.PersonalNumber = userDto.PersonalNumber;
                existingUser.BirthDate = userDto.BirthDate;
                existingUser.CityId = userDto.CityId;
                foreach (var existingPhone in existingUser.PhoneNumbers)
                {
                    var updatedPhone = userDto.PhoneNumbers.FirstOrDefault(p => p.TypeId == existingPhone.TypeId);
                    if (updatedPhone != null)
                    {
                        existingPhone.Number = updatedPhone.Number; 
                    }
                }

                await  _unitOfWork.Users.Update(existingUser);
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
             
                var user= await _unitOfWork.Users.Delete(id);  
                if(!user)
                {
                    return BadRequest(new { message = "Invalid user ID." });
                }
                
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
                    Gender = user.Gender.Gender,
                    PersonalNumber = user.PersonalNumber,
                    BirthDate = user.BirthDate,
                    City = user.City.CityName,
                    ProfileImage = user.ProfileImage,
                    PhoneNumbers = user.PhoneNumbers.Select(p => new PhoneNumberGetDto
                    {
                        Type = p.NumberType.Type,
                        Number = p.Number
                    }).ToList(),
                    Relationships = user.Relationships.Select(p => new RelationshipGetDto
                    {
                        Type = p.RelatedType.Type,
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
        public async Task<IActionResult> SearchUsers([FromQuery] UserSearchDto searchFilter, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
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
                    Gender = user.Gender.Gender,
                    PersonalNumber = user.PersonalNumber,
                    BirthDate = user.BirthDate,
                    City = user.City.CityName,
                    ProfileImage = user.ProfileImage,
                    PhoneNumbers = user.PhoneNumbers.Select(p => new PhoneNumberGetDto
                    {
                        Type = p.NumberType.Type,
                        Number = p.Number
                    }).ToList(),
                    Relationships = user.Relationships.Select(p => new RelationshipGetDto
                    {
                        Type = p.RelatedType.Type,
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
        [HttpGet("getReport/{id}")]
        public async Task<ActionResult<IEnumerable<PersonReportDto>>> GetReport(int id)
        {
            try
            {
                var report = await _unitOfWork.Users.ReportRelatedPersons(id);
                
                return Ok(report);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching users.", error = ex.Message });
            }
        }

    }
}
