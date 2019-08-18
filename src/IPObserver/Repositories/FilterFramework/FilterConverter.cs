using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace IPObserver.DataStorage
{
	public sealed class FilterConverter
	{
		/// <summary>Преобразует лямбда выражение из одного типа в другой.</summary>
		/// <typeparam name="TSource">Тип исходного выражения.</typeparam>
		/// <typeparam name="TTarger">Тип в который нужно преобразовать.</typeparam>
		/// <param name="expression">Исходное выражение.</param>
		/// <returns>Возвращает преобразованное выражение.</returns>
		/// <exception cref="InvalidCastException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		public static Expression<Func<TTarger, bool>> Convert<TSource, TTarger>(Expression<Func<TSource, bool>> expression)
		{
			Verify.Argument.IsNotNull(expression, nameof(expression));

			var conveter = new LambdaConverter();
			conveter.TypeTransformation.Add(typeof(TSource), typeof(TTarger));
			return (Expression<Func<TTarger, bool>>)conveter.Visit(expression);
		}

		/// <summary>Преобразует лямбда выражение из одного типа в другой.</summary>
		/// <typeparam name="TSource">Тип исходного выражения.</typeparam>
		/// <typeparam name="TTarger">Тип в который нужно преобразовать.</typeparam>
		/// <param name="expression">Исходное выражение.</param>
		/// <param name="childsMap">
		///		Словарь дочерних типов для преобразования.
		///		Key - исходный тип.
		///		Value - в который нужно преобразовать.
		/// </param>
		/// <returns>Возвращает преобразованное выражение.</returns>
		/// <exception cref="InvalidCastException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		public static Expression<Func<TTarger, bool>> Convert<TSource, TTarger>(Expression<Func<TSource, bool>> expression, Dictionary<Type, Type> childsMap)
		{
			Verify.Argument.IsNotNull(expression, nameof(expression));
			Verify.Argument.IsNotNull(childsMap, nameof(childsMap));

			if(!childsMap.ContainsKey(typeof(TSource)))
			{
				childsMap.Add(typeof(TSource), typeof(TTarger));
			}
			var conveter = new LambdaConverter();
			conveter.TypeTransformation = childsMap;
			return (Expression<Func<TTarger, bool>>)conveter.Visit(expression);
		}
	}
}
