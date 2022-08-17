using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Business.Interface;
using DataAccess.Context;
using Models.Business;
using Models.Filters;
using Models.Infrastructure;

namespace Business.Services
{
    public class ClientService : BaseService<Client, int>, IClientService
    {
        public ClientService(IUnitOfWork<DBContext> unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public SearchResponse<TOutputModel> Search<TOutputModel>(ClientFilter filter)
        {
            var response = base.Search<TOutputModel>(
               filter.GetFilter(),
               include: null,
               orderBy: null,
               filter.Page,
               filter.RowsPerPage
            );

            return response;
        }
    }
}
