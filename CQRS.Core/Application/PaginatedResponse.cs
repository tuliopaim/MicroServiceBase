using System;
using System.Collections.Generic;

namespace CQRS.Core.Application
{
    public class PaginatedResponse<TItem> where TItem : IPagedQueryResultItem
    {
        public IEnumerable<TItem> Items { get; init; }
        
        public int Number { get; init; }
        
        public int Size { get; init; }

        public int TotalElements { get; init; }

        public int TotalPages => Size == 0
            ? 0
            : (int)Math.Ceiling(TotalElements / (double)Size);

        public int FirstPage => 0;

        public int LastPage => TotalPages == 0 ? 0 : TotalPages - 1;

        public bool HasPrevPage => Number >= 1;

        public bool HasNextPage => Number < LastPage;

        public int PrevPage => !HasPrevPage ? FirstPage : Number - 1;

        public int NextPage => !HasNextPage ? LastPage : Number + 1;
    }
}