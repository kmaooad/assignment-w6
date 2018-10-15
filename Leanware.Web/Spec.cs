using System;
using SutStartup = KmaOoad18.Leanware.Web.Startup;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using KmaOoad18.Leanware.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System.Net.Http;
using Newtonsoft.Json;

namespace KmaOoad18.Leanware.Web
{
    public class Spec :
    IDisposable,
    IClassFixture<WebApplicationFactory<SutStartup>>
    {
        private readonly WebApplicationFactory<SutStartup> _factory;

        public Spec(WebApplicationFactory<SutStartup> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.UseSolutionRelativeContentRoot("./Leanware.Web/");
                builder.ConfigureServices(services =>
                {
                    services.AddDbContext<LeanwareContext, TestContext>(options =>
                    options.UseSqlite("Data Source=test.db"));
                });
            });
        }


        public void Dispose()
        {
            using (var db = new TestContext())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM [SpecialOfferings]");
                db.Database.ExecuteSqlCommand("DELETE FROM [Products]");
                db.Database.ExecuteSqlCommand("DELETE FROM [Customers]");
            }
        }

    }

    internal class LeanwareTestClient
    {
        private readonly HttpClient _client;

        public LeanwareTestClient(WebApplicationFactory<SutStartup> factory)
        {
            this._client = factory.CreateClient();
        }


        private async Task<HttpResponseMessage> Post<T>(string path, T dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, path);

            request.Content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return response;
        }
    }

    internal class TestContext : LeanwareContext
    {
        public TestContext()
        {
        }

        public TestContext(DbContextOptions<LeanwareContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source=test.db");
        }
    }
}