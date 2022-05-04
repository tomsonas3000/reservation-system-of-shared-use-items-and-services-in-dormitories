using System;
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
    public class ReservationsIntegrationTests: IClassFixture<TestsWebApplicationFactory<Startup>>
    {
        private readonly HttpClient httpClient;

        public ReservationsIntegrationTests(TestsWebApplicationFactory<Startup> factory)
        {
            var scope = factory.Services.GetService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetService<ReservationDbContext>();
            Utilities.InitializeDbForTests(context);
            httpClient = factory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
        }

        [Fact]
        public async Task Should_Get_Reservations()
        {
            var response = await httpClient.GetAsync("/reservations");
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Get_Reservations_For_Calendar()
        {
            var response = await httpClient.GetAsync("/reservations/calendar");
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Should_Create_Reservation()
        {
            var response = await httpClient.PostAsJsonAsync("/reservations", new CreateReservationDto
            {
                StartDate = DateTime.Now.AddMinutes(5).ToString(),
                EndDate = DateTime.Now.AddMinutes(15).ToString(),
                ServiceId = AuthConstants.DefaultServiceId,
                
            });
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Should_Update_Reservation()
        {
            var response = await httpClient.PutAsJsonAsync($"/reservations/{AuthConstants.DefaultReservationId}", new UpdateReservationDto
            {
                StartDate = DateTime.Now.AddMinutes(5).ToString(),
                EndDate = DateTime.Now.AddMinutes(15).ToString(),
                
            });
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Should_Get_Dormitory_Reservations()
        {
            var response = await httpClient.GetAsync($"/dormitory-reservations/{AuthConstants.DefaultDormitoryId}");
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Get_User_Reservations()
        {
            var response = await httpClient.GetAsync($"/user-reservations/{AuthConstants.DefaultStudentUserId}");
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Get_Service_Reservations()
        {
            var response = await httpClient.GetAsync($"/service-reservations/{AuthConstants.DefaultServiceId}");
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Delete_Reservation()
        {
            var response = await httpClient.DeleteAsync($"/reservations/{AuthConstants.DefaultReservationId}");
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}