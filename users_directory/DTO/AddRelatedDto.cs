using System.ComponentModel.DataAnnotations;
using users_directory.Models;

namespace users_directory.DTO
{
    public class AddRelatedDto
    {
        public int TypeId { get; set; }

        public int UserId { get; set; }

        public string RelatedPerson { get; set; }
    }
}
