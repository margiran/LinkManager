using System.Collections.Immutable;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using LinkManagerApi.Commands;
using LinkManagerApi.Data;
using LinkManagerApi.Dtos;
using LinkManagerApi.Routes;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LinkManagerApi.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient client;

        protected IntegrationTest()
        {
            var webAppFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(Builder=>{
                    Builder.ConfigureServices( services =>{
                        services.RemoveAll(typeof(ApplicationDbContext));
                        services.AddDbContext<ApplicationDbContext>(options =>{
                            options.UseInMemoryDatabase("dbForTest");
                        });
                    });
                });
            client = webAppFactory.CreateClient();
        }

        protected async Task<LinkResponseDto> CreateALink(CreateLinkCommand command)
        {
           var response= await client.PostAsJsonAsync(ApiRoutes.Links.Create,command);
           return await response.Content.ReadFromJsonAsync<LinkResponseDto>() ?? new LinkResponseDto();
        }
    }
}
