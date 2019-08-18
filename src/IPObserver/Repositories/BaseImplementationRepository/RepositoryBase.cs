using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPObserver.DataStorage
{
	public abstract class RepositoryBase<TEntity, TEntityImpl, TFilter>
		where TEntity : class
		where TEntityImpl : class
	{
		public IDatabaseService DatabaseService { get; }

		public RepositoryBase(IDatabaseService databaseService)
		{
			Verify.Argument.IsNotNull(databaseService, nameof(databaseService));

			DatabaseService = databaseService;
		}

		protected abstract DbSet<TEntity> GetDbSet(StorageContext context);

		protected abstract IQueryable<TEntity> IncludeDependence(IQueryable<TEntity> query);

		protected abstract IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query, TFilter filter);

		protected abstract TEntityImpl Represent(TEntity entity, IRepresentationContext representationContext = null);
	}
}
