using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	public sealed class LocationData : ILocation
	{
		public int? AccuracyRadius { get; }

		public double? Latitude { get; }

		public double? Longitude { get; }

		public string TimeZone { get; }

		public LocationData(
			int? accuracyRadius,
			double? latitude,
			double? logitude,
			string timeZone)
		{
			AccuracyRadius = accuracyRadius;
			Latitude = latitude;
			Longitude = logitude;
			TimeZone = timeZone;
		}
	}
}
