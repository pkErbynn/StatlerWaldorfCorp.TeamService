//using System;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Microsoft.VisualStudio.TestPlatform.TestHost;

//namespace StatlerWaldorfCorp.TeamService.Tests.Integration
//{
//    public class IntegrationTest : IClassFixture<WebApplicationFactory<Program>>
//    {
//        private readonly WebApplicationFactory<Program> _factory;
//        private readonly HttpClient _client;

//        private IntegrationTest(WebApplicationFactory<Program> factory)
//        {
//            _factory = factory;
//            _client = _factory.CreateClient();
//        }

//        [Fact]
//        public async Task TestGetEndpoint()
//        {
//            // Arrange (if necessary)

//            // Act
//            var response = await _client.GetAsync("/teams");


//            // Assert
//            response.EnsureSuccessStatusCode()
//            //Assert.Equal("200", response.EnsureSuccessStatusCode().StatusCode)
//            ; // Ensure a 2xx status code
//        }

//    }
//}

