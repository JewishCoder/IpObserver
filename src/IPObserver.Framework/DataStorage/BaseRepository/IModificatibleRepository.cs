using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IPObserver.DataStorage
{
	public interface IModificatibleRepository<TEntityImpl, TModificationData>
	{
		TEntityImpl Modification(TEntityImpl entity, TModificationData modification);

		Task<TEntityImpl> ModificationAsync(TEntityImpl entit, TModificationData modification, CancellationToken cancellation);
	}
}
