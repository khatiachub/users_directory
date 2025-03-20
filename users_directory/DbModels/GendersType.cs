using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace users_directory.DbModels
{
    public class GendersType
    {
        [Key]
        public int Id
        {
            get; set;
        }
        public string Gender
        {
            get; set;
        }
    } 
}
