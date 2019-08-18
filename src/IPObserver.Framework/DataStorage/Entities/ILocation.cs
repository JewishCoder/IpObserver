using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	public interface ILocation
	{
		int? AccuracyRadius { get; }

		double? Latitude { get; }

		double? Longitude { get; }

		string TimeZone { get; }
	}
}
