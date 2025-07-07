using System;

namespace FGC_PWA2025.Helpers
{
    public class Pager
    {
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }

        public Pager(int totalItems, int currentPage, int pageSize = 5)
        {
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);

            int startPage = currentPage - 5;
            int endPage = currentPage + 4;

            if (startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }

            if (endPage > TotalPages)
            {
                endPage = TotalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            StartPage = startPage;
            EndPage = endPage;
        }

        public Pager() { }
    }
}
