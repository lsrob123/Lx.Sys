namespace Lx.Utilities.Contract.Infrastructure.Dto {
    public interface IWithPaginatedList : IWithPagination, IDisplayOrder {
        int? PageCount { get; set; }
        long? ItemCount { get; set; }
    }
}