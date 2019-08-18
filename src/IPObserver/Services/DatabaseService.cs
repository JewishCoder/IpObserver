using System.Threading.Tasks;

using IPObserver.DataStorage.Providers;

namespace IPObserver.DataStorage
{
	public sealed class DatabaseService : IDatabaseService
	{
		public IDatabaseProvider DatabaseProvider { get; }

		public DatabaseService(IDatabaseProvider databaseProvider)
		{
			Verify.Argument.IsNotNull(databaseProvider, nameof(databaseProvider));

			DatabaseProvider = databaseProvider;
		}

		public StorageContext CreateContext()
		{
			return new StorageContext(DatabaseProvider);
		}

		public Task<StorageContext> CreateContextAsync()
		{
			return Task.Run(CreateContext);
		}
	}
}
