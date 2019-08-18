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
			Continent.Configurate(modelBuilder);
			County.Configurate(modelBuilder);
			City.Configurate(modelBuilder);
			IpClient.Configurate(modelBuilder);
			Location.Configurate(modelBuilder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			switch(DatabaseProvider.DatabaseType)
			{
				case DatabaseType.PostgreSql:
					optionsBuilder.UseNpgsql(DatabaseProvider.ConnectionProvider.GetConnection());
					break;
			}
		}
	}
}
