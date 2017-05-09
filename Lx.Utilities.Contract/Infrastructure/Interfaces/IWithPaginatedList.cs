namespace Lx.Utilities.Contract.Infrastructure.Interfaces {
    public interface IWithPaginatedList : IWithPagination, IDisplayOrder {
        int? PageCount { get; set; }
        long? ItemCount { get; set; }
    }
}