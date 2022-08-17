using Client.Integration.Test.Utils;
using Microsoft.AspNetCore.Mvc.Testing;
using Models.Infrastructure;
using Models.Mapper.Request;
using Models.Mapper.Response;
using Newtonsoft.Json;
using System.Text;
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

            var data = await response.Content.ReadAsStringAsync();
            var clients = JsonConvert.DeserializeObject<SearchResponse<ClientResponse>>(data);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.True(clients is not null);
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
                Nome = $"IntegrationTest-Client {Guid.NewGuid()}",
                Estado = uf
            };

            var json = JsonConvert.SerializeObject(post);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync($"/api/Client", stringContent);

            var data = await response.Content.ReadAsStringAsync();
            var clienteRegistered = JsonConvert.DeserializeObject<ClientResponse>(data);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.True(clienteRegistered is not null);
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
                Nome = "IntegrationTest-Client",
                Estado = "RR"
            };

            var json = JsonConvert.SerializeObject(post);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync($"/api/Client", stringContent);

            // Assert
            Assert.True(response.IsSuccessStatusCode is false);

        }
    }
}
