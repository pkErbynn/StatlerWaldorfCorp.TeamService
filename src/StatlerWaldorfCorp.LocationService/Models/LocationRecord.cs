﻿using System;
namespace StatlerWaldorfCorp.LocationService.Models
{
	public class LocationRecord
	{
		public Guid Id { get; set; }
		public float Latitude { get; set; }
		public float Longitude { get; set; }
		public float Altitude { get; set; }
		public long Timestamp { get; set; }
		public Guid MemberId { get; set; }
	}
}

