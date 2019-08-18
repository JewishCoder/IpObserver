using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	public sealed class ContinentData : IContinent
	{
		public string Name { get; }

		public string Code { get; }

		public IReadOnlyList<ICounty> Counties { get; }

		public ContinentData(string name, string code, IReadOnlyList<ICounty> counties)
		{
			Name = name;
			Code = code;
			Counties = Counties;
		}
	}
}
