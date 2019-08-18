using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IPObserver.DataStorage
{
	internal sealed class IpV4ClientsRepository : WriteableRepositoryBase<IpV4Client, IIpV4Client, IpV4ClientFilter, IIpV4Client>, IIpV4ClientsRepository
	{
		private readonly ContinentsRepository _continentsRepository;
		private readonly CountriesRepository _countriesRepository;
		private readonly CitiesRepository _citiesRepositories;
		private readonly LocationsRepository _locationsRepository;

		private Dictionary<Type, Type> FilterEntityMap { get; }

		public IpV4ClientsRepository(IDatabaseService databaseService) : base(databaseService)
		{
			_continentsRepository = new ContinentsRepository(databaseService);
			_countriesRepository  = new CountriesRepository(databaseService);
			_citiesRepositories   = new CitiesRepository(databaseService);
			_locationsRepository  = new LocationsRepository(databaseService);

			FilterEntityMap = new Dictionary<Type, Type>
			{
				{ typeof(ICounty), typeof(County) },
				{ typeof(ICity), typeof(City) },
				{ typeof(IContinent), typeof(Continent) },
				{ typeof(ILocation), typeof(Location) }
			};
		}

		protected override IQueryable<IpV4Client> ApplyFilter(IQueryable<IpV4Client> query, IpV4ClientFilter filter)
		{
			if(filter == null || filter.Expression == null)
			{
				return query;
			}

			var exp = FilterConverter.Convert<IIpV4Client, IpV4Client>(filter.Expression, FilterEntityMap);

			return query.Where(exp);
		}

		protected override DbSet<IpV4Client> GetDbSet(StorageContext context)
		{
			return context.Set<IpV4Client>();
		}

		internal override IpV4Client GetEntity(StorageContext context, IIpV4Client data)
		{
			var continent = default(Continent);
			var country = default(County);
			var city = default(City);
			if(data.Continent != null)
			{
				continent = _continentsRepository.GetEntity(context, data.Continent);
			}
			if(data.County != null)
			{
				country = _countriesRepository.GetEntity(context, data.County);
			}
			if(data.City != null)
			{
				city = _citiesRepositories.GetEntity(context, data.City);
			}

			return new IpV4Client
			{
				IpV4 = data.IpV4,
				Continent = continent,
				County = country,
				City = city,
				Location = _locationsRepository.GetEntity(context, data.Location),
			};
		}

		internal override async Task<IpV4Client> GetEntityAsync(StorageContext context, IIpV4Client data, CancellationToken cancellationToken)
		{
			var continent = default(Continent);
			var country = default(County);
			var city = default(City);
			if(data.Continent != null)
			{
				continent = await _continentsRepository
					.GetEntityAsync(context, data.Continent, cancellationToken)
					.ConfigureAwait(continueOnCapturedContext: false);
			}
			if(data.County != null)
			{
				country = await _countriesRepository
					.GetEntityAsync(context, data.County, cancellationToken)
					.ConfigureAwait(continueOnCapturedContext: false);
			}
			if(data.City != null)
			{
				city = await _citiesRepositories.GetEntityAsync(context, data.City, cancellationToken)
					.ConfigureAwait(continueOnCapturedContext: false);
			}

			return new IpV4Client
			{
				IpV4 = data.IpV4,
				Continent = continent,
				County = country,
				City = city,
				Location = await _locationsRepository.GetEntityAsync(context, data.Location, cancellationToken)
					.ConfigureAwait(continueOnCapturedContext:false),
			};
		}

		protected override IQueryable<IpV4Client> IncludeDependence(IQueryable<IpV4Client> query)
		{
			return query
				.Include(x => x.Continent)
				.Include(x => x.County)
				.Include(x => x.City)
				.Include(x => x.Location);
		}

		protected override IIpV4Client Represent(IpV4Client entity, IRepresentationContext representationContext = null)
		{
			return entity.Represent(representationContext);
		}
	}
}
