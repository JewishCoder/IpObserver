using System;
using System.Linq.Expressions;

namespace IPObserver.DataStorage
{
	public class CityFilter : BaseFilter<ICity>
	{
		public CityFilter(string name = null) : base(BuildFilter(name))
		{

		}

		public CityFilter(Expression<Func<ICity, bool>> expression) : base(expression)
		{

		}

		private static Expression<Func<ICity, bool>> BuildFilter(string name)
		{
			var result = default(Expression<Func<ICity, bool>>);
			if(!string.IsNullOrWhiteSpace(name))
			{
				result = x => x.Name.Equals(name);
			}

			return result;
		}
	}
}
