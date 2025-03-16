using System.ComponentModel.DataAnnotations;
using users_directory.Models;

namespace users_directory.DTO
{
    public class PhoneNumberDto
    {
        [Required]
        [EnumDataType(typeof(PhoneType))]
        public PhoneType Type { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Number { get; set; }
    }
}
