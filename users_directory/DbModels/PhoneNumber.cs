using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace users_directory.Models
{
    public class PhoneNumber
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [EnumDataType(typeof(PhoneType))]
        public PhoneType Type { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Number { get; set; }

        [Required]
        public int PersonId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
    public enum PhoneType
    {
        მობილური,
        ოფისის,
        სახლის
    }
}
