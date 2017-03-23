namespace Lx.Utilities.Contract.Infrastructure.Dto {
    public interface IWithPagination {
        int PageNumber { get; set; }
        int PageSize { get; set; }
    }
}