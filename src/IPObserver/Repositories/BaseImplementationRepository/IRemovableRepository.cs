using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IPObserver.DataStorage
{
	public interface IRemovableRepository<TFilter>
	{
		int Remove(TFilter filter);

		Task<int> RemoveAsync(TFilter filter, CancellationToken cancellation);
	}
}
