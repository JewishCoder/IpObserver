using System;
using System.Linq.Expressions;

namespace IPObserver.DataStorage
{
	/// <summary>Строитель фильтра.</summary>
	/// <typeparam name="T">Тип фильтра.</typeparam>
	public interface IFilterBuilder<T>
	{
		/// <summary>Создает фильтр.</summary>
		/// <param name="expression">Ламбда выражение.</param>
		/// <returns>Возвращает фильтр.</returns>
		IFilter<T> Create(Expression<Func<T, bool>> expression);
	}
}
