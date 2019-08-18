using System;
using System.Linq.Expressions;

namespace IPObserver.DataStorage
{
	public sealed class FilterBuilder<T> : IFilterBuilder<T>
	{
		private IFilter<T> _cacheFilter;

		public FilterBuilder()
		{
		}

		public IFilter<T> Create(Expression<Func<T, bool>> expression)
		{
			if(_cacheFilter == null)
			{
				_cacheFilter = new BaseFilter<T>(expression);
				return _cacheFilter;
			}

			var oldExpressiong = _cacheFilter.Expression;
			//Склеивание двух фильтров через оператор AND
			var newExpressionBody = Expression.AndAlso(oldExpressiong.Body, expression.Body);
			//Получение нового фильтра
			var newExpression = Expression.Lambda<Func<T, bool>>(newExpressionBody, oldExpressiong.Parameters);

			var conveter = new LambdaParameterPeplacer();
			conveter.ReplacmentParameters.AddRange(oldExpressiong.Parameters);

			//Исправление параметров, если параметры фильтра разные, то заменяются на правильные.
			var correctedExpression = (Expression<Func<T, bool>>)conveter.Visit(newExpression);

			_cacheFilter = new BaseFilter<T>(correctedExpression);
			return _cacheFilter;
		}

		public void Reset()
		{
			_cacheFilter = null;
		}
	}
}
