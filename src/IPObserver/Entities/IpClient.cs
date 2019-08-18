
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace IPObserver.DataStorage
{
	public class IpClient : IEntity<long>
	{
		#region Configuration

		internal class IpClientConfiguration : IEntityTypeConfiguration<IpClient>
		{
			public void Configure(EntityTypeBuilder<IpClient> builder)
			{
				builder.ToTable("IpClients");

				builder
					.Property(x => x.Id)
					.HasColumnName("Id")
					.IsRequired()
					.ValueGeneratedOnAdd();

				builder.HasKey(x => x.Id);

				builder
					.Property(x => x.CityId)
					.HasColumnName("CityId");


				builder
					.Property(x => x.CountyId)
					.HasColumnName("CountyId");

				builder
					.Property(x => x.ContinentId)
					.HasColumnName("ContinentId");

				builder
					.Property(x => x.LocationId)
					.HasColumnName("LocationId");
			}
		}

		#endregion


		public long Id { get; set; }

		internal long CityId { get; set; }

		public City City { get; set; }

		internal long CountyId { get; set; }

		public County County { get; set; }

		internal long ContinentId { get; set; }

		public Continent Continent { get; set; }

		internal long LocationId { get; set; }

		public Location Location { get; set; }

		internal static IpClientConfiguration GetConfiguration() => new IpClientConfiguration();
	}
}
