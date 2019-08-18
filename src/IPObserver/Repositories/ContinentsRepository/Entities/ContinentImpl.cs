using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	public sealed class ContinentImpl : IContinent
	{
		public string Name { get; }

		public string Code { get; }

		public IReadOnlyList<ICounty> Counties { get; }

		public ContinentImpl(string name, string code, IReadOnlyList<ICounty> counties)
		{
			Name = name;
			Code = code;
			Counties = counties ?? new List<ICounty>();
		}
	}
}
