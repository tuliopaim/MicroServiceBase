namespace CQRS.Core.Application
{
    public class QueryPaginationResult : IQueryPaginationResult
    {
        public int TotalElements { get; set; }
        public int Size { get; set; }   
        public int Number { get; set; }
    }
}