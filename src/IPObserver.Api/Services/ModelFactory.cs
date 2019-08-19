using IPObserver.Api.Models;
using IPObserver.DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPObserver.Api.Services
{
	public static class ModelFactory
	{
		public static ContinentModel CreateContinent(IContinent continent)
		{
			var result = new ContinentModel
			{
				Code = continent.Code,
				Name = continent.Name,
			};
			if(continent.Counties.Count > 0)
			{
				for(var i = 0; i < continent.Counties.Count; i++)
				{
					result.Counties.Add(CreateCountry(continent.Counties[i]));
				}
			}

			return result;
		}

		public static CounrtyModel CreateCountry(ICounty county)
		{
			var result = new CounrtyModel
			{
				Code = county.Code,
				Name = county.Name,
				Continent = CreateContinent(county.Continent),
			};
			if(county.Cities.Count > 0)
			{
				for(var i = 0; i < county.Cities.Count; i++)
				{
					result.Cities.Add(CreateCity(county.Cities[i]));
				}
			}

			return result;
		}

		public static CityModel CreateCity(ICity city)
		{
			return new CityModel
			{
				Name = city.Name,
				County = CreateCountry(city.County),
			};
		}

		public static LocationModel CreateLocation(ILocation location)
		{
			return new LocationModel
			{
				AccuracyRadius = location.AccuracyRadius,
				Latitude = location.Latitude,
				Longitude = location.Longitude,
				TimeZone = location.TimeZone,
			};
		}

		public static IpClientModel CreateIpClient(IIpV4Client ipV4Client)
		{
			var result = new IpClientModel
			{
				IpV4 = ipV4Client.IpV4,
			};
			if(ipV4Client.Continent != null)
			{
				result.Continent = CreateContinent(ipV4Client.Continent);
			}
			if(ipV4Client.County != null)
			{
				result.County = CreateCountry(ipV4Client.County);
			}
			if(ipV4Client.City != null)
			{
				result.City = CreateCity(ipV4Client.City);
			}

			return result;
		}

		public static IpClientModel CreateIpClient(IIpV6Client ipV6Client)
		{
			var result = new IpClientModel
			{
				IpV6 = ipV6Client.IpV6,
			};
			if(ipV6Client.Continent != null)
			{
				result.Continent = CreateContinent(ipV6Client.Continent);
			}
			if(ipV6Client.County != null)
			{
				result.County = CreateCountry(ipV6Client.County);
			}
			if(ipV6Client.City != null)
			{
				result.City = CreateCity(ipV6Client.City);
			}

			return result;
		}
	}
}
