using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tekton.Domain.Entities
{
    public class Product: BaseDomainModel
	{
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("name")]
        public string Name { get; set; } =null!;

        [Column("status_id")]
        public short StatusId { get; set; }

        [Column("stock")]
        public int Stock { get; set; }

        [Column("description")]
        public string Description { get; set; } = null!;

        [Column("price")]
        public double Price { get; set; }

        [Column("sku")]
        public string SKU { get; set; } = null!;

    }
}

