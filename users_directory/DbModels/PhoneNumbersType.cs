using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using users_directory.Models;

namespace users_directory.DbModels
{
    public class PhoneNumbersType
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }

    }
}
