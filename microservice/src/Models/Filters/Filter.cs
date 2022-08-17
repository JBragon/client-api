namespace Models.Filters
{
    public class Filter
    {
        private int? _rowPerPage;
        public int RowsPerPage
        {
            get
            {
                if (!this._rowPerPage.HasValue)
                    this._rowPerPage = 10; // Este valor padrão deve ficar como parametro da aplicação web.
                return this._rowPerPage.Value;
            }
            set { this._rowPerPage = value; }
        }

        private int _pageIndex = 0;

        public int Page
        {
            get { return this._pageIndex; }
            set { this._pageIndex = value; }
        }

    }
}
