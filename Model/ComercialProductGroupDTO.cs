using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ComercialProductGroupDTO
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

        [Required]
        public int TermOfUse { get; set; }

        [Required]
        public bool Ends { get; set; }
    }
}
