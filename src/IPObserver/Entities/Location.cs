
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IPObserver.DataStorage
{
	public sealed class Location : IEntity<long>, IRepresentable<ILocation>
	{
		#region Configuration

		internal sealed class LocationConfiguration : IEntityTypeConfiguration<Location>
		{
			public void Configure(EntityTypeBuilder<Location> builder)
			{

				builder.ToTable("Locations");

				builder
					.Property(x => x.Id)
					.HasColumnName("Id")
					.IsRequired()
					.ValueGeneratedOnAdd();

				builder.HasKey(x => x.Id);

				builder
					.Property(x => x.AccuracyRadius)
					.HasColumnName("AccuracyRadius");

				builder
					.Property(x => x.Latitude)
					.HasColumnName("Latitude");

				builder
					.Property(x => x.Longitude)
					.HasColumnName("Longitude");

				builder
					.Property(x => x.TimeZone)
					.HasColumnName("TimeZone");
			}
		}

		#endregion

		public long Id { get; set; }

		public int? AccuracyRadius { get; set; }

		public double? Latitude { get; set; }

		public double? Longitude { get; set; }

		public string TimeZone { get; set; }

		internal static LocationConfiguration GetConfiguration() => new LocationConfiguration();

		public ILocation Represent(IRepresentationContext context = null)
		{
			return RepresentFactory.CreateLocation(this, context);
		}
	}
}
