using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	/// <summary>Представляемая сущность</summary>
	/// <typeparam name="T">Тип представления.</typeparam>
	public interface IRepresentable<out T>
	{
		/// <summary>Представляет сущность.</summary>
		/// <param name="context">Контекст представления.</param>
		/// <returns>Представленная сущность.</returns>
		T Represent(IRepresentationContext context = null);
	}
}
