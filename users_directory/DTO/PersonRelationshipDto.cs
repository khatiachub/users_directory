using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using users_directory.Models;

namespace users_directory.DTO
{
    public class PersonRelationshipDto
    {
        [Required]
        [EnumDataType(typeof(RelationshipType))]
        public RelationshipType Type { get; set; } = RelationshipType.ნათესავი;

        [Required]
        public string RelatedPerson { get; set; }
    }
}
