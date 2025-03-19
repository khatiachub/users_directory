using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using users_directory.Models;

namespace users_directory.DTO
{
    public class PhoneNumberDto
    {
        [Required]
        [EnumDataType(typeof(PhoneType))]
        public PhoneType Type { get; set; } = PhoneType.მობილური;
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Number { get; set; }
    }
   
}
