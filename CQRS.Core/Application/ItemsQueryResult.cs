using System.Collections.Generic;

namespace CQRS.Core.Application
{
    public class ItemsQueryResult : QueryResult
    {
        public IEnumerable<IQueryResultItem> Items { get; set; }
    }

    public class PagedQueryResult : ItemsQueryResult
    {
        public IPagedQueryResultPagination Pagination { get; set; }
    }

    public interface IQueryResultItem
    {
    }
}