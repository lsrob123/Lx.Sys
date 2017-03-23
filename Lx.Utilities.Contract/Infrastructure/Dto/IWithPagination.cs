namespace Lx.Utilities.Contract.Infrastructure.DTO {
    public interface IWithPagination {
        int PageNumber { get; set; }
        int PageSize { get; set; }
    }
}