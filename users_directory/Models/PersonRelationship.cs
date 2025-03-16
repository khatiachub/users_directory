﻿using System.ComponentModel.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace users_directory.Models
{
    public class PersonRelationship
    {
        [Key]
        public int Id { get; set; }

        [EnumDataType(typeof(RelationshipType))]
        public RelationshipType Type { get; set; }

        public int UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }

        public string RelatedPerson { get; set; }
    }
    public enum RelationshipType
    {
        კოლეგა = 1,
        ნაცნობი = 2,
        ნათესავი = 3
    }
}
