using System.ComponentModel.DataAnnotations;
using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using users_directory.DbModels;

namespace users_directory.Models
{
    public class PersonRelationship
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int RelatedTypeId { get; set; }
        [JsonIgnore]
        public virtual RelationshipType RelatedType { get; set; }

        public int UserId { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; }

        public string RelatedPerson { get; set; }
    }
}
