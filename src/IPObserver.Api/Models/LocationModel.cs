using IPObserver.DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPObserver.Api.Models
{
	public class LocationModel
	{
		public int? AccuracyRadius { get; set; }

		public double? Latitude { get; set; }

		public double? Longitude { get; set; }

		public string TimeZone { get; set; }
	}
}
