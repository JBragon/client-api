namespace Models.Infrastructure
{
    public class SearchResponse<TEntity>
    {
        public SearchResponse()
        {

        }
        public SearchResponse(IEnumerable<TEntity> items, int rowsCount)
        {
            Items = items;
            RowsCount = rowsCount;
        }
        public IEnumerable<TEntity> Items { get; set; }
        public int RowsCount { get; set; }
    }
}
