using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Core.Application
{
    public static class PaginationExtension
    {
        public static async Task<PaginatedResponse<TResultItem>> PaginateAsync<TResultItem>(
            this IQueryable<TResultItem> query,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken)
            where TResultItem : IPagedQueryResultItem
        {
            pageNumber = (pageNumber < 0) ? 0 : pageNumber;
            
            var totalElements = await query.CountAsync(cancellationToken);

            var startRow = (pageNumber) * pageSize;
            var items = await query
                .Skip(startRow)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            var result = new PaginatedResponse<TResultItem>
            {
                Items = items,
                Number = pageNumber,
                Size = pageSize,
                TotalElements = totalElements
            };

            return result;
        }
    }
}