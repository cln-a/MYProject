namespace Application.Model
{
    public class PageResult<T>
    {
        public List<T> items { get; set; }
        public int pagenumber { get; set; }
        public int pageSize { get; set; } = 25;
        public int totalCount { get; set; }
        public int totalPage { get; set; }

        public PageResult() { }

        public PageResult(int pagenumber, int pageSize, int totalCount, int totalPage, List<T> items)
        {
            this.items = items;
            this.pagenumber = pagenumber;
            this.pageSize = pageSize;
            this.totalCount = totalCount;
            this.totalPage = totalPage;
        }

        public PageResult<T1> Map<T1>(Func<T, T1> MapFunc) =>
            new()
            {
                pagenumber = this.pagenumber,
                pageSize = this.pageSize,
                totalCount = this.totalCount,
                totalPage = this.totalPage,
                items = this.items.Select(x => MapFunc.Invoke(x)).ToList()
            };

        public static PageResult<T> CreatePageFromSqlSugar(List<T> items, int pagenumber, int pageSize, int totalCount, int totalPage)
            => new()
            {
                items = items,
                pagenumber = pagenumber,
                pageSize = pageSize,
                totalCount = totalCount,
                totalPage = totalPage
            };
    }
}
