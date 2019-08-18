using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	public interface IIpV6Client
	{
		string IpV6 { get; }

		ICity City { get; }

		ICounty County { get; }

		IContinent Continent { get; }
	}
}
