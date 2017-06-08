namespace Lx.Utilities.Contract.Infrastructure.Interfaces {
    public interface IWithPagination : IDisplayOrder {
        int PageNumber { get; set; }
        int PageSize { get; set; }
    }
}