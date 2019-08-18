using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	public interface ICounty
	{
		string Name { get; }

		string Code { get; }

		IContinent Continent { get; }

		IReadOnlyList<ICity> Cities { get; }
	}
}
