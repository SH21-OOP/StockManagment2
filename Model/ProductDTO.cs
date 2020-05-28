using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ProductDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ComercialProductGroupID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Preparation { get; set; }

        [Required]
        public DateTime Expiration { get; set; }

        [Required]
        [Range(1, 999999)]
        public int Quantity { get; set; }
    }
}
