using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	public sealed class Ip64ClientData : IIpV6Client
	{
		public string IpV6 { get; }

		public ICity City { get; }

		public ICounty County { get; }

		public IContinent Continent { get; }

		public ILocation Location { get; }

		public Ip64ClientData(
			string ipV6,
			ICity city,
			ICounty county,
			IContinent continent,
			ILocation location)
		{
			IpV6      = ipV6;
			City      = city;
			County    = county;
			Continent = continent;
			Location  = location;
		}
	}
}
