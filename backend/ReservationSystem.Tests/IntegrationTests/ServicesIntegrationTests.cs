using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ReservationSystem.DataAccess;
using ReservationSystem.DataAccess.Enums;
using ReservationSystem.Shared.Contracts.Dtos;
using ReservationSystem.Tests.Constants;
using ReservationSystem.Tests.Helpers;
using Xunit;

namespace ReservationSystem.Tests.IntegrationTests
{
    public class ServicesIntegrationTests : IClassFixture<TestsWebApplicationFactory<Startup>>
    {
        private readonly HttpClient httpClient;

        public ServicesIntegrationTests(TestsWebApplicationFactory<Startup> factory)
        {
            var scope = factory.Services.GetService<IServiceScopeFactory>()?.CreateScope();
            var context = scope?.ServiceProvider.GetService<ReservationDbContext>();
            if (context != null) Utilities.InitializeDbForTests(context);
            httpClient = factory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
        }
        
        [Fact]
        public async Task Should_Get_Services()
        {
            var response = await httpClient.GetAsync("/services");
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task Should_Get_Service()
        {
            var response = await httpClient.GetAsync($"/services/{AuthConstants.DefaultServiceId}");
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task Should_Get_Service_Types()
        {
            var response = await httpClient.GetAsync("/service-types");
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task Should_Create_Service()
        {
            var response = await httpClient.PostAsJsonAsync("/services", new CreateUpdateServiceDto
            {
                Dormitory = AuthConstants.DefaultDormitoryId,
                Room = AuthConstants.DefaultRoomId,
                MaxTimeOfUse = 20,
                Name = "Service",
                Type = ServiceType.Basketball.ToString(),
            });
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
        
        [Fact]
        public async Task Should_Update_Service()
        {
            var response = await httpClient.PutAsJsonAsync($"/services/{AuthConstants.DefaultServiceId}", new CreateUpdateServiceDto
            {
                Dormitory = AuthConstants.DefaultDormitoryId,
                MaxTimeOfUse = 20,
                Room = AuthConstants.DefaultRoomId,
                Name = "Service",
                Type = ServiceType.Basketball.ToString(),
            });
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}