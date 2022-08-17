using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Business.Interface;
using DataAccess.Context;
using Models.Filters;
using Models.Infrastructure;
using Moq;
using Xunit;

namespace Client.Unit.Test
{
    public class ClientTest
    {
        private readonly IClientService _clientService;
        private readonly Mock<IUnitOfWork<DBContext>> _unitOfWorkMock;

        public ClientTest()
        {
            var fixture = new TestFixture();

            _clientService = fixture.ClientService;
            _unitOfWorkMock = fixture.unitOfWorkMock;
        }

        #region CRUD

        [Fact]
        public void Client_Create_Success()
        {
            var Client = new Models.Business.Client()
            {
                CPF = "456.462.300-12",
                Name = "João",
                State = "MG",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var result = _clientService.Create<Models.Business.Client>(Client);

            Assert.True(result is not null);
            Assert.IsType<Models.Business.Client>(result);
            _unitOfWorkMock.Verify(uow => uow.SaveChanges(false), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        public void Client_Get_Success(int Id)
        {
            var result = _clientService.GetById<Models.Business.Client>(Id);

            Assert.True(result is not null);
            Assert.True(result.Id == Id);
            Assert.IsType<Models.Business.Client>(result);
        }

        [Theory]
        [InlineData(0)]
        public void Client_Get_Null(int Id)
        {
            var result = _clientService.GetById<Models.Business.Client>(Id);

            Assert.True(result == null);
        }

        [Fact]
        public void Client_Search_Success()
        {
            var filter = new ClientFilter()
            {
                CPF = "869.669.460-01"
            };

            var result = _clientService.Search<Models.Business.Client>(filter);

            Assert.NotNull(result);
            Assert.True(result.Items.Any());
            Assert.Equal(1, result.Items?.FirstOrDefault()?.Id);
            Assert.IsType<List<Models.Business.Client>>(result.Items);
            Assert.IsType<SearchResponse<Models.Business.Client>>(result);
        }

        [Fact]
        public void Client_Search_Null()
        {
            //Arrange
            var filter = new ClientFilter()
            {
                CPF = "123456789"
            };

            //Act
            var result = _clientService.Search<Models.Business.Client>(filter);

            //Assert
            Assert.NotNull(result);
            Assert.False(result.Items.Any());
            Assert.IsType<List<Models.Business.Client>>(result.Items);
            Assert.IsType<SearchResponse<Models.Business.Client>>(result);
        }

        [Fact]
        public void Client_Update_Success()
        {
            var Client = new Models.Business.Client()
            {
                Id = 1,
                CPF = "869.669.460-01",
                Name = "João",
                State = "MG",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var result = _clientService.Update<Models.Business.Client>(Client);

            Assert.True(result != null);
            Assert.IsType<Models.Business.Client>(result);
            _unitOfWorkMock.Verify(uow => uow.SaveChanges(false), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        public void Client_Delete_Success(int Id)
        {
            _clientService.Delete(Id);

            _unitOfWorkMock.Verify(uow => uow.SaveChanges(false), Times.Once);
        }

        #endregion
    }
}
