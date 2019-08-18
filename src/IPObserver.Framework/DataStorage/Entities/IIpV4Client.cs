using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	public interface IIpV4Client
	{
		string IpV4 { get; }

		ICity City { get; }

		ICounty County { get; }

		IContinent Continent { get; }
	}
}
