using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	public interface ICity
	{
		string Name { get; }

		public ICounty County { get; }
	}
}
