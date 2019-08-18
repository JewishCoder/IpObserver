using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace IPObserver.DataStorage
{
	public class CountyFilter : BaseFilter<ICounty>
	{
		public CountyFilter(string name = null, string code = null) :base(BuildFilter(name, code))
		{

		}

		public CountyFilter(Expression<Func<ICounty, bool>> expression) : base(expression)
		{
		}

		private static Expression<Func<ICounty, bool>> BuildFilter(string name, string code)
		{
			var result = default(Expression<Func<ICounty, bool>>);

			var filterBuilder = new FilterBuilder<ICounty>();
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
