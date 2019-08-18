using System;
using System.Linq.Expressions;

namespace IPObserver.DataStorage
{
	/// <summary>Фильтр.</summary>
	/// <typeparam name="T">Тип фильтра</typeparam>
	public interface IFilter<T>
	{
		/// <summary>Предикат фильтра.</summary>
		Func<T, bool> Predicate { get; }

		/// <summary>Лямбда выражения фильтра.</summary>
		Expression<Func<T, bool>> Expression { get; }
	}
}
