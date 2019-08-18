
using Microsoft.EntityFrameworkCore;
using System;

namespace IPObserver.DataStorage
{
	public class IpClient : IEntity<long>
	{
		public long Id { get; set; }

		internal IEntity<long> CityId { get; set; }

		public City City { get; set; }

		internal IEntity<long> CountyId { get; set; }

		public County County { get; set; }

		internal IEntity<long> ContinentId { get; set; }

		public Continent Continent { get; set; }

		protected static Action<ModelBuilder> ChildConfigurateAction { get; set; }

		internal static void Configurate(ModelBuilder builder)
		{
			var model = builder.Entity<IpClient>();

			model.ToTable("IpClients");

			model
				.Property(x => x.Id)
				.HasColumnName("Id")
				.IsRequired();

			model.HasKey(x => x.Id);

			model
				.Property(x => x.CityId)
				.HasColumnName("CityId");

			model
				.Property(x => x.City)
				.HasColumnName("City");

			model
				.HasOne(x => x.City)
				.WithMany()
				.HasForeignKey(x => x.CityId);

			model
				.Property(x => x.CountyId)
				.HasColumnName("CountyId");

			model
				.Property(x => x.County)
				.HasColumnName("County");

			model
				.HasOne(x => x.County)
				.WithMany()
				.HasForeignKey(x => x.CountyId);

			model
				.Property(x => x.ContinentId)
				.HasColumnName("ContinentId");

			model
				.Property(x => x.Continent)
				.HasColumnName("Continent");

			model
				.HasOne(x => x.Continent)
				.WithMany()
				.HasForeignKey(x => x.ContinentId);

			ChildConfigurateAction?.Invoke(builder);
		}
	}
}
