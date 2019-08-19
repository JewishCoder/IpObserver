using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	public interface IContinentsRepository :
		IReadOnlyRepository<IContinent, ContinentFilter>,
		IWriteableRepository<IContinent, ContinentFilter,IContinent>, 
		IRemovableRepository<ContinentFilter>
	{
	}
}
