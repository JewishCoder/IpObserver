using System.Threading;
using System.Threading.Tasks;

namespace IPObserver.DataStorage.Providers
{
	public sealed class PostgreSqlDatabaseProvider : IDatabaseProvider
	{
		IConnectionProvider IDatabaseProvider.ConnectionProvider => ConnectionProvoder;

		public PostgreSqlConnectionProvoder ConnectionProvoder { get; }

		public DatabaseType DatabaseType { get; }

		public PostgreSqlDatabaseProvider(PostgreSqlConnectionProvoder connectionProvoder)
		{
			Verify.Argument.IsNotNull(connectionProvoder, nameof(connectionProvoder));

			DatabaseType = DatabaseType.PostgreSql;
			ConnectionProvoder = connectionProvoder;
		}

		public async Task CreateDatabaseAsync(CancellationToken cancellationToken)
		{
			using(var context = new StorageContext(this))
			{
				await context.Database
					.EnsureCreatedAsync(cancellationToken)
					.ConfigureAwait(continueOnCapturedContext: false);
			}
		}
	}
}
