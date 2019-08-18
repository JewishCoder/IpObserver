using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IPObserver.DataStorage
{
	public abstract class WriteableRepositoryBase<TEntity, TEntityImpl, TFilter, TData> : 
		RepositoryBase<TEntity, TEntityImpl, TFilter>, 
		IWriteableRepository<TEntityImpl, TFilter, TData>, IRemovableRepository<TFilter>
			where TEntity : class
			where TEntityImpl : class
	{
		public WriteableRepositoryBase(IDatabaseService databaseService): base(databaseService)
		{

		}

		internal abstract TEntity GetEntity(StorageContext context, TData data);

		internal abstract Task<TEntity> GetEntityAsync(StorageContext context, TData data, CancellationToken cancellationToken);

		public virtual TEntityImpl Add(TData data)
		{
			using(var context = DatabaseService.CreateContext())
			{
				var entity = GetDbSet(context).Add(GetEntity(context, data));

				context.SaveChanges();

				return Represent(entity.Entity);
			}
		}

		public virtual async Task<TEntityImpl> AddAsync(TData data, CancellationToken cancellationToken)
		{
			using(var context = await DatabaseService
				.CreateContextAsync()
				.ConfigureAwait(continueOnCapturedContext: false))
			{
				var entity = await GetEntityAsync(context, data, cancellationToken)
					.ConfigureAwait(continueOnCapturedContext: false);
				var result = GetDbSet(context).Add(entity);

				await context.SaveChangesAsync(cancellationToken)
				   .ConfigureAwait(continueOnCapturedContext: false);

				return Represent(result.Entity);
			}
		}


		public virtual int Remove(TFilter filter)
		{
			using(var context = DatabaseService.CreateContext())
			{

				var dbSet = GetDbSet(context);
				var records = ApplyFilter(dbSet, filter).ToList();
				if(records.Count > 0)
				{
					for(var i = 0; i < records.Count; i++)
					{
						dbSet.Remove(records[i]);
					}

					return context.SaveChanges();
				}

				return 0;
			}
		}

		public virtual async Task<int> RemoveAsync(TFilter filter, CancellationToken cancellationToken)
		{
			using(var context = await DatabaseService
				.CreateContextAsync()
				.ConfigureAwait(continueOnCapturedContext: false))
			{
				var dbSet = GetDbSet(context);
				var records = await ApplyFilter(dbSet, filter)
					.ToListAsync(cancellationToken)
					.ConfigureAwait(continueOnCapturedContext: false);
				if(records.Count > 0)
				{
					for(var i = 0; i < records.Count; i++)
					{
						dbSet.Remove(records[i]);
					}

					return await context
						.SaveChangesAsync(cancellationToken)
						.ConfigureAwait(continueOnCapturedContext: false);
				}

				return 0;
			}
		}
	}
}
