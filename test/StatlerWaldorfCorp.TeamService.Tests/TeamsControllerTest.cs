using System;
using Microsoft.AspNetCore.Mvc;
using StatlerWaldorfCorp.TeamService.Controllers;
using StatlerWaldorfCorp.TeamService.Models;

namespace StatlerWaldorfCorp.TeamService.Tests
{
	public class TeamsControllerTest
	{
		[Fact]
		public async Task QueryTeamList_ReturnsCorrectTeams()
		{
			TeamsController teamsController = new TeamsController(new TestInMemoryTeamRepository());

			var teamsObjectResult = teamsController.GetAllTeams() as ObjectResult;
            var teamsValue = (IEnumerable<Team>)(teamsObjectResult).Value;
            List<Team> teams = new List<Team>(teamsValue);

			Assert.Equal(2, teams.Count);
			Assert.Equal("one", teams[0].Name);
			Assert.Equal("two", teams[1].Name);
        }

        [Fact]
        public void GetTeam_RetrievesTeam()
        {
            TeamsController controller = new TeamsController(new TestInMemoryTeamRepository());

            string sampleName = "sample";
            Guid id = Guid.NewGuid();
            Team sampleTeam = new Team(sampleName, id);
            controller.CreateTeam(sampleTeam);

            Team retrievedTeam = (Team)(controller.GetTeam(id) as ObjectResult).Value;
            Assert.Equal(retrievedTeam.Name, sampleName);
            Assert.Equal(retrievedTeam.Id, id);
        }


        [Fact]
        public void GetNonExistentTeamReturnsNotFound()
        {
            TeamsController controller = new TeamsController(new TestInMemoryTeamRepository());

            Guid id = Guid.NewGuid();
            var result = controller.GetTeam(id);
            Assert.True(result is NotFoundResult);
        }

        [Fact]
		public async void CreateTeam_AddsToTeamList()
		{
            TeamsController teamsController = new TeamsController(new TestInMemoryTeamRepository());

            var teams = (IEnumerable<Team>)(teamsController.GetAllTeams() as ObjectResult).Value;
			List<Team> originalTeams = new List<Team>(teams);

			Team newTeam = new Team("sample");
			var result = teamsController.CreateTeam(newTeam);
            //TODO: also assert that the destination URL of the new team reflects the team's GUID
            Assert.Equal(201, (result as ObjectResult).StatusCode);

			var newTeams = (IEnumerable<Team>)(teamsController.GetAllTeams() as ObjectResult).Value;
			List<Team> newTeamList = new List<Team>(newTeams);

			Assert.Equal(originalTeams.Count + 1, newTeamList.Count);

			var sampleTeam = newTeamList.FirstOrDefault(target => target.Name == "sample");
			Assert.NotNull(sampleTeam);
		}

        [Fact]
        public void UpdateTeam_ModifiesTeamToList()
        {
            TeamsController controller = new TeamsController(new TestInMemoryTeamRepository());
            var teams = (IEnumerable<Team>)(controller.GetAllTeams() as ObjectResult).Value;
            List<Team> original = new List<Team>(teams);

            Guid id = Guid.NewGuid();
            Team t = new Team("sample", id);
            var result = controller.CreateTeam(t);

            Team newTeam = new Team("sample2", id);
            controller.UpdateTeam(newTeam, id);

            var newTeamsRaw = (IEnumerable<Team>)(controller.GetAllTeams() as ObjectResult).Value;
            List<Team> newTeams = new List<Team>(newTeamsRaw);
            var sampleTeam = newTeams.FirstOrDefault(target => target.Name == "sample");
            Assert.Null(sampleTeam);

            Team retrievedTeam = (Team)(controller.GetTeam(id) as ObjectResult).Value;
            Assert.Equal(retrievedTeam.Name, "sample2");
        }

        [Fact]
        public void UpdateNonExistentTeam_ReturnsNotFound()
        {
            TeamsController controller = new TeamsController(new TestInMemoryTeamRepository());
            var teams = (IEnumerable<Team>)(controller.GetAllTeams() as ObjectResult).Value;
            List<Team> original = new List<Team>(teams);

            Team someTeam = new Team("Some Team", Guid.NewGuid());
            controller.CreateTeam(someTeam);

            Guid newTeamId = Guid.NewGuid();
            Team newTeam = new Team("New Team", newTeamId);
            var result = controller.UpdateTeam(newTeam, newTeamId);

            Assert.True(result is NotFoundResult);  // same as: Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteTeam_RemovesFromList()
        {
            TeamsController controller = new TeamsController(new TestInMemoryTeamRepository());
            var teams = (IEnumerable<Team>)(controller.GetAllTeams() as ObjectResult).Value;
            int ct = teams.Count();

            string sampleName = "sample";
            Guid id = Guid.NewGuid();
            Team sampleTeam = new Team(sampleName, id);
            controller.CreateTeam(sampleTeam);

            teams = (IEnumerable<Team>)(controller.GetAllTeams() as ObjectResult).Value;
            sampleTeam = teams.FirstOrDefault(target => target.Name == sampleName);
            Assert.NotNull(sampleTeam);

            controller.DeleteTeam(id);

            teams = (IEnumerable<Team>)(controller.GetAllTeams() as ObjectResult).Value;
            sampleTeam = teams.FirstOrDefault(target => target.Name == sampleName);
            Assert.Null(sampleTeam);
        }

        [Fact]
        public void DeleteNonExistentTeam_ReturnsNotFound()
        {
            TeamsController controller = new TeamsController(new TestInMemoryTeamRepository());
            Guid id = Guid.NewGuid();

            var result = controller.DeleteTeam(id);
            Assert.True(result is NotFoundResult);
        }
    }
}

