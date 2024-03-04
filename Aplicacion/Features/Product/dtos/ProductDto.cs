using System.ComponentModel.DataAnnotations.Schema;

namespace Tekton.Application.Features.Product.dtos
{
	public class ProductDto
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; } = null!;

        public short StatusId { get; set; }

        public int Stock { get; set; }

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public string SKU { get; set; } = null!;
    }
}

