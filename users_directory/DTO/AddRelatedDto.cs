using System.ComponentModel.DataAnnotations;
using users_directory.Models;

namespace users_directory.DTO
{
    public class AddRelatedDto
    {
        [EnumDataType(typeof(RelationshipType))]
        public RelationshipType Type { get; set; }

        public int UserId { get; set; }

        public string RelatedPerson { get; set; }
    }
}
