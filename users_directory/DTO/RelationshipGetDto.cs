using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using users_directory.DbModels;

namespace users_directory.DTO
{
    public class RelationshipGetDto
    {
        [Required]
        public string Type { get; set; }
        [JsonIgnore]
        public virtual RelationshipType RelatedType { get; set; }

        [Required]
        public string RelatedPerson { get; set; }
    }
}
