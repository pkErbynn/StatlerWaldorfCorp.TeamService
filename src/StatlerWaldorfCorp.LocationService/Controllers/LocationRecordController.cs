﻿using System;
using Microsoft.AspNetCore.Mvc;
using StatlerWaldorfCorp.LocationService.Models;

namespace StatlerWaldorfCorp.LocationService.Controllers
{
	[Route("locations/{memberId}")]
	public class LocationRecordController: Controller
	{
        private ILocationRecordRepository locationRepository;

        public LocationRecordController(ILocationRecordRepository locationRecordRepository)
		{
			this.locationRepository = locationRecordRepository;
		}

		[HttpPost]
		public IActionResult AddLocation(Guid memberId,
			[FromBody] LocationRecord locationRecord)
		{
			locationRepository.Add(locationRecord);
			return this.Created($"/locations/{locationRecord.Id}", locationRecord);
		}
	}
}

