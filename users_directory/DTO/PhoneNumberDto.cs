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
        [SwaggerSchema(Description = "დასაშვები მნიშვნელობები: მობილური, ოფისის, სახლის")]
        public PhoneType Type { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Number { get; set; }
    }
   
}
