using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace IPObserver.DataStorage
{
	internal sealed class Continent : IEntity<long>, IRepresentable<IContinent>
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

		public IContinent Represent(IRepresentationContext context = null)
		{
			if(context == null)
			{
				context = new RepresentationContext();
			}

			var counties = new List<ICounty>();
			if(Counties.Count > 0)
			{
				for(var i = 0; i < Counties.Count; i++)
				{
					counties.Add(Counties[i].Represent(context));
				}
			}

			return context.GetOrAdd(Id, () => new ContinentImpl(Name, Code, counties));
		}
	}
}
