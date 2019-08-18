using System.Threading;
using System.Threading.Tasks;

namespace IPObserver.DataStorage.Providers
{
	public interface IDatabaseProvider
	{
		IConnectionProvider ConnectionProvider { get; }

		DatabaseType DatabaseType { get; }

		Task CreateDatabaseAsync(CancellationToken cancellationToken);
	}
}
