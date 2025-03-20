using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using users_directory.DTO;
using users_directory.Models;
using users_directory.Services;

namespace users_directory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatedPersonController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RelatedPersonController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost("addRelatedPerson")]
        public async Task<IActionResult> AddRelatedPerson([FromBody] AddRelatedDto relationshipDto)
        {
            if (relationshipDto == null)
                return BadRequest(new { message = "User data cannot be null." });

            var userExists = await _unitOfWork.Users.GetByIdAsync(relationshipDto.UserId);
            if (userExists == null)
            {
                return BadRequest(new { message = $"User with ID {relationshipDto.UserId} does not exist." });
            }

            try
            {
                var user = new PersonRelationship
                {
                    RelatedTypeId = relationshipDto.TypeId,
                    UserId = relationshipDto.UserId,
                    RelatedPerson = relationshipDto.RelatedPerson
                };

                await _unitOfWork.Relationships.AddAsync(user);
                await _unitOfWork.CompleteAsync();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the user.", error = ex.Message });
            }
        }
        [HttpDelete("removeRelatedPerson/{id}/{userId}")]
        public async Task<IActionResult> DeleteRelatedPerson(int id, int userId)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid user ID." });

            try
            {
                await _unitOfWork.Relationships.DeleteRelated(id, userId);
                await _unitOfWork.CompleteAsync();

                return Ok(new { message = "User deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the user.", error = ex.Message });
            }
        }

    }
}
