using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace IPObserver.DataStorage
{
	public class LocationFilter : BaseFilter<ILocation>
	{
		public LocationFilter(Expression<Func<ILocation, bool>> expression) : base(expression)
		{
		}
	}
}
