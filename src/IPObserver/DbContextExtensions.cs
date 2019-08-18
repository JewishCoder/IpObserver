using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	public static class DbContextExtensions
	{
		/// <summary>Конфигурирует контекст только для чтения данных.</summary>
		/// <param name="context">Конфигурируемый контекс.</param>
		/// <exception cref="ArgumentNullException"><paramref name="context"/> == <c>null</c>.</exception>
		public static void ConfigureAsFetchOnly(this StorageContext context)
		{
			if(context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			context.ChangeTracker.AutoDetectChangesEnabled = false;
			context.ChangeTracker.LazyLoadingEnabled = false;
		}
	}
}
