using Client.Integration.Test.Utils;
using Microsoft.AspNetCore.Mvc.Testing;
using Models.Infrastructure;
using Models.Mapper.Request;
using Models.Mapper.Response;
using System.Net.Http.Json;
using Xunit;

namespace Client.Integration.Test
{
    public class ClientAPITest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ClientAPITest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_Cliente_Success()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/api/Client");

            var clients = await response.ReadContentAs<SearchResponse<ClientResponse>>();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.NotNull(clients);
            Assert.IsType<SearchResponse<ClientResponse>>(clients);
        }

        [Theory]
        [InlineData("AA")]
        [InlineData("SS")]
        [InlineData("DD")]
        [InlineData("FF")]
        [InlineData("GG")]
        [InlineData("HH")]
        [InlineData("JJ")]
        [InlineData("KK")]
        public async Task Create_Cliente_Success(string uf)
        {
            // Arrange
            var client = _factory.CreateClient();

            ClientPost post = new ClientPost
            {
                CPF = CPFUtils.GerarCpf(),
                Name = $"IntegrationTest-Client {Guid.NewGuid()}",
                State = uf
            };

            // Act
            var response = await client.PostAsJsonAsync($"/api/Client", post);

            var clienteRegistered = await response.ReadContentAs<ClientResponse>();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.NotNull(clienteRegistered);
            Assert.IsType<ClientResponse>(clienteRegistered);

        }

        [Fact]
        public async Task Create_Cliente_Error()
        {
            // Arrange
            var client = _factory.CreateClient();

            ClientPost post = new ClientPost
            {
                CPF = Guid.NewGuid().ToString().Substring(0, 11),
                Name = "IntegrationTest-Client",
                State = "RR"
            };

            // Act
            var response = await client.PostAsJsonAsync($"/api/Client", post);

            // Assert
            Assert.False(response.IsSuccessStatusCode);

        }
    }
}
