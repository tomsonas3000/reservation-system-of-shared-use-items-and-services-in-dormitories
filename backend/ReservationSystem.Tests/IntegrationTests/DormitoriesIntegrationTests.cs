using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using ReservationSystem.DataAccess;
using ReservationSystem.DataAccess.Entities;
using ReservationSystem.Shared.Contracts.Dtos;
using ReservationSystem.Tests.Constants;
using ReservationSystem.Tests.Helpers;
using Xunit;

namespace ReservationSystem.Tests.IntegrationTests
{
    public class DormitoriesIntegrationTests : IClassFixture<TestsWebApplicationFactory<Startup>>
    {
        private readonly HttpClient httpClient;

        public DormitoriesIntegrationTests(TestsWebApplicationFactory<Startup> factory)
        {
            var scope = factory.Services.GetService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetService<ReservationDbContext>();
            Utilities.InitializeDbForTests(context);
            httpClient = factory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
        }

        [Fact]
        public async Task Should_Get_Dormitories()
        {
            var response = await httpClient.GetAsync("/dormitories");
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Get_Dormitory()
        {
            var response = await httpClient.GetAsync($"/dormitories/{AuthConstants.DefaultDormitoryId}");
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Get_Dormitories_Lookup_List()
        {
            var response = await httpClient.GetAsync("/dormitories-lookup");
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Create_Dormitory()
        {
            var response = await httpClient.PostAsJsonAsync("/dormitories", new CreateUpdateDormitoryDto
            {
                Name = "Dorm",
                City = "Kaunas",
                Address = "Pasiles g. 37",
                Manager = AuthConstants.DefaultManagerUserId.ToString(),
                Rooms = new List<string>(),
            });
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Should_Update_Dormitory()
        {
            var response = await httpClient.PutAsJsonAsync($"/dormitories/{AuthConstants.DefaultDormitoryId}", new CreateUpdateDormitoryDto
            {
                Name = "Dorm",
                City = "Kaunas",
                Address = "Pasiles g. 37",
                Manager = AuthConstants.DefaultManagerUserId.ToString(),
                Rooms = new List<string>(),
            });
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Should_Add_Students_To_Dormitory()
        {
            var response = await httpClient.PostAsJsonAsync($"/dormitories/{AuthConstants.DefaultDormitoryId}/update-students", new List<Guid> { AuthConstants
            .DefaultStudentUserId });
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Should_Get_Students()
        {
            var response = await httpClient.GetAsync($"/dormitories/{AuthConstants.DefaultDormitoryId}/students");
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}