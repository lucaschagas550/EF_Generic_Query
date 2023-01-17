using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF.Generic_Query.API.Models
{
    [Table("Products")]
    public class Product : Entity
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required]
        [Precision(14, 2)]
        public decimal Price { get; set; }

        [ForeignKey("IdCategory")]
        public Guid IdCategory { get; set; }

        public virtual Category Category { get; set; }
    }
}
