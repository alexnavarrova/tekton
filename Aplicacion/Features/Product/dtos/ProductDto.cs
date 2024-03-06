
namespace Tekton.Application.Features.Product.dtos
{
	public class ProductDto
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = null!;

        public string Status { get; set; } = null!;

        public int Stock { get; set; }

        public string Description { get; set; } = null!;

        public double Price { get; set; }

        public float Average { get; set; }

        public double FinalPrice { get; set; }

        public string SKU { get; set; } = null!;
    }
}

