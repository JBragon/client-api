using LinqKit;
using Models.Business;
using System.Linq.Expressions;

namespace Models.Filters
{
    public class ClientFilter : Filter
    {
        public string? CPF { get; set; }

        private Expression<Func<Client, bool>> filter = PredicateBuilder.New<Client>(true);
        private Func<IQueryable<Client>, IOrderedQueryable<Client>> order;

        public Expression<Func<Client, bool>> GetFilter()
        {
            if (!string.IsNullOrWhiteSpace(CPF))
                filter = filter.And(x => x.CPF == CPF);

            return filter;
        }

    }
}
