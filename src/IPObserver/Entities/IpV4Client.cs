
using Microsoft.EntityFrameworkCore;

namespace IPObserver.DataStorage
{
	internal sealed class IpV4Client : IpClient, IRepresentable<IIpV4Client>
	{
		public string IpV4 { get; set; }

		internal IpV4Client()
		{
			ChildConfigurateAction = (builder) =>
			{
				var model = builder.Entity<IpV4Client>();

				model
					.Property(x => x.IpV4)
					.HasColumnName("IpV4")
					.IsRequired();
			};
		}

		public IIpV4Client Represent(IRepresentationContext context = null)
		{
			if(context == null)
			{
				context = new RepresentationContext();
			}

			return context.GetOrAdd(Id,
				() => new IpV4ClientImpl(
					IpV4,
					City.Represent(context),
					County.Represent(context),
					Continent.Represent(context),
					Location.Represent(context)));
		}
	}
}
