using DAL.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class ComercialProductGroup : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int PurchasePrice { get; set; }

        [Required]
        public int SellPrice { get; set; }

        [Required]
        public int DeliveryTime { get; set; }

        public int TermOfUse { get; set; }

        [Required]
        public bool Ends { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}
