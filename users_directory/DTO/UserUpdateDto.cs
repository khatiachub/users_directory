using System.ComponentModel.DataAnnotations;
using users_directory.Models;

namespace users_directory.DTO
{
    public class UserUpdateDto
    {
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
        public Gender Gender { get; set; } = Gender.ქალი;
        [Required]
        [StringLength(11, MinimumLength = 11)]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "პირადი ნომერი უნდა იყოს ზუსტად 11 ციფრი.")]
        public string PersonalNumber { get; set; }
        [Required]
        [CustomValidation(typeof(UserDto),nameof(UserDto.ValidateBirthDate))]
        public DateOnly BirthDate { get; set; }
        [Required]
        public int CityId { get; set; }
        public virtual List<PhoneNumberDto> PhoneNumbers { get; set; } = new();
        
    }
}
