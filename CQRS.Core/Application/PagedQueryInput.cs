﻿using Microsoft.AspNetCore.Mvc;

namespace CQRS.Core.Application
{
    public class PagedQueryInput<TQueryResult> 
        : MediatorInput<TQueryResult> where TQueryResult : QueryResult
    {
        [FromQuery]
        public int PageSize { get; set; }
        [FromQuery]
        public int PageNumber { get; set; }
    }
}