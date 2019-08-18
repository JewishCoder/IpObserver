using System.Collections.Generic;

namespace IPObserver.DataStorage
{
	public sealed class CountyImpl : ICounty
	{
		public string Name { get; }

		public string Code { get; }

		public IContinent Continent { get; }

		public IReadOnlyList<ICity> Cities { get; }

		public CountyImpl(
			string name, 
			string code, 
			IContinent continent,
			IReadOnlyList<ICity> cities)
		{
			Name = name;
			Code = code;
			Continent = continent;
			Cities = cities ?? new List<ICity>();
		}
	}
}
