using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace IPObserver.DataStorage
{
	public abstract class ReadOnlyRepositoryBase<TEntity, TEntityImpl, TFilter>: RepositoryBase<TEntity, TEntityImpl, TFilter>, IReadOnlyRepository<TEntityImpl, TFilter>
		where TEntity : class
		where TEntityImpl : class
	{

		public ReadOnlyRepositoryBase(IDatabaseService databaseService) :base(databaseService)
		{
	
		}

		public virtual int Count(TFilter filter)
		{
			using(var context = DatabaseService.CreateContext())
			{
				context.ConfigureAsFetchOnly();

				var query = GetDbSet(context).AsNoTracking();
				query = ApplyFilter(query, filter);

				return query.Count();
			}
		}

		public virtual async Task<int> CountAsync(TFilter filter, CancellationToken cancellationToken)
		{
			using(var context = await DatabaseService
				.CreateContextAsync()
				.ConfigureAwait(continueOnCapturedContext: false))
			{
				var query = GetDbSet(context).AsNoTracking();
				query = ApplyFilter(query, filter);

				return await query
					.CountAsync()
					.ConfigureAwait(continueOnCapturedContext: false);
			}
		}

		public virtual IReadOnlyList<TEntityImpl> Fetch(TFilter filter)
		{
			using(var context = DatabaseService.CreateContext())
			{
				context.ConfigureAsFetchOnly();

				var query = GetDbSet(context).AsNoTracking();
				query = IncludeDependence(query);
				query = ApplyFilter(query, filter);

				return query.ToList().ConvertAll(x => Represent(x));
			}
		}

		public virtual async Task<IReadOnlyList<TEntityImpl>> FetchAsync(TFilter filter, CancellationToken cancellation)
		{
			using(var context = await DatabaseService
				.CreateContextAsync()
				.ConfigureAwait(continueOnCapturedContext: false))
			{
				context.ConfigureAsFetchOnly();

				var query = GetDbSet(context).AsNoTracking();
				query = IncludeDependence(query);
				query = ApplyFilter(query, filter);

				var records = await query
					.ToListAsync(cancellation)
					.ConfigureAwait(continueOnCapturedContext: false);

				return records.ConvertAll(x => Represent(x));
			}
		}
	}
}
