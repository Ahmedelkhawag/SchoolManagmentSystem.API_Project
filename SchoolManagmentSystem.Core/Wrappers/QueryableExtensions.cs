using Microsoft.EntityFrameworkCore;

namespace SchoolManagmentSystem.Core.Wrappers
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedResult<T>> ToPaginatedListAsynd<T>(this IQueryable<T> source, int pageNumber, int pageSize) where T : class
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            var totalCount = await source.AsNoTracking().CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            if (totalCount == 0) return PaginatedResult<T>.Success(new List<T>(), pageNumber, totalPages, totalCount, pageSize, false);
            var data = await source.AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return PaginatedResult<T>.Success(data, pageNumber, totalPages, totalCount, pageSize, true);
        }
    }
}
