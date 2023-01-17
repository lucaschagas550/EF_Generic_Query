using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF.Generic_Query.API.Models
{
    [Table("Categories")]
    public class Category : Entity
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
    }
}
