using System;
using Microsoft.AspNetCore.Mvc;
using StatlerWaldorfCorp.TeamService.LocationClient;
using StatlerWaldorfCorp.TeamService.Models;
using StatlerWaldorfCorp.TeamService.Persistence;

namespace StatlerWaldorfCorp.TeamService.Controllers
{
    [Route("/teams/{teamId}/[controller]")]
    public class MembersController : Controller
    {
        private ITeamRepository teamRepository;
        private ILocationClient locationClient;

        public MembersController(ITeamRepository teamRepository, ILocationClient locationClient)
        {
            this.teamRepository = teamRepository;
            this.locationClient = locationClient;
        }

        [HttpGet]
        public virtual IActionResult GetMembers(Guid teamId)
        {
            Team team = teamRepository.Get(teamId);

            if (team == null)
            {
                return this.NotFound();
            }

            return this.Ok(team.Members);
        }

        [HttpGet]
        [Route("/teams/{teamId}/[controller]/{memberId}")]
        public async virtual Task<IActionResult> GetMemberAsync(Guid teamId, Guid memberId)
        {
            Team team = teamRepository.Get(teamId);

            if (team == null)
            {
                return this.NotFound();
            }

            var members = team.Members.Where(m => m.Id == memberId);

            if (members.Count() < 1)
            {
                return this.NotFound();
            }

            Member member = (Member)members.First();

            return this.Ok(new LocatedMember
            {
                Id = member.Id,
                FirstName = member.FirstName,
                LastName = member.LastName,
                LastLocation = await this.locationClient.GetLatestLocationForMemberAsync(member.Id)
            });

        }

        [HttpGet]
        [Route("/members/{memberId}/team")]
        public IActionResult GetMemberTeamId(Guid memberId)
        {
            Guid result = this.GetTeamIdForMember(memberId);

            if (result == Guid.Empty)
            {
                return this.NotFound();

            }

            return this.Ok(new
            {
                TeamID = result
            });
        }

        [HttpPut]
        [Route("/teams/{teamId}/[controller]/{memberId}")]
        public virtual IActionResult UpdateMember([FromBody] Member updatedMember, Guid teamID, Guid memberId)
        {
            Team team = teamRepository.Get(teamID);

            if (team == null)
            {
                return this.NotFound();
            }
           
            var matchedMembers = team.Members.Where(m => m.Id == memberId);

            if (matchedMembers.Count() < 1)
            {
                return this.NotFound();
            }
            else
            {
                team.Members.Remove(matchedMembers.First());
                team.Members.Add(updatedMember);
                this.teamRepository.Update(team);
                return this.Ok();
            }
            
        }


        [HttpPost]
        public virtual IActionResult CreateMember([FromBody] Member newMember, [FromRoute] Guid teamId)
        {
            Team team = teamRepository.Get(teamId);

            if (team == null)
            {
                return this.NotFound();
            }
            else
            {
                team.Members.Add(newMember);
                this.teamRepository.Update(team);
                var teamMember = new { TeamID = team.Id, MemberID = newMember.Id };
                return this.Created($"/teams/{teamMember.TeamID}/[controller]/{teamMember.MemberID}", teamMember);
            }
        }

        private Guid GetTeamIdForMember(Guid memberId)
        {
            foreach (var team in teamRepository.List())
            {
                var member = team.Members.FirstOrDefault(m => m.Id == memberId);
                if (member != null)
                {
                    return team.Id;
                }
            }
            return Guid.Empty;
        }
    }
}

