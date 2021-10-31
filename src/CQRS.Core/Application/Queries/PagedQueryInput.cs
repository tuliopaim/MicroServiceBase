﻿using Microsoft.AspNetCore.Mvc;

namespace CQRS.Core.Application.Queries
{
    public class PagedQueryInput<TQueryResult>
        : QueryInput<TQueryResult> where TQueryResult : QueryResult
    {
        [FromQuery]
        public int PageSize { get; set; }
        [FromQuery]
        public int PageNumber { get; set; }
    }
}