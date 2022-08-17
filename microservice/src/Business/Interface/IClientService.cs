using Models.Filters;
using Models.Infrastructure;

namespace Business.Interface
{
    public interface IClientService : IBaseService<int>
    {
        SearchResponse<TOutputModel> Search<TOutputModel>(ClientFilter filter);
    }
}
