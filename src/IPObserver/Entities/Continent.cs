using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace IPObserver.DataStorage
{
	public sealed class Continent : IEntity<long>
	{
		public long Id { get; set; }

		public string Name { get; set; }

		public string Code { get; set; }

		public List<County> Counties { get; }

		public Continent()
		{
			Counties = new List<County>();
		}

		internal static void Configurate(ModelBuilder builder)
		{
			var model = builder.Entity<Continent>();

			model.ToTable("Continents");

			model
				.Property(x => x.Id)
				.HasColumnName("Id")
				.IsRequired();

			model.HasKey(x => x.Id);

			model
				.Property(x => x.Name)
				.HasColumnName("Name")
				.IsRequired()
				.HasMaxLength(100);

			model
				.Property(x => x.Code)
				.HasColumnName("Code")
				.IsRequired();
		}
	}
}
