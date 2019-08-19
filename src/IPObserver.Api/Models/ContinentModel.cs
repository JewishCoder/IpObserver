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
	public class ContinentModel
	{
		[JsonProperty]
		public string Name { get; set; }

		[JsonProperty]
		public string Code { get; set; }

		[JsonProperty]
		public List<CounrtyModel> Counties { get; set; }

		public ContinentModel()
		{
			Counties = new List<CounrtyModel>();
		}
	}
}
