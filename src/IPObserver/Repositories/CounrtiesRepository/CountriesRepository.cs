using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace IPObserver.DataStorage
{
	internal sealed class CountriesRepository : WriteableRepositoryBase<County, ICounty, CountyFilter, ICounty>, ICountriesRepository
	{
		private Dictionary<Type, Type> FilterEntityMap { get; }

		public CountriesRepository(IDatabaseService databaseService): base(databaseService)
		{
			FilterEntityMap = new Dictionary<Type, Type>
			{
				{ typeof(ICounty), typeof(County) },
				{ typeof(ICity), typeof(City) }
			};
		}

		protected override IQueryable<County> ApplyFilter(IQueryable<County> query, CountyFilter filter)
		{
			if(filter == null || filter.Expression == null)
			{
				return query;
			}

			var exp = FilterConverter.Convert<ICounty, County>(filter.Expression, FilterEntityMap);

			return query.Where(exp);
		}

		protected override DbSet<County> GetDbSet(StorageContext context)
		{
			return context.Set<County>();
		}

		protected override IQueryable<County> IncludeDependence(IQueryable<County> query)
		{
			return query
				.Include(x => x.Continent)
				.Include(x => x.Cities);
		}

		protected override ICounty Represent(County entity, IRepresentationContext representationContext = null)
		{
			return entity.Represent(representationContext);
		}

		internal override County GetEntity(StorageContext context, ICounty data)
		{
			var continent = GetDbSet(context)
				.Where(x => x.Continent.Name.Equals(data.Continent.Name))
				.Select(x => x.Continent)
				.FirstOrDefault();
			var country = new County
			{
				Name = data.Name,
				Code = data.Code,
			};
			if(continent == null)
			{
				continent = EntityFactory.CreateContinent(data.Continent);
			}

			country.ContinentId = continent.Id;
			country.Continent = continent;

			return country;
		}

		internal override async Task<County> GetEntityAsync(StorageContext context, ICounty data, CancellationToken cancellationToken)
		{
			var continent = await GetDbSet(context)
				.Where(x => x.Continent.Name.Equals(data.Continent.Name))
				.Select(x => x.Continent)
				.FirstOrDefaultAsync(cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);
			var country = new County
			{
				Name = data.Name,
				Code = data.Code,
			};
			if(continent == null)
			{
				continent = EntityFactory.CreateContinent(data.Continent);
			}

			country.ContinentId = continent.Id;
			country.Continent = continent;

			return country;
		}
	}
}
