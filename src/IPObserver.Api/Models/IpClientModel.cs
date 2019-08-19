using IPObserver.DataStorage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace IPObserver.Api.Models
{
	[DataContract]
	public class IpClientModel
	{
		[JsonProperty]
		public string IpV4 { get; set; }

		[JsonProperty]
		public string IpV6 { get; set; }

		[JsonProperty]
		public CityModel City { get; set; }

		[JsonProperty]
		public CounrtyModel County { get; set; }

		[JsonProperty]
		public ContinentModel Continent { get; set; }

		[JsonProperty]
		public LocationModel Location { get; set; }
	}
}
