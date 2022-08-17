using Billing.Performance.Test;
using Client.Performance.Test;
using NBomber.Contracts;
using NBomber.CSharp;
using System.Net.Http.Json;

namespace NBomberTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using var httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri("https://localhost:7290/");

            var step = Step.Create("GetClients", async context =>
            {
                var response = await httpClient.GetAsync("api/Client");

                return response.IsSuccessStatusCode
                    ? Response.Ok()
                    : Response.Fail();
            });

            var step2 = Step.Create("PostClient", async context =>
            {
                ClientPost post = new ClientPost
                {
                    CPF = CPFUtils.GerarCpf(),
                    Estado = "PT",
                    Nome = $"PerformanceTest-Client {Guid.NewGuid()}"
                };

                var response = await httpClient.PostAsJsonAsync($"api/Client", post);

                return response.IsSuccessStatusCode
                    ? Response.Ok()
                    : Response.Fail();
            });

            NBomberRunner
                .RegisterScenarios(ScenarioBuilder.CreateScenario("GetClientsRequest", step))
                .Run();

            NBomberRunner
                .RegisterScenarios(ScenarioBuilder.CreateScenario("PostClientRequest", step2))
                .Run();
        }
    }
}