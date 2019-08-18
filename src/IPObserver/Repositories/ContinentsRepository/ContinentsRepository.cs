using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IPObserver.DataStorage
{
	internal sealed class ContinentsRepository : WriteableRepositoryBase<Continent, IContinent, ContinentFilter, IContinent>, IContinentsRepository
	{
		private Dictionary<Type, Type> FilterEntityMap { get; }

		public ContinentsRepository(IDatabaseService databaseService): base(databaseService)
		{
			FilterEntityMap = new Dictionary<Type, Type>
			{
				{ typeof(ICounty), typeof(County) },
			};
		}

		protected override IQueryable<Continent> ApplyFilter(IQueryable<Continent> query, ContinentFilter filter)
		{
			if(filter == null || filter.Expression == null)
			{
				return query;
			}

			var exp = FilterConverter.Convert<IContinent, Continent>(filter.Expression, FilterEntityMap);

			return query.Where(exp);
		}

		protected override DbSet<Continent> GetDbSet(StorageContext context)
		{
			return context.Set<Continent>();
		}

		protected override Continent GetEntity(StorageContext context, IContinent data)
		{
			return EntityFactory.CreateContinent(data);
		}

		protected override Task<Continent> GetEntityAsync(StorageContext context, IContinent data, CancellationToken cancellationToken)
		{
			return Task.Run(() => EntityFactory.CreateContinent(data));
		}

		protected override IQueryable<Continent> IncludeDependence(IQueryable<Continent> query)
		{
			return query.Include(x => x.Counties);
		}

		protected override IContinent Represent(Continent entity, IRepresentationContext representationContext = null)
		{
			return entity.Represent(representationContext);
		}
	}
}
