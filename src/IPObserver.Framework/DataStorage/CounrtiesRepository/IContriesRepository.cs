using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	public interface ICountriesRepository:
		IReadOnlyRepository<ICounty, CountyFilter>,
		IWriteableRepository<ICounty, CountyFilter, ICounty>, 
		IRemovableRepository<CountyFilter>
	{
	}
}
