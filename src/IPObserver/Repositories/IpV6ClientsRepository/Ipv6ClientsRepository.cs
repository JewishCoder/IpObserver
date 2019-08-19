using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IPObserver.DataStorage
{
	internal sealed class IpV6ClientsRepository : CrudRepositoryBasecs<IpV6Client, IIpV6Client, IpV6ClientFilter, IIpV6Client>, IIpV6ClientsRepository
	{
		private readonly ContinentsRepository _continentsRepository;
		private readonly CountriesRepository _countriesRepository;
		private readonly CitiesRepository _citiesRepositories;
		private readonly LocationsRepository _locationsRepository;

		private Dictionary<Type, Type> FilterEntityMap { get; }

		public IpV6ClientsRepository(IDatabaseService databaseService) : base(databaseService)
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

		protected override IQueryable<IpV6Client> ApplyFilter(IQueryable<IpV6Client> query, IpV6ClientFilter filter)
		{
			if(filter == null || filter.Expression == null)
			{
				return query;
			}

			var exp = FilterConverter.Convert<IIpV6Client, IpV6Client>(filter.Expression, FilterEntityMap);

			return query.Where(exp);
		}

		protected override DbSet<IpV6Client> GetDbSet(StorageContext context)
		{
			return context.Set<IpV6Client>();
		}

		internal override IpV6Client GetEntity(StorageContext context, IIpV6Client data)
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

			return new IpV6Client
			{
				IpV6      = data.IpV6,
				Continent = continent,
				County    = country,
				City      = city,
				Location  = _locationsRepository.GetEntity(context, data.Location),
			};
		}

		internal override async Task<IpV6Client> GetEntityAsync(StorageContext context, IIpV6Client data, CancellationToken cancellationToken)
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

			return new IpV6Client
			{
				IpV6      = data.IpV6,
				Continent = continent,
				County    = country,
				City      = city,
				Location  = await _locationsRepository.GetEntityAsync(context, data.Location, cancellationToken)
					.ConfigureAwait(continueOnCapturedContext:false),
			};
		}

		protected override IQueryable<IpV6Client> IncludeDependence(IQueryable<IpV6Client> query)
		{
			return query
				.Include(x => x.Continent)
				.Include(x => x.County)
				.Include(x => x.City)
				.Include(x => x.Location);
		}

		protected override IIpV6Client Represent(IpV6Client entity, IRepresentationContext representationContext = null)
		{
			return entity.Represent(representationContext);
		}
	}
}
