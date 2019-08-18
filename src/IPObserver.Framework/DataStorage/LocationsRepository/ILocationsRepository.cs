using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	public interface ILocationsRepository : IWriteableRepository<ILocation,LocationFilter,ILocation>, IRemovableRepository<LocationFilter>
	{
	}
}
