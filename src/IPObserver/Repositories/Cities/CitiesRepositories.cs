using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IPObserver.DataStorage
{
	internal sealed class CitiesRepositories : WriteableRepositoryBase<City, ICity, CityFilter, ICity>
	{
		private Dictionary<Type, Type> FilterEntityMap { get; }

		public CitiesRepositories(IDatabaseService databaseService) : base(databaseService)
		{
			FilterEntityMap = new Dictionary<Type, Type>
			{
				{ typeof(ICounty), typeof(County) },
				{ typeof(IContinent), typeof(Continent) }
			};
		}

		protected override IQueryable<City> ApplyFilter(IQueryable<City> query, CityFilter filter)
		{
			if(filter == null || filter.Expression == null)
			{
				return query;
			}

			var exp = FilterConverter.Convert<ICity, City>(filter.Expression, FilterEntityMap);

			return query.Where(exp);
		}

		protected override DbSet<City> GetDbSet(StorageContext context)
		{
			return context.Set<City>();
		}

		
		protected override IQueryable<City> IncludeDependence(IQueryable<City> query)
		{
			return query.Include(x => x.County);
		}

		protected override ICity Represent(City entity, IRepresentationContext representationContext = null)
		{
			return entity.Represent(representationContext);
		}

		protected override City GetEntity(StorageContext context, ICity data)
		{
			var county = GetDbSet(context)
				.Where(x => x.County.Name.Equals(data.County.Name))
				.Select(x=>x.County)
				.FirstOrDefault();
			var city = new City
			{
				Name = data.Name,
			};
			if(county == null)
			{
				county = EntityFactory.CreateCounty(data.County);
			}

			city.CountyId = county;
			city.County = county;

			return city;
		}

		protected override async Task<City> GetEntityAsync(StorageContext context, ICity data, CancellationToken cancellationToken)
		{
			var county = await GetDbSet(context)
				.Where(x => x.County.Name.Equals(data.County.Name))
				.Select(x => x.County)
				.FirstOrDefaultAsync(cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);
			var city = new City
			{
				Name = data.Name,
			};
			if(county == null)
			{
				county = EntityFactory.CreateCounty(data.County);
			}

			city.CountyId = county;
			city.County = county;

			return city;
		}
	}
}
