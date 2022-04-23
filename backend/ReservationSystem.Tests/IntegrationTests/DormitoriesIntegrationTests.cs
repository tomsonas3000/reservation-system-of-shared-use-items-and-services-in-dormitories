using System;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
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
        private readonly TestsWebApplicationFactory<Startup> factory;

        public DormitoriesIntegrationTests(TestsWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task Should_Get_Dormitories_Lookup_List()
        {
            var httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions { BaseAddress = new Uri("http://localhost:5000")});
            
            httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Test");

            var response = await httpClient.GetAsync("/dormitories-lookup");
            var responseContentString = await response.Content.ReadAsStringAsync();
            var responseContent = JsonSerializer.Deserialize<Dormitory[]>(responseContentString);
        }
    }
}