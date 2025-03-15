using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace users_directory.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
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
        public Gender Gender { get; set; }
        [Required]
        [StringLength(11, MinimumLength = 11)]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "პირადი ნომერი უნდა იყოს ზუსტად 11 ციფრი.")]
        public string PersonalNumber { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(User), nameof(ValidateBirthDate))]
        public DateTime BirthDate { get; set; }
        [Required]
        public int CityId { get; set; }
        public virtual City City { get; set; }

        public virtual List<PhoneNumber> PhoneNumbers { get; set; } = new();

        public string PicturePath { get; set; }
        public virtual List<PersonRelationship> Relationships { get; set; } = new();

        public static ValidationResult ValidateBirthDate(DateTime birthDate, ValidationContext context)
        {
            return birthDate <= DateTime.Today.AddYears(-18)
                ? ValidationResult.Success
                : new ValidationResult("მინიმუმ 18 წლის უნდა იყოს.");
        }
    }
    public enum Gender
    {
        ქალი = 1,
        კაცი = 2
    }
}
