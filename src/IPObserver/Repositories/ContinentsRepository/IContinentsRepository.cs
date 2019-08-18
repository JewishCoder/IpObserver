using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	public interface IContinentsRepository : IWriteableRepository<IContinent,ContinentFilter,IContinent>, IRemovableRepository<ContinentFilter>
	{
	}
}
