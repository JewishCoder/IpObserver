
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace IPObserver.DataStorage
{
	internal sealed class County : IEntity<long>, IRepresentable<ICounty>
	{
		public long Id { get; set; }

		public string Name { get; set; }

		public string Code { get; set; }

		internal IEntity<long> ContinentId { get; set; }

		public Continent Continent { get; set; }

		public List<City> Cities { get; set; }

		internal County()
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
				.Property(x => x.Continent)
				.HasColumnName("Continent")
				.IsRequired();

			model
				.HasOne(x => x.Continent)
				.WithMany(x => x.Counties)
				.HasForeignKey(x => x.ContinentId);
		}

		public ICounty Represent(IRepresentationContext context = null)
		{
			if(context == null)
			{
				context = new RepresentationContext();
			}

			var cities = new List<ICity>();
			if(Cities.Count > 0)
			{
				for(var i = 0; i < Cities.Count; i++)
				{
					cities.Add(Cities[i].Represent(context));
				}
			}

			return context.GetOrAdd(Id, () => new CountyImpl(Name, Code, Continent.Represent(context), cities));
		}
	}
}
