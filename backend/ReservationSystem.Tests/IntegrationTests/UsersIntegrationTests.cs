using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ReservationSystem.DataAccess;
using ReservationSystem.Shared.Contracts.Dtos;
using ReservationSystem.Tests.Constants;
using ReservationSystem.Tests.Helpers;
using Xunit;

namespace ReservationSystem.Tests.IntegrationTests
{
    public class UsersIntegrationTests : IClassFixture<TestsWebApplicationFactory<Startup>>
    {
        private readonly HttpClient httpClient;

        public UsersIntegrationTests(TestsWebApplicationFactory<Startup> factory)
        {
            var scope = factory.Services.GetService<IServiceScopeFactory>()!.CreateScope();
            var context = scope.ServiceProvider.GetService<ReservationDbContext>();
            Utilities.InitializeDbForTests(context!);
            httpClient = factory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
        }
        
        [Fact]
        public async Task Should_Get_Users()
        {
            var response = await httpClient.GetAsync("/users");
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task Should_Managers_Lookup()
        {
            var response = await httpClient.GetAsync("/managers-lookup");
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task Should_Roles_Lookup()
        {
            var response = await httpClient.GetAsync("/roles-lookup");
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task Should_Ban_User_From_Reserving()
        {
            var response = await httpClient.PostAsync($"/users/{AuthConstants.DefaultStudentUserId}/ban", null);
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
        
        [Fact]
        public async Task Should_Remove_Reservation_Ban()
        {
            var response = await httpClient.PostAsync($"/users/{AuthConstants.DefaultStudentUserId}/remove-ban", null);
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
        
        [Fact]
        public async Task Should_Not_Send_Email_If_Recipient_Is_Invalid()
        {
            var response = await httpClient.PostAsJsonAsync($"/users/send-email", new EmailDto
            {
                Body = "Body",
                Recipient = "test@email.com",
                Subject = "Subject"
            });
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}