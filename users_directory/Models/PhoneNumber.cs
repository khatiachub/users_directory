using System;
using System.ComponentModel.DataAnnotations;

namespace users_directory.Models
{
    public class PhoneNumber
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EnumDataType(typeof(PhoneType))]
        public PhoneType Type { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Number { get; set; }

        [Required]
        public int PersonId { get; set; }
        public virtual User User { get; set; }
    }
    public enum PhoneType
    {
        მობილური = 1,
        ოფისის = 2,
        სახლის = 3
    }
}
