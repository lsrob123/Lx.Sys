namespace Lx.Utilities.Contract.Infrastructure.DTO {
    public static class PaginationExtension {
        public static TPaginatedList WithPaginationAndDisplayOrder<TPaginatedList>(this TPaginatedList list,
            int pageNumber, int pageSize, bool inDescendingOrder, long? itemCount = null)
            where TPaginatedList : IWithPaginatedList {
            list.PageNumber = pageNumber > 0 ? pageNumber : 1;
            list.PageSize = pageSize;
            list.InDescendingOrder = inDescendingOrder;
            list.ItemCount = itemCount;
            list.PageCount = null;

            if (!itemCount.HasValue)
                return list;

            list.PageCount = itemCount <= pageSize
                ? 1
                : (int) (itemCount.Value%pageSize == 0 ? itemCount.Value/pageSize : itemCount.Value/pageSize + 1);

            if (list.PageNumber > list.PageCount.Value)
                list.PageNumber = list.PageCount.Value;

            return list;
        }

        public static TPaginatedList WithPaginationAndDisplayOrder<TPaginatedList>(this TPaginatedList list,
            IWithPaginatedList sourceInfo) where TPaginatedList : IWithPaginatedList {
            return list.WithPaginationAndDisplayOrder(sourceInfo.PageNumber, sourceInfo.PageSize,
                sourceInfo.InDescendingOrder);
        }
    }
}