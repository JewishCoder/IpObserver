using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace IPObserver.DataStorage
{
	public class IpV6ClientFilter : BaseFilter<IIpV6Client>
	{
		public IpV6ClientFilter(Expression<Func<IIpV6Client, bool>> expression) : base(expression)
		{
		}
	}
}
