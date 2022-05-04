using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using ReservationSystem.DataAccess.Entities;
using ReservationSystem.Tests.Helpers;
using Xunit;

namespace ReservationSystem.Tests.IntegrationTests
{
    public class DormitoriesIntegrationTests : IClassFixture<TestsWebApplicationFactory<Startup>>
    {
        private readonly HttpClient httpClient;

        public DormitoriesIntegrationTests(TestsWebApplicationFactory<Startup> factory)
        {
            httpClient = factory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
        }

        [Fact]
        public async Task Should_Get_Dormitories_Lookup_List()
        {
            var response = await httpClient.GetAsync("/dormitories-lookup");
            var responseContentString = await response.Content.ReadAsStringAsync();
            var responseContent = JsonSerializer.Deserialize<Dormitory[]>(responseContentString);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}