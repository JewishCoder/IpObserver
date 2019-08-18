using System;
using System.Linq.Expressions;


namespace IPObserver.DataStorage
{
	public class BaseFilter<T> : IFilter<T>
	{
		public Func<T, bool> Predicate { get; protected set; }

		public Expression<Func<T, bool>> Expression { get; protected set; }

		public BaseFilter(Expression<Func<T, bool>> expression)
		{
			Expression = expression;
			Predicate = expression?.Compile();
		}
	}
}
