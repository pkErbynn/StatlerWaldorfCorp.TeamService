using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using StatlerWaldorfCorp.TeamService.Models;

namespace StatlerWaldorfCorp.TeamService.Tests.Integration;

public class SimpleIntegrationTests
{
    //private readonly TestServer testServer;
    private readonly WebApplicationFactory<Program> factory;
    private readonly HttpClient testClient;

    private readonly Team zombieTeamPayload;

    public SimpleIntegrationTests()
    {
        factory = new WebApplicationFactory<Program>();
        testClient = factory.CreateDefaultClient();

        zombieTeamPayload = new Team()
        {
            Id = Guid.NewGuid(),
            Name = "Zombie"
        };
    }

    [Fact]
    public async Task TestTeamPostAndGet()
    {
        var teamStringContent = new StringContent(JsonConvert.SerializeObject(zombieTeamPayload), UnicodeEncoding.UTF8, "application/json");

        HttpResponseMessage postResponse = await testClient.PostAsync("/teams", teamStringContent);
        postResponse.EnsureSuccessStatusCode();

        var getResponse = await testClient.GetAsync("/teams");
        getResponse.EnsureSuccessStatusCode();

        string rawResponse = await getResponse.Content.ReadAsStringAsync();
        List<Team> teams = JsonConvert.DeserializeObject<List<Team>>(rawResponse);

        Assert.Single(teams);
        Assert.Equal("Zombie", teams[0].Name);
        Assert.Equal(zombieTeamPayload.Id, teams[0].Id);
    }
}
