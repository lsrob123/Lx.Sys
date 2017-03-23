namespace Lx.Utilities.Contract.Infrastructure.DTO {
    public interface IWithPaginatedList : IWithPagination, IDisplayOrder {
        int? PageCount { get; set; }
        long? ItemCount { get; set; }
    }
}