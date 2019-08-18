
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IPObserver.DataStorage
{
	public sealed class IpV4Client : IpClient, IRepresentable<IIpV4Client>
	{
		#region Configuration

		internal sealed class IpV4ClientConfiguration : IEntityTypeConfiguration<IpV4Client>
		{
			public void Configure(EntityTypeBuilder<IpV4Client> builder)
			{
				builder
					.Property(x => x.IpV4)
					.HasColumnName("IpV4")
					.IsRequired();
			}
		}

		#endregion

		public string IpV4 { get; set; }

		internal static IpV4ClientConfiguration GetIpV4Configuration() => new IpV4ClientConfiguration();

		public IIpV4Client Represent(IRepresentationContext context = null)
		{
			if(context == null)
			{
				context = new RepresentationContext();
			}

			return context.GetOrAdd(Id,
				() => new IpV4ClientImpl(
					IpV4,
					City?.Represent(context),
					County?.Represent(context),
					Continent?.Represent(context),
					Location?.Represent(context)));
		}
	}
}
