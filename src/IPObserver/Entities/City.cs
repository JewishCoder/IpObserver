using Microsoft.EntityFrameworkCore;

namespace IPObserver.DataStorage
{
	public sealed class City : IEntity<long>
	{
		public long Id { get; set; }

		public string Name { get; set; }

		internal IEntity<long> CountyId { get; set; }

		public County County { get; set; }

		internal static void Configurate(ModelBuilder builder)
		{
			var model = builder.Entity<City>();

			model.ToTable("Cities");

			model
				.Property(x => x.Id)
				.HasColumnName("Id")
				.IsRequired();

			model.HasKey(x => x.Id);

			model
				.Property(x => x.Name)
				.HasColumnName("Name")
				.IsRequired()
				.HasMaxLength(400);

			model
				.Property(x => x.CountyId)
				.HasColumnName("CountyId")
				.IsRequired();

			model
				.Property(x => x.County)
				.HasColumnName("County")
				.IsRequired();

			model.HasOne(x => x.County)
				.WithMany(x => x.Cities)
				.HasForeignKey(x => x.CountyId);
		}
	}
}
