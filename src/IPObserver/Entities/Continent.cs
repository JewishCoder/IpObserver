using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace IPObserver.DataStorage
{
	public sealed class Continent : IEntity<long>, IRepresentable<IContinent>
	{
		#region Configuration

		internal sealed class ContinentConfiguration : IEntityTypeConfiguration<Continent>
		{
			public void Configure(EntityTypeBuilder<Continent> builder)
			{
				builder.ToTable("Continents");

				builder
					.Property(x => x.Id)
					.HasColumnName("Id")
					.IsRequired();

				builder.HasKey(x => x.Id);

				builder
					.Property(x => x.Name)
					.HasColumnName("Name")
					.IsRequired()
					.HasMaxLength(100);

				builder
					.Property(x => x.Code)
					.HasColumnName("Code")
					.IsRequired();
			}
		}

		#endregion

		public long Id { get; set; }

		public string Name { get; set; }

		public string Code { get; set; }

		public List<County> Counties { get; set; }

		public Continent()
		{
			Counties = new List<County>();
		}

		internal static ContinentConfiguration GetConfiguration() => new ContinentConfiguration();

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
