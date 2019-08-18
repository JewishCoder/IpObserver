
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPObserver.DataStorage
{
	public sealed class County : IEntity<long>, IRepresentable<ICounty>
	{
		#region Configuration

		internal sealed class CountyConfiguration : IEntityTypeConfiguration<County>
		{
			public void Configure(EntityTypeBuilder<County> builder)
			{
				builder.ToTable("Counties");

				builder
					.Property(x => x.Id)
					.HasColumnName("Id")
					.IsRequired()
					.ValueGeneratedOnAdd();

				builder.HasKey(x => x.Id);

				builder
					.Property(x => x.Name)
					.HasColumnName("Name")
					.IsRequired()
					.HasMaxLength(400);

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

		internal long ContinentId { get; set; }

		public Continent Continent { get; set; }

		public List<City> Cities { get; set; }

		public County()
		{
			Cities = new List<City>();
		}

		internal static CountyConfiguration GetConfiguration() => new CountyConfiguration();

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
					var city = Cities[i];
					var county = default(ICounty);
					if(city.County != null)
					{
						county = new CountyImpl(city.County.Name, city.County.Code, city.County.Continent?.Represent(context), null);	
					}
					cities.Add(new CityImpl(city.Name, county));
				}
			}

			return context.GetOrAdd(Id, () => new CountyImpl(Name, Code, Continent?.Represent(context), cities));
		}
	}
}
