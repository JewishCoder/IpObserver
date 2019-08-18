using Microsoft.EntityFrameworkCore;


namespace IPObserver.DataStorage
{
	public sealed class IpV6Client : IpClient
	{
		public string IpV6 { get; set; }

		public IpV6Client()
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
	}
}
