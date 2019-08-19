using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IPObserver.DataStorage
{
	public interface IWriteableRepository<TEntityImpl, TFilter, TData>
	{
		TEntityImpl Add(TData data);

		Task<TEntityImpl> AddAsync(TData data, CancellationToken cancellation = default);
	}
}
