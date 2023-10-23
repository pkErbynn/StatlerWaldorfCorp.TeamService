using System;
using StatlerWaldorfCorp.TeamService.Models;

namespace StatlerWaldorfCorp.TeamService.Persistence
{
	public class InMemoryTeamRepository: ITeamRepository
	{
        protected static ICollection<Team> teams; // private static pke??, code smell, just for test

        public InMemoryTeamRepository()
        {
            if (teams == null)
            {
                teams = new List<Team>();
            }
        }

        public InMemoryTeamRepository(ICollection<Team> teamList)
        {
            teams = teamList ?? new List<Team>();
        }

        public IEnumerable<Team> List()
        {
            return teams;
        }

        public Team Get(Guid id)
        {
            return teams.FirstOrDefault(t => t.Id == id);
        }

        public Team Update(Team t)
        {
            if (t == null) return null;

            Team team = this.Delete(t.Id);
            if (team != null)
            {
                team = this.Add(t);
            }
            return team;
        }

        public Team Add(Team team)
        {
            if (team == null) return null;

            teams.Add(team);
            return team;
        }

        public Team Delete(Guid id)
        {
            var t = teams.Where(x => x.Id == id);
            Team team = null;

            if (t.Count() > 0)
            {
                team = t.First();
                teams.Remove(team);
            }
            return team;
        }
    }
}

