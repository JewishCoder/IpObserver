using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	internal sealed class RepresentationContext : IRepresentationContext
	{
		#region Static

		private readonly Dictionary<Type, Type> _implementations = new Dictionary<Type, Type>()
		{
			{ typeof(ICity), typeof(City) },
			{ typeof(ICounty), typeof(County) },
			{ typeof(IContinent), typeof(Continent) },
			{ typeof(ILocation), typeof(Location) },
			{ typeof(IIpV4Client), typeof(IpV4Client) },
			{ typeof(IIpV6Client), typeof(IpV6Client) },
		};

		#endregion

		#region Data

		private Dictionary<Type, Dictionary<long, object>> _caches;

		#endregion

		#region .ctor

		/// <summary>Создает <see cref="RepresentationContext"/>.</summary>
		public RepresentationContext()
		{
			_caches = new Dictionary<Type, Dictionary<long, object>>();
		}

		#endregion

		#region Methods

		private Dictionary<long, object> GetCache(Type type)
		{
			if(!_implementations.TryGetValue(type, out var tmp))
			{
				tmp = type;
			}
			if(!_caches.TryGetValue(tmp, out var cache))
			{
				cache = new Dictionary<long, object>();
				_caches.Add(tmp, cache);
			}
			return cache;
		}

		/// <summary>Возвращает закэшированную сущность или создает ее.</summary>
		/// <typeparam name="T">Тип сущности.</typeparam>
		/// <param name="id">Идентификатор сущности.</param>
		/// <param name="factory">Фабрика сущностей.</param>
		/// <returns>Сущность требуемого типа с указанным идентификатором.</returns>
		public T GetOrAdd<T>(long id, Func<T> factory)
			where T : class
		{
			var cache = GetCache(typeof(T));
			if(!cache.TryGetValue(id, out var value))
			{
				value = factory();

				if(!cache.TryGetValue(id, out var value2))
				{
					cache.Add(id, value);
				}
				else if(value != value2)
				{
					throw new InvalidOperationException($@"Cache value does not match {nameof(factory)} return value.");
				}
			}
			return (T)value;
		}

		/// <summary>Пытается вернуть закэшированную сущность.</summary>
		/// <typeparam name="T">Тип сущности.</typeparam>
		/// <param name="id">Идентификатор сущности.</param>
		/// <returns>Сущность требуемого типа с указанным идентификатором или <c>null</c>.</returns>
		public T TryGet<T>(long id)
			where T : class
		{
			var cache = GetCache(typeof(T));
			cache.TryGetValue(id, out var value);
			return (T)value;
		}

		#endregion
	}
}
