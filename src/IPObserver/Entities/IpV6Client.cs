using Microsoft.EntityFrameworkCore;


namespace IPObserver.DataStorage
{
	internal sealed class IpV6Client : IpClient, IRepresentable<IIpV6Client>
	{
		public string IpV6 { get; set; }

		internal IpV6Client()
		{

			ChildConfigurateAction = (builder) =>
			{
				var model = builder.Entity<IpV6Client>();

				model
					.Property(x => x.IpV6)
					.HasColumnName("IpV6")
					.IsRequired();
			};
		}

		public IIpV6Client Represent(IRepresentationContext context = null)
		{
			if(context == null)
			{
				context = new RepresentationContext();
			}

			return context.GetOrAdd(Id,
				() => new IpV6ClientImpl(
					IpV6,
					City.Represent(context),
					County.Represent(context),
					Continent.Represent(context)));
		}
	}
}
