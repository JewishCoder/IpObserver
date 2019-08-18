using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	public sealed class CountyData : ICounty
	{
		public string Name { get; }

		public string Code { get; }

		public IContinent Continent { get; }

		public IReadOnlyList<ICity> Cities { get; }

		public CountyData(string name, string code, IContinent continent, IReadOnlyList<ICity> cities)
		{
			Name      = name;
			Code      = code;
			Continent = continent;
			Cities    = cities ?? new List<ICity>();
		}
	}
}
