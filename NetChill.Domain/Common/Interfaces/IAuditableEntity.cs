
namespace NetChill.Domain.Common.Interfaces
{
    //Adds additional properties to keep track of the entitie's audit trail information.
    public interface IAuditableEntity
    {
        DateTime? CreatedOn { get; set; }

        string? CreatedBy { get; set; }

        DateTime? LastModifiedOn { get; set; }

        string? LastModifiedBy { get; set; }
    }
}
