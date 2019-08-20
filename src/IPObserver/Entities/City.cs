using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPObserver.DataStorage
{
	public sealed class City : IEntity<long>, IRepresentable<ICity>
	{
		#region Configuration

		internal sealed class CityConfiguration : IEntityTypeConfiguration<City>
		{
			public void Configure(EntityTypeBuilder<City> builder)
			{
				builder.ToTable("Cities");

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
					.Property(x => x.CountyId)
					.HasColumnName("CountyId")
					.IsRequired();
			}
		}

		#endregion

		public long Id { get; set; }

		public string Name { get; set; }

		internal long CountyId { get; set; }

		public County County { get; set; }

		internal static CityConfiguration GetConfiguration() => new CityConfiguration();

		public ICity Represent(IRepresentationContext context = null)
		{
			if(context == null)
			{
				context = new RepresentationContext();
			}

			return RepresentFactory.CreateCity(this, context);
		}
	}
}
