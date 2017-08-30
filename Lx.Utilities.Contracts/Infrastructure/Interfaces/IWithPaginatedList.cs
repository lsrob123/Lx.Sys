namespace Lx.Utilities.Contracts.Infrastructure.Interfaces
{
    public interface IWithPaginatedList : IWithPagination
    {
        int? PageCount { get; set; }
        long? ItemCount { get; set; }
    }
}