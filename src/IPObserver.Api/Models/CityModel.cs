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
	public class CityModel
	{
		[JsonProperty]
		public string Name { get; set; }

		[JsonProperty]
		public CounrtyModel County { get; set; }
	}
}
