using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IPObserver.Api.Services
{
	public class ServiceInitializationRepository
	{
		private List<IServiceInitialization> _services;

		public ServiceInitializationRepository()
		{
			_services = new List<IServiceInitialization>
			{
				new DatabaseServiceInitialization(),
			};
		}

		public IReadOnlyList<IServiceInitialization> GetServices()
		{
			return _services;
		}

		public async Task InitializationServices(IServiceCollection serviceCollection, IConfiguration configuration, CancellationToken cancellationToken = default)
		{
			for(var i = 0; i < _services.Count; i++)
			{
				await _services[i].InitializeAsync(serviceCollection, configuration, cancellationToken)
					.ConfigureAwait(continueOnCapturedContext: false);
			}
		}
	}
}
