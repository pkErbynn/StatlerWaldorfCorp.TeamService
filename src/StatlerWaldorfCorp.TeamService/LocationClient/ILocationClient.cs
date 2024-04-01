using System;
using StatlerWaldorfCorp.TeamService.Models;

namespace StatlerWaldorfCorp.TeamService.LocationClient
{
	public interface ILocationClient
	{
		Task<Location> GetLatestLocationForMemberAsync(Guid memberId);
        Task<Location> AddLocationAsync(Guid memberId, Location locationRecord);
    }
}

