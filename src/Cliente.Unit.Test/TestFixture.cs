using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Business.Interface;
using Business.Services;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using Moq.AutoMock;

namespace Client.Unit.Test
{
    public class TestFixture
    {
        public readonly IClientService ClientService;
        public readonly Mock<IUnitOfWork<DBContext>> unitOfWorkMock;
        public readonly Mock<IRepository<Models.Business.Client>> repositoryMock;
        public readonly IMapper mapper;

        public TestFixture()
        {
            repositoryMock = new Mock<IRepository<Models.Business.Client>>();
            var context = new Mock<DBContext>();
            var dbFacade = new Mock<DatabaseFacade>(context.Object);

            var mockMapper = new MapperConfiguration(cfg =>
            {
            });

            mapper = mockMapper.CreateMapper();

            var client = new Models.Business.Client()
            {
                Id = 1,
                CPF = "869.669.460-01",
                Name = "José",
                State = "MG",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            List<Models.Business.Client> entitybases = new List<Models.Business.Client>() { client };

            var dbSetMock = new Mock<DbSet<Models.Business.Client>>();

            dbSetMock.As<IQueryable<Models.Business.Client>>().Setup(x => x.Provider).Returns(entitybases.AsQueryable().Provider);
            dbSetMock.As<IQueryable<Models.Business.Client>>().Setup(x => x.Expression).Returns(entitybases.AsQueryable().Expression);
            dbSetMock.As<IQueryable<Models.Business.Client>>().Setup(x => x.ElementType).Returns(entitybases.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<Models.Business.Client>>().Setup(x => x.GetEnumerator()).Returns(entitybases.AsQueryable().GetEnumerator());

            context.Setup(x => x.Set<Models.Business.Client>()).Returns(dbSetMock.Object);

            var repository = new Repository<Models.Business.Client>(context.Object);

            var dbContextTransaction = new Mock<IDbContextTransaction>();

            AutoMocker mocker = new AutoMocker();
            mocker.GetMock<IUnitOfWork<DBContext>>().Setup(uow => uow.GetRepository<Models.Business.Client>(false)).Returns(repository);
            mocker.GetMock<IUnitOfWork<DBContext>>().Setup(uow => uow.DbContext.Database).Returns(dbFacade.Object);
            mocker.GetMock<IUnitOfWork<DBContext>>().Setup(uow => uow.DbContext.Database.BeginTransaction()).Returns(dbContextTransaction.Object);
            mocker.GetMock<IUnitOfWork<DBContext>>().Setup(uow => uow.DbContext.Set<Models.Business.Client>()).Returns(dbSetMock.Object);

            unitOfWorkMock = mocker.GetMock<IUnitOfWork<DBContext>>();

            ClientService = new ClientService(unitOfWorkMock.Object, mapper);
        }

    }
}
