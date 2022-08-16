using System.Linq.Expressions;

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

        public string Sort { get; set; }
        //private string _sort { get; set; }
        public string SortDir { get; set; }

        public Func<IQueryable<TModel>, IOrderedQueryable<TModel>> GetOrder<TModel>(bool ascending = true, params Expression<Func<TModel, object>>[] orderingProperties)
        {
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> order = null;
            if (!string.IsNullOrEmpty(Sort))
            {
                var sort = string.Concat(Sort, ":", SortDir).Split(',');

                foreach (var property in sort)
                {
                    //Verificar se existe o separador ':'
                    var field = property.Split(':');

                    //Verificar se o atributo existe  
                    order = source => source.OrderByProperty(field[0], field[1].ToUpper() == "ASC");
                }
            }
            foreach (var expressionProperty in orderingProperties)
            {
                order = source => source.OrderByProperty(GetPropertyName(expressionProperty.Body), ascending);
            }
            return order;
        }

        private string GetPropertyName(Expression expression)
        {
            return ((MemberExpression)((UnaryExpression)expression).Operand).Member.Name;
        }

    }
}
