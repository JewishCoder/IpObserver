using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace IPObserver.Api.Models
{
	[DataContract]
	public class ResponseModel
	{
		[JsonProperty]
		public object Data { get; }

		[JsonProperty]
		public bool IsFailed { get; }

		[JsonProperty]
		public string Exception { get; }

		public ResponseModel(object data, string exception = null)
		{
			Data = data;
			if(exception != null)
			{
				Exception = exception;
				IsFailed = true;
			}
		}
	}
}
