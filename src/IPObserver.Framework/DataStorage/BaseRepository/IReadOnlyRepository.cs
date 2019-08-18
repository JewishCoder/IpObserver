using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IPObserver.DataStorage
{
	public interface IReadOnlyRepository<TEntityImpl, TFilter>
	{
		int Count(TFilter filter);

		Task<int> CountAsync(TFilter filter, CancellationToken cancellationToken);

		IReadOnlyList<TEntityImpl> Fetch(TFilter filter);

		Task<IReadOnlyList<TEntityImpl>> FetchAsync(TFilter filter, CancellationToken cancellation);
	}
}
