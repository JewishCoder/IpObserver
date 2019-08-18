using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace IPObserver.DataStorage
{
	public class IpV4ClientFilter : BaseFilter<IIpV4Client>
	{
		public IpV4ClientFilter(Expression<Func<IIpV4Client, bool>> expression) : base(expression)
		{
		}
	}
}
