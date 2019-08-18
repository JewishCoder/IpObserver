
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace IPObserver.DataStorage
{
	public sealed class County : IEntity<long>
	{
		public long Id { get; set; }

		public string Name { get; set; }

		public string Code { get; set; }

		internal IEntity<long> ContinentId { get; set; }

		public Continent Continent { get; set; }

		public List<City> Cities { get; set; }

		public County()
		{
			Cities = new List<City>();
		}

		internal static void Configurate(ModelBuilder builder)
		{
			var model = builder.Entity<County>();

			model.ToTable("Counties");

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
				.Property(x => x.Code)
				.HasColumnName("Code")
				.IsRequired();

			model
				.HasOne(x => x.Continent)
				.WithMany(x => x.Counties)
				.HasForeignKey(x => x.ContinentId);
		}
	}
}
