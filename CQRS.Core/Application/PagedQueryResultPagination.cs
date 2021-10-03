namespace CQRS.Core.Application
{
    public class PagedQueryResultPagination : IPagedQueryResultPagination
    {
        public int TotalItems { get; set; }
        public int PageSize { get; set; }   
        public int CurrentPage { get; set; }
    }
}