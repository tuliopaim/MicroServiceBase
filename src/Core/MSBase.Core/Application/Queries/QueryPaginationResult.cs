namespace MSBase.Core.Application.Queries;

public class QueryPaginationResult : IQueryPaginationResult
{
    public int TotalElements { get; set; }
    public int Size { get; set; }
    public int Number { get; set; }
}