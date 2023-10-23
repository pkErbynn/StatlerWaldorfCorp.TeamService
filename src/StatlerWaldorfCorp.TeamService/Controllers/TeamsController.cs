using System;
using Microsoft.AspNetCore.Mvc;
using StatlerWaldorfCorp.TeamService.Models;
using StatlerWaldorfCorp.TeamService.Persistence;

namespace StatlerWaldorfCorp.TeamService.Controllers
{
    [Route("[controller]")]
    public class TeamsController: Controller
	{
		private readonly ITeamRepository repository;

		public TeamsController(ITeamRepository repository)
		{
			this.repository = repository;
		}

		[HttpGet]
		public virtual IActionResult GetAllTeams()
		{
			return this.Ok(repository.List());
		}

        [HttpGet("{id}")]
        public IActionResult GetTeam(Guid id)
        {
            Team team = repository.Get(id);

            if (team != null)			  
            {
                return this.Ok(team);
            }

            return this.NotFound();
        }

        [HttpPost]
        public virtual IActionResult CreateTeam([FromBody]Team newTeam)
        {
			this.repository.Add(newTeam);

            return this.Created($"/teams/{newTeam.Id}", newTeam);
        }

        [HttpPut("{id}")]
        public virtual IActionResult UpdateTeam([FromBody] Team team, Guid id)
        {
            team.Id = id;   // nb

            if (repository.Update(team) == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(team);
            }
        }

        [HttpDelete("{id}")]
        public virtual IActionResult DeleteTeam(Guid id)
        {
            Team team = repository.Delete(id);

            if (team == null)
            {
                return this.Ok();
            }
            else
            {
                return this.Ok(team.Id);
            }
        }
    }
}

