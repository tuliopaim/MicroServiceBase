using EasyCqrs.Queries;
using Microsoft.EntityFrameworkCore;

namespace MSBase.Core.Extensions;

public static class PaginationExtension
{
    public static async Task<(List<TResultItem> Result, QueryPagination pagination)> PaginateAsync<TResultItem>(
        this IQueryable<TResultItem> query,
        int pageNumber = 0,
        int pageSize = 10,
        CancellationToken cancellationToken = new())
        where TResultItem : class, new()
    {
        pageNumber = pageNumber < 0 ? 0 : pageNumber;
        pageSize = pageSize < 1 ? 10 : pageSize;

        var totalElements = await query.CountAsync(cancellationToken);

        var startRow = pageNumber * pageSize;
        var items = await query
            .Skip(startRow)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        
        return (items, new QueryPagination
        {
            PageNumber = pageNumber,
            PageSize = items.Count,
            TotalElements = totalElements
        });  
    }
}