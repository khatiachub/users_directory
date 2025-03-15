using System.ComponentModel.DataAnnotations;
using System;

namespace users_directory.Models
{
    public class PersonRelationship
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EnumDataType(typeof(RelationshipType))]
        public RelationshipType Type { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public int RelatedPersonId { get; set; }
        public virtual User RelatedPerson { get; set; }
    }
    public enum RelationshipType
    {
        კოლეგა = 1,
        ნაცნობი = 2,
        ნათესავი = 3
    }
}
