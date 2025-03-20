using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;
using users_directory.DbModels;
using users_directory.DTO;


namespace users_directory.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }   
        public string FirstName { get; set; }
    
        public string LastName { get; set; }
        public int GenderId { get; set; }
        public virtual GendersType Gender { get; set; }

        public string PersonalNumber { get; set; }
        [DataType(DataType.Date)]
        public DateOnly BirthDate { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }

        public virtual List<PhoneNumber> PhoneNumbers { get; set; } = new();

        public string? ProfileImage { get; set; }
        public virtual List<PersonRelationship> Relationships { get; set; } = new();
    }
  

}
