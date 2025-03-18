using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using users_directory.Models;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;
using System.Globalization;
using System.Text.Json;

namespace users_directory.DTO
{
    public class UserDto
    {
        [Key]
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^([ა-ჰ]+|[A-Za-z]+)$", ErrorMessage = "სახელი უნდა შეიცავდეს მხოლოდ ქართულ ან ლათინურ ასოებს.")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^([ა-ჰ]+|[A-Za-z]+)$", ErrorMessage = "გვარი უნდა შეიცავდეს მხოლოდ ქართულ ან ლათინურ ასოებს.")]
        public string LastName { get; set; }
        [Required]
        [EnumDataType(typeof(Gender))]
        [SwaggerSchema(Description = "Select gender from the list")]
        public Gender Gender { get; set; }
        [Required]
        [StringLength(11, MinimumLength = 11)]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "პირადი ნომერი უნდა იყოს ზუსტად 11 ციფრი.")]
        public string PersonalNumber { get; set; }
        [Required]
        [SwaggerSchema(Format = "date")] 
        public DateOnly BirthDate { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public virtual List<PhoneNumberDto> PhoneNumbers { get; set; } = new();
        [Required]
        public IFormFile ProfileImage { get; set; }
        [Required]
        public virtual List<PersonRelationshipDto> Relationships { get; set; } = new();

        public static ValidationResult ValidateBirthDate(DateTime birthDate, ValidationContext context)
        {
            return birthDate <= DateTime.Today.AddYears(-18)
                ? ValidationResult.Success
                : new ValidationResult("მინიმუმ 18 წლის უნდა იყოს.");
        }
       
    }

}
