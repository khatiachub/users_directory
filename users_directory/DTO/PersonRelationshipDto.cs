using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using users_directory.Models;

namespace users_directory.DTO
{
    public class PersonRelationshipDto
    {
        [Required]
        public int TypeId { get; set; }

        [Required]
        public string RelatedPerson { get; set; }
    }
}
