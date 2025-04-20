namespace SchoolManagmentSystem.Core.Wrappers
{
    public class PaginatedResult<T>
    {
        public PaginatedResult(List<T> data)
        {
            Data = data;
        }
        public List<T> Data { get; set; }

        public PaginatedResult(List<T> data = default, List<string> Message = null, int curruntpage = 1, int totalCount = 0, int pageSize = 10, bool succeeded = true) : this(data)
        {

            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            TotalCount = totalCount;
            PageSize = pageSize;
            CurruntPage = curruntpage;
            Succeeded = succeeded;
        }

        public int CurruntPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public object Meta { get; set; }
        public int PageSize { get; set; }
        public bool HasPerviousPage => CurruntPage > 1;
        public bool HasNextPage => CurruntPage < TotalPages;
        public List<string> Message { get; set; } = new();
        public bool Succeeded { get; set; }

        public static PaginatedResult<T> Success(List<T> data, int curruntPage, int totalPages, int totalCount, int pageSize, bool succeeded)
        {
            return new(data, null, curruntPage, totalCount, pageSize, true);
        }
    }
}
