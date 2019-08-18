using System.Data.Common;

namespace IPObserver.DataStorage.Providers
{
	public interface IConnectionProvider
	{
		string GetConnectionString();

		DbConnection GetConnection();
	}
}
