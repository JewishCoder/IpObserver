
using Microsoft.EntityFrameworkCore;

namespace IPObserver.DataStorage
{
	public sealed class IpV4Client : IpClient
	{
		public string IpV4 { get; set; }

		public IpV4Client()
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
	}
}
