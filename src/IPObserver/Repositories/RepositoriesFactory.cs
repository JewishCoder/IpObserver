using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	public static class RepositoriesFactory
	{
		private static Dictionary<Type, Func<IDatabaseService, object>> _repositories = new Dictionary<Type, Func<IDatabaseService, object>>
		{
			{ typeof(IContinentsRepository), x => new ContinentsRepository(x) },
			{ typeof(ICountriesRepository), x => new CountriesRepository(x) },
			{ typeof(ICitiesRepository), x => new CitiesRepository(x) },
			{ typeof(ILocationsRepository), x => new LocationsRepository(x) },
			{ typeof(IIpV4ClientsRepository), x => new IpV4ClientsRepository(x) },
			{ typeof(IIpV6ClientsRepository), x => new IpV6ClientsRepository(x) },

		};

		public static T Create<T>(IDatabaseService databaseService)
		{
			if(_repositories.TryGetValue(typeof(T), out var repository))
			{
				return (T)repository(databaseService);
			}

			return default;
		}
	}
}
