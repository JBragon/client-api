using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Business.Interface;
using Microsoft.EntityFrameworkCore.Query;
using Models.Infrastructure;
using System.Linq.Expressions;

namespace Business.Services
{
    public class BaseService<TEntity, TPrimarykey> : IBaseService<TPrimarykey>
        where TEntity : BaseEntity<TPrimarykey>
    {
        protected readonly IMapper _mapper;

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Properties
        protected readonly IUnitOfWork _unitOfWork;
        private IRepository<TEntity> repository;

        protected IRepository<TEntity> Repository
        {
            get
            {
                if (repository == null)
                {
                    repository = _unitOfWork.GetRepository<TEntity>();
                }
                return repository;
            }
        }

        #endregion

        public virtual TOutputModel Create<TOutputModel>(object inputModel)
        {
            var entity = _mapper.Map<TEntity>(inputModel);

            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;

            Repository.Insert(entity);
            _unitOfWork.SaveChanges();

            return _mapper.Map<TOutputModel>(entity);
        }

        public virtual TOutputModel GetById<TOutputModel>(TPrimarykey Id)
        {
            var outputModel = GetById<TOutputModel>(Id, null);

            return outputModel;
        }

        public virtual TOutputModel Update<TOutputModel>(object entity)
        {
            var item = _mapper.Map<TEntity>(entity);

            item.UpdatedAt = DateTime.Now;

            Repository.Update(item);
            _unitOfWork.SaveChanges();

            return _mapper.Map<TOutputModel>(item);

        }

        //public virtual async Task Delete(TPrimarykey id)
        public virtual void Delete(TPrimarykey id)
        {
            var veiculo = Repository.GetFirstOrDefault(predicate: v => v.Id.Equals(id));
            Repository.Delete(veiculo);
            _unitOfWork.SaveChanges();
        }

        public virtual bool Exist(TPrimarykey Id)
        {
            return Repository.GetFirstOrDefault(predicate: v => v.Id.Equals(Id)) != null;
        }

        protected virtual SearchResponse<TEntity> Search(Expression<Func<TEntity, bool>> filter,
                                                      Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                      int pageIndex = 0,
                                                      int pageSize = 10)
        {
            var response = Repository.GetPagedList(
                filter,
                include: include,
                orderBy: orderBy,
                pageIndex: pageIndex,
                pageSize: pageSize
                );

            return new SearchResponse<TEntity>()
            {
                Items = response.Items,
                RowsCount = response.TotalCount
            };
        }

        protected virtual SearchResponse<TOutputModel> Search<TOutputModel>(Expression<Func<TEntity, bool>> filter,
                                                      Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                      int pageIndex = 0,
                                                      int pageSize = 10)
        {
            var response = Search(filter, include, orderBy, pageIndex, pageSize);
            return new SearchResponse<TOutputModel>(_mapper.Map<IEnumerable<TOutputModel>>(response.Items), response.RowsCount);
        }


        protected virtual TOutputModel GetById<TOutputModel>(TPrimarykey Id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var entity = Repository.GetFirstOrDefault(predicate: v => v.Id.Equals(Id), include: include);

            var outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }
    }
}
