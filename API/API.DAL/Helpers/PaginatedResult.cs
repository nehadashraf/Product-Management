
using Microsoft.EntityFrameworkCore;

namespace API.DAL.Helpers
{
    public class PaginationResult<T>
    {
        public PaginationMetadata Metadata { get; set; } = default!;
        public List<T> Items { get; set; } = [];
    }
    public class PaginationMetadata
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    };
    public class PaginationHelper
    {
        public static async Task<PaginationResult<T>> CreateAsync<T>(IQueryable<T> query, int pageNumber, int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var count = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginationResult<T>
            {
                Metadata = new PaginationMetadata
                {
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    TotalCount = count,
                    TotalPages = (int)Math.Ceiling(count / (double)pageSize)
                },
                Items = items
            };
        }

    };

}

