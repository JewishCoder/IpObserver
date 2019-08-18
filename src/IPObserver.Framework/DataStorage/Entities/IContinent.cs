using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	public interface IContinent
	{
		string Name { get; }

		string Code { get; }

		IReadOnlyList<ICounty> Counties { get; }
	}
}
