using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IPObserver.DataStorage;
using IPObserver.DataStorage.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IPObserver.Api.Services
{
	public class DatabaseServiceInitialization : IServiceInitialization
	{
		public async Task InitializeAsync(IServiceCollection serviceCollection, IConfiguration configuration, CancellationToken cancellationToken = default)
		{
			var connectionString = new PostgreSqlConnectionProvoder
			{
				DatabaseName = configuration.GetValue<string>("Database:PostgreSql:DatabaseName"),
				Host = configuration.GetValue<string>("Database:PostgreSql:Host"),
				Port = configuration.GetValue<int>("Database:PostgreSql:Port"),
				UserName = configuration.GetValue<string>("Database:PostgreSql:UserName"),
				Password = configuration.GetValue<string>("Database:PostgreSql:Password"),
				CommandTimeout = configuration.GetValue<int>("Database:PostgreSql:CommandTimeout"),
			};
			var databaseProvider = new PostgreSqlDatabaseProvider(connectionString);
			var databaseService = new DatabaseService(databaseProvider);
			
			serviceCollection.AddSingleton<IDatabaseService>(databaseService);
			serviceCollection.AddSingleton(RepositoriesFactory.Create<ICitiesRepository>(databaseService));
			serviceCollection.AddSingleton(RepositoriesFactory.Create<ICountriesRepository>(databaseService));
			serviceCollection.AddSingleton(RepositoriesFactory.Create<IContinentsRepository>(databaseService));
			serviceCollection.AddSingleton(RepositoriesFactory.Create<IIpV4ClientsRepository>(databaseService));
			serviceCollection.AddSingleton(RepositoriesFactory.Create<IIpV6ClientsRepository>(databaseService));
			serviceCollection.AddSingleton(RepositoriesFactory.Create<ILocationsRepository>(databaseService));

			await databaseProvider.CreateDatabaseAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}
	}
}
