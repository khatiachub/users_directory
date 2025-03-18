using System.Text.Json.Serialization;
using users_directory.Models;

namespace users_directory.DTO
{
    public class UserGetDto
    {
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Gender? Gender { get; set; }
        public string? PersonalNumber { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? City { get; set; } 
        public string? ProfileImage { get; set; }
        public List<PhoneNumberDto>? PhoneNumbers { get; set; }
        public List<PersonRelationshipDto>? Relationships { get; set; }
    }

}
