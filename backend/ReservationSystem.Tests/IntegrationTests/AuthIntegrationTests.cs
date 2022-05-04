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
    public class AuthIntegrationTests : IClassFixture<TestsWebApplicationFactory<Startup>>
    {
        private readonly HttpClient httpClient;

        public AuthIntegrationTests(TestsWebApplicationFactory<Startup> factory)
        {
            var scope = factory.Services.GetService<IServiceScopeFactory>()!.CreateScope();
            var context = scope.ServiceProvider.GetService<ReservationDbContext>();
            Utilities.InitializeDbForTests(context!);
            httpClient = factory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
        }
        
        [Fact]
        public async Task Should_Login()
        {
            var response = await httpClient.PostAsJsonAsync("/auth/login", new LoginDto
            {
                Email = AuthConstants.DefaultStudentUserEmail,
                
            });
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        
        [Fact]
        public async Task Should_Create_User()
        {
            var response = await httpClient.PostAsJsonAsync("/auth/create", new CreateUserDto
            {
                EmailAddress = "test@email.com",
                Password = "Password123",
                Name = "Name",
                Surname = "Surname",
                Role = "Student",
                TelephoneNumber = "+37061111111"
            });
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}