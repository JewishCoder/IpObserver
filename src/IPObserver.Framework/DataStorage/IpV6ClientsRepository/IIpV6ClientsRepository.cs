using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	public interface IIpV6ClientsRepository :
		IReadOnlyRepository<IIpV6Client, IpV6ClientFilter>,
		IWriteableRepository<IIpV6Client, IpV6ClientFilter, IIpV6Client>, 
		IRemovableRepository<IpV6ClientFilter>
	{
	}
}
