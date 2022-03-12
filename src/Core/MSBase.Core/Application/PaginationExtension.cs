using Microsoft.EntityFrameworkCore;
using MSBase.Core.Application.Queries;

namespace MSBase.Core.Application;

public static class PaginationExtension
{
    public static async Task<PagedQueryResult<TResultItem>> PaginateAsync<TResultItem>(
        this IQueryable<TResultItem> query,
        int pageNumber = 0,
        int pageSize = 10,
        CancellationToken cancellationToken = new())
        where TResultItem : IPagedQueryResultItem
    {
        pageNumber = pageNumber < 0 ? 0 : pageNumber;
        pageSize = pageSize < 1 ? 10 : pageSize;

        var totalElements = await query.CountAsync(cancellationToken);

        var startRow = pageNumber * pageSize;
        var items = await query
            .Skip(startRow)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var result = new PagedQueryResult<TResultItem>
        {
            Result = items,
            Pagination = new QueryPaginationResult
            {
                Number = pageNumber,
                Size = items.Count,
                TotalElements = totalElements
            }
        };

        return result;
    }
}