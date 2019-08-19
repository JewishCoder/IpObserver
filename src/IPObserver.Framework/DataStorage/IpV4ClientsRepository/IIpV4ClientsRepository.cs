using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	public interface IIpV4ClientsRepository :
		IReadOnlyRepository<IIpV4Client, IpV4ClientFilter>,
		IWriteableRepository<IIpV4Client, IpV4ClientFilter, IIpV4Client>, 
		IRemovableRepository<IpV4ClientFilter>
	{
	}
}
