using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IPObserver.DataStorage
{
	public interface IReadOnlyRepository<TEntityImpl, TFilter>
	{
		int Count(TFilter filter = default);

		Task<int> CountAsync(TFilter filter = default, CancellationToken cancellationToken = default);

		IReadOnlyList<TEntityImpl> Fetch(TFilter filter = default);

		Task<IReadOnlyList<TEntityImpl>> FetchAsync(TFilter filter = default, CancellationToken cancellation = default);
	}
}
