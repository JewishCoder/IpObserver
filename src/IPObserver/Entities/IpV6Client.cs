using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IPObserver.DataStorage
{
	internal sealed class IpV6Client : IpClient, IRepresentable<IIpV6Client>
	{
		#region Configuration

		internal sealed class IpV6ClientConfiguration : IEntityTypeConfiguration<IpV6Client>
		{
			public void Configure(EntityTypeBuilder<IpV6Client> builder)
			{
				builder
					.Property(x => x.IpV6)
					.HasColumnName("IpV6")
					.IsRequired();
			}
		}

		#endregion

		public string IpV6 { get; set; }

		internal static IpV6ClientConfiguration GetIpV6Configuration() => new IpV6ClientConfiguration();

		public IIpV6Client Represent(IRepresentationContext context = null)
		{
			if(context == null)
			{
				context = new RepresentationContext();
			}

			return context.GetOrAdd(Id,
				() => new IpV6ClientImpl(
					IpV6,
					City?.Represent(context),
					County?.Represent(context),
					Continent?.Represent(context),
					Location?.Represent(context)));
		}
	}
}
