using DAL.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Product : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ComercialProductGroupID { get; set; }

        [ForeignKey(nameof(ComercialProductGroupID))]
        public ComercialProductGroup ProductGroup { get; set; }

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
