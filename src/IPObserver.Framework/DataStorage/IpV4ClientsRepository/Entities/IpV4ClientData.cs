using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	public sealed class IpV4ClientData : IIpV4Client
	{
		public string IpV4 { get; }

		public ICity City { get; }

		public ICounty County { get; }

		public IContinent Continent { get; }

		public ILocation Location { get; }

		public IpV4ClientData(
			string ipV4,
			ICity city,
			ICounty county,
			IContinent continent,
			ILocation location)
		{
			IpV4      = ipV4;
			City      = city;
			County    = county;
			Continent = continent;
			Location  = location;
		}
	}
}
