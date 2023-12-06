using NetChill.Domain.Common.Interfaces;

namespace NetChill.Domain.Common
{
    public class BaseAuditableEntity : BaseEntity, IAuditableEntity
    {
        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public string? LastModifiedBy { get; set; }
    }
}
