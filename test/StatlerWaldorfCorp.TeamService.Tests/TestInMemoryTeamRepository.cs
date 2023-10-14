using System;
using StatlerWaldorfCorp.TeamService.Models;
using StatlerWaldorfCorp.TeamService.Persistence;

namespace StatlerWaldorfCorp.TeamService.Tests
{
	public class TestInMemoryTeamRepository : InMemoryTeamRepository
    {
		public TestInMemoryTeamRepository() : base(CreateInitialFake())
		{
		}

		private static ICollection<Team> CreateInitialFake()
		{
			var teams = new List<Team>();
			teams.Add(new Team("one"));
			teams.Add(new Team("two"));

            return teams;
		}
	}
}

