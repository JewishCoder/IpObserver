
using Microsoft.EntityFrameworkCore;

namespace IPObserver.DataStorage
{
	internal sealed class Location : IEntity<long>, IRepresentable<ILocation>
	{
		public long Id { get; set; }

		public int? AccuracyRadius { get; set; }

		public double? Latitude { get; set; }

		public double? Longitude { get; set; }

		public string TimeZone { get; set; }

		internal static void Configurate(ModelBuilder builder)
		{
			var model = builder.Entity<Location>();

			model.ToTable("Continents");

			model
				.Property(x => x.Id)
				.HasColumnName("Id")
				.IsRequired();

			model.HasKey(x => x.Id);

			model
				.Property(x => x.AccuracyRadius)
				.HasColumnName("AccuracyRadius");

			model
				.Property(x => x.Latitude)
				.HasColumnName("Latitude");

			model
				.Property(x => x.Longitude)
				.HasColumnName("Longitude");

			model
				.Property(x => x.TimeZone)
				.HasColumnName("TimeZone");
		}

		public ILocation Represent(IRepresentationContext context = null)
		{
			throw new System.NotImplementedException();
		}
	}
}
