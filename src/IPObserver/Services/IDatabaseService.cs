using IPObserver.DataStorage.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPObserver.DataStorage
{
	public interface IDatabaseService
	{
		IDatabaseProvider DatabaseProvider { get; }

		StorageContext CreateContext();

		Task<StorageContext> CreateContextAsync();
	}
}
