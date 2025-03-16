using System.ComponentModel.DataAnnotations;
using users_directory.Models;

namespace users_directory.DTO
{
    public class PersonRelationshipDto
    {
        [Required]
        [EnumDataType(typeof(RelationshipType))]
        public RelationshipType Type { get; set; }

        [Required]
        public string RelatedPerson { get; set; }
    }
}
