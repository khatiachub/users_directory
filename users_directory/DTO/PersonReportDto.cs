using System.Text.Json.Serialization;
using users_directory.DbModels;

namespace users_directory.DTO
{
    public class PersonReportDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
        [JsonIgnore]
        public virtual RelationshipType RelatedType { get; set; }
        public int RelatedPersons{ get; set; }
    }

}
