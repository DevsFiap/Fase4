using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Fase04.Api;

namespace TechChallengeFase02.IntegrationTests
{
    public class ApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public ApiIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
            _client.BaseAddress = new Uri("http://128.203.65.250"); // Nome do serviço do Docker
        }

        [Fact]
        public async Task Test_CreateContact_ReturnsSuccess()
        {
            var jsonData = JsonConvert.SerializeObject(new 
            { 
                Nome = "Nome Usuario",
                Telefone = "11987654327",
                Email = "user@example.com"
            });
            HttpContent httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/contatos/criar-contato", httpContent); 
            response.EnsureSuccessStatusCode();  // Valida se o status é sucesso

            var content = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(content);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            Console.WriteLine("Test_CreateContact_ReturnsSuccess passou com sucesso!");
        }

        [Fact]
        public async Task Test_CreateContact_ReturnsErrorStatus()
        {
            var jsonData = JsonConvert.SerializeObject(new
            {
                Nome = "Nome Usuario",
                Telefone = "783899",
                Email = "user@example.com"
            });
            HttpContent httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/contatos/criar-contato", httpContent);
            
            Assert.False(response.IsSuccessStatusCode);

            Console.WriteLine("Test_CreateContact_ReturnsErrorStatus passou com sucesso!");
        }
  
  
    }
}