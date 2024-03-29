﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	internal static class EntityFactory
	{
		internal static Continent CreateContinent(IContinent data)
		{
			if(data == null) return null;
			var result = new Continent
			{
				Code = data.Code,
				Name = data.Name,
			};

			if(data.Counties != null && data.Counties.Count > 0)
			{
				var counties = new List<County>();
				for(var i = 0; i < data.Counties.Count; i++)
				{
					counties.Add(CreateCounty(data.Counties[i]));
				}
				result.Counties = counties;
			}

			return result;
		}

		internal static County CreateCounty(ICounty data)
		{
			if(data == null) return null;
			var result = new County
			{
				Code = data.Code,
				Name = data.Name,
				Continent = CreateContinent(data.Continent),
			};

			if(data.Cities != null && data.Cities.Count > 0)
			{
				var cities = new List<City>();
				for(var i = 0; i < data.Cities.Count; i++)
				{
					cities.Add(CreateCity(data.Cities[i]));
				}
				result.Cities = cities;
			}

			return result;
		}

		internal static City CreateCity(ICity data)
		{
			if(data == null) return null;
			return new City
			{
				Name = data.Name,
				County = CreateCounty(data.County),
			};
		}

		internal static IpV4Client CreateIpV4Client(IIpV4Client data)
		{
			if(data == null) return null;
			var result = new IpV4Client
			{
				IpV4 = data.IpV4,
			};
			if(data.Continent != null)
			{
				result.Continent = CreateContinent(data.Continent);
			}
			if(data.County != null)
			{
				result.County = CreateCounty(data.County);
			}
			if(data.City != null)
			{
				result.City = CreateCity(data.City);
			}

			return result;
		}

		internal static IpV6Client CreateIpV4Client(IIpV6Client data)
		{
			if(data == null) return null;
			var result = new IpV6Client
			{
				IpV6 = data.IpV6,
			};
			if(data.Continent != null)
			{
				result.Continent = CreateContinent(data.Continent);
			}
			if(data.County != null)
			{
				result.County = CreateCounty(data.County);
			}
			if(data.City != null)
			{
				result.City = CreateCity(data.City);
			}

			return result;
		}

		internal static Location CreateLocation(ILocation data)
		{
			if(data == null) return null;
			return new Location
			{
				AccuracyRadius = data.AccuracyRadius,
				Latitude = data.Latitude,
				Longitude = data.Longitude,
				TimeZone = data.TimeZone,
			};
		}
	}
}
