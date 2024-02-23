using System;
using StatlerWaldorfCorp.TeamService.Models;

namespace StatlerWaldorfCorp.TeamService.LocationClient
{
	public class InMemoryLocationClient: ILocationClient
	{
        public Dictionary<Guid, SortedList<long, Location>> MemberLocationHistory { get; set; }

        public InMemoryLocationClient()
		{
            this.MemberLocationHistory = new Dictionary<Guid, SortedList<long, Location>>();
		}

        public async Task<Location> GetLatestLocationForMemberAsync(Guid memberId)
        {
            return await Task.Run(() =>
            {
                if (MemberLocationHistory.ContainsKey(memberId))
                {
                    return MemberLocationHistory[memberId].Values.LastOrDefault();
                }

                return null;
            });
        }

        public async Task<Location> AddLocationAsync(Guid memberId, Location locationRecord)
        {
            return await Task.Run(() =>
            {
                if (!MemberLocationHistory.ContainsKey(memberId))
                {
                    MemberLocationHistory.Add(memberId, new SortedList<long, Location>());
                }

                MemberLocationHistory[memberId].Add(locationRecord.Timestamp, locationRecord);

                return locationRecord;
            });
        }
    }
}

