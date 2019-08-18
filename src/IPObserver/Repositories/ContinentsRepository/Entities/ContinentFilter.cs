using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace IPObserver.DataStorage
{
	public class ContinentFilter : BaseFilter<IContinent>
	{
		public ContinentFilter(string name = null, string code = null) : base(BuildFilter(name, code))
		{

		}

		public ContinentFilter(Expression<Func<IContinent, bool>> expression) : base(expression)
		{
		}

		private static Expression<Func<IContinent, bool>> BuildFilter(string name, string code)
		{
			var result = default(Expression<Func<IContinent, bool>>);

			var filterBuilder = new FilterBuilder<IContinent>();
			if(!string.IsNullOrWhiteSpace(name))
			{
				result = filterBuilder.Create(x => x.Name.Equals(name)).Expression;
			}
			if(!string.IsNullOrWhiteSpace(code))
			{
				result = filterBuilder.Create(x => x.Code.Equals(code)).Expression;
			}

			return result;
		}
	}
}
