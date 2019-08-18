using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	/// <summary>Контекст представления записей в рамках одного запроса.</summary>
	public interface IRepresentationContext
	{
		/// <summary>Возвращает закэшированную сущность или создает ее.</summary>
		/// <typeparam name="T">Тип сущности.</typeparam>
		/// <param name="id">Идентификатор сущности.</param>
		/// <param name="factory">Фабрика сущностей.</param>
		/// <returns>Сущность требуемого типа с указанным идентификатором.</returns>
		T GetOrAdd<T>(long id, Func<T> factory) where T : class;

		/// <summary>Пытается вернуть закэшированную сущность.</summary>
		/// <typeparam name="T">Тип сущности.</typeparam>
		/// <param name="id">Идентификатор сущности.</param>
		/// <returns>Сущность требуемого типа с указанным идентификатором или <c>null</c>.</returns>
		T TryGet<T>(long id) where T : class;
	}
}
