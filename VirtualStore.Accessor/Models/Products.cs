using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualStore.Accessor.Models
{
    [Table("Products")]
    public class Products
    {
        [Key]
        public int ProductID { get; set; }

        [Required, MaxLength(40)]
        public string ProductName { get; set; } = string.Empty;

        public int? SupplierID { get; set; }

        public int? CategoryID { get; set; }

        [MaxLength(20)]
        public string? QuantityPerUnit { get; set; }

        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }

        public short? UnitsOnOrder { get; set; }

        public short? ReorderLevel { get; set; }

        [Required]
        public bool Discontinued { get; set; }
    }
}
