using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using users_directory.Models;

namespace users_directory.DTO
{
    public class UserSearchDto
    {
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? PersonalNumber { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }
        public string? RelatedPerson {  get; set; }
    }
}
