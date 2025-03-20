using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using users_directory.DbModels;

namespace users_directory.DTO
{
    public class PhoneNumberGetDto
    {
        [Required]
        public string Type { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Number { get; set; }
    }
}
