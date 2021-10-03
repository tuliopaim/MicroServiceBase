using System;

namespace CQRS.Core.Application
{
    public interface IPagedQueryResultPagination
    {
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages => PageSize == 0
            ? 0
            : (int)Math.Ceiling(TotalItems / (double)PageSize);

        public int FirstPage => 0;

        public int LastPage => TotalPages == 0 ? 0 : TotalPages - 1;

        public bool HasPrevPage => CurrentPage >= 1;

        public bool HasNextPage => CurrentPage < LastPage;

        public int PrevPage => !HasPrevPage ? FirstPage : CurrentPage - 1;

        public int NextPage => !HasNextPage ? LastPage : CurrentPage + 1;
    }
}