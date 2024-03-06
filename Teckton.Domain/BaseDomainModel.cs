using System.ComponentModel.DataAnnotations.Schema;

namespace Tekton.Domain
{
    public abstract class BaseDomainModel
    {
        [Column("created_date")]
        public DateTime? CreatedDate { get; set; }

        [Column("created_by")]
        public string? CreatedBy { get; set; }

        [Column("last_modified_date")]
        public DateTime? LastModifiedDate { get; set; }

        [Column("last_modified_by")]
        public string? LastModifiedBy { get; set; }
    }
}

