using IPObserver.DataStorage.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	public class StorageContext : DbContext
	{
		protected virtual IDatabaseProvider DatabaseProvider { get; }

		public StorageContext(IDatabaseProvider databaseProvider)
		{
			Verify.Argument.IsNotNull(databaseProvider, nameof(databaseProvider));

			DatabaseProvider = databaseProvider;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfiguration(City.GetConfiguration());
			modelBuilder.ApplyConfiguration(County.GetConfiguration());
			modelBuilder.ApplyConfiguration(Continent.GetConfiguration());
			modelBuilder.ApplyConfiguration(IpClient.GetConfiguration());
			modelBuilder.ApplyConfiguration(IpV4Client.GetIpV4Configuration());
			modelBuilder.ApplyConfiguration(IpV6Client.GetIpV6Configuration());
			modelBuilder.ApplyConfiguration(Location.GetConfiguration());
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			switch(DatabaseProvider.DatabaseType)
			{
				case DatabaseType.PostgreSql:
					var connetionString = DatabaseProvider.ConnectionProvider.GetConnectionString();
					optionsBuilder.UseNpgsql(connetionString);
					break;
			}
		}
	}
}
