using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	public sealed class CityImpl : ICity
	{
		public string Name { get; }

		public ICounty County { get; }

		public CityImpl(string name, ICounty county)
		{
			Name   = name;
			County = county;
		}
	}
}
