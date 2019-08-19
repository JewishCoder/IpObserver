using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace IPObserver.DataStorage
{
	internal sealed class LocationsRepository : CrudRepositoryBasecs<Location, ILocation, LocationFilter, ILocation>, ILocationsRepository
	{
		public LocationsRepository(IDatabaseService databaseService) : base(databaseService)
		{
		}

		protected override IQueryable<Location> ApplyFilter(IQueryable<Location> query, LocationFilter filter)
		{
			if(filter == null || filter.Expression == null)
			{
				return query;
			}

			var exp = FilterConverter.Convert<ILocation, Location>(filter.Expression);

			return query.Where(exp);
		}

		protected override DbSet<Location> GetDbSet(StorageContext context)
		{
			return context.Set<Location>();
		}

		protected override IQueryable<Location> IncludeDependence(IQueryable<Location> query)
		{
			return query;
		}

		protected override ILocation Represent(Location entity, IRepresentationContext representationContext = null)
		{
			return entity.Represent(representationContext);
		}

		internal override Location GetEntity(StorageContext context, ILocation data)
		{
			return EntityFactory.CreateLocation(data);
		}

		internal override Task<Location> GetEntityAsync(StorageContext context, ILocation data, CancellationToken cancellationToken)
		{
			return Task.Run(() => EntityFactory.CreateLocation(data), cancellationToken);
		}
	}
}
