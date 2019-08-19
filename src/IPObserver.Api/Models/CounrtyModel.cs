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
	public class CounrtyModel
	{
		[JsonProperty]
		public string Name { get; set; }

		[JsonProperty]
		public string Code { get; set; }

		[JsonProperty]
		public ContinentModel Continent { get; set; }

		[JsonProperty]
		public List<CityModel> Cities { get; set; }
	}
}
