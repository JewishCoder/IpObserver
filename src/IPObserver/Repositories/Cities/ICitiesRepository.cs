using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	public interface ICitiesRepository : IWriteableRepository<ICity, CityFilter, ICity>, IRemovableRepository<CityFilter>
	{
	}
}
