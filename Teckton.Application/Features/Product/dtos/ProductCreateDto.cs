namespace Tekton.Application.Features.Product.dtos
{
	public class ProductCreateDto
	{
        public string Name { get; set; } = null!;

        public int StatusId { get; set; }

        public int Stock { get; set; }

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public string SKU { get; set; } = null!;
    }
}

