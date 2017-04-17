namespace Lx.Utilities.Contract.Infrastructure.Interfaces
{
    public interface IWithPagination
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
    }
}