using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tekton.Core.Entities
{
    public class Product
	{
        [Key]
        [Column("product_id")]
        public Guid ProductId { get; set; }

        [Column("name")]
        public string Name { get; set; } =null!;

        [Column("status_id")]
        public short StatusId { get; set; }

        [Column("stock")]
        public int Stock { get; set; }

        [Column("description")]
        public string Description { get; set; } = null!;

        [Column("price")]
        public decimal Price { get; set; }

        [Column("sku")]
        public string SKU { get; set; } = null!;

        [Column("creation_date")]
        public DateTime CreationDate { get; set; }

        [Column("update_date")]
        public DateTime? UpdateDate { get; set; }

        [Column("usuario_id")]
        public int? UserId { get; set; }

    }
}

