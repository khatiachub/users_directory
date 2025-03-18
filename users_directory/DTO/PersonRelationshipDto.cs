using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using users_directory.Models;

namespace users_directory.DTO
{
    public class PersonRelationshipDto
    {
        [Required]
        [EnumDataType(typeof(RelationshipType))]
        [SwaggerSchema(Description = "დასაშვები მნიშვნელობები:კოლეგა, ნათესავი,ნაცნობი")]
        public RelationshipType Type { get; set; }

        [Required]
        public string RelatedPerson { get; set; }
    }
}
