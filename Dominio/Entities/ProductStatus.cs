using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tekton.Domain.Entities
{
    public class ProductStatus: BaseDomainModel
    {
        [Key]
        [Column("status_id")]
        public int StatusId { get; set; }

        [Column("description")]
        public string Description { get; set; } = null!;

       

    }
}

