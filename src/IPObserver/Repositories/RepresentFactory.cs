using System;
using System.Collections.Generic;
using System.Text;

namespace IPObserver.DataStorage
{
	internal static class RepresentFactory
	{
		internal enum ReprecentType
		{
			OnlyEntity,
			Partial,
			Full,
		}

		internal static ICity CreateCity(City city, IRepresentationContext context, ReprecentType type = ReprecentType.Full)
		{
			if(city == null) return null;

			var counrty = default(ICounty);
			if(type == ReprecentType.Full)
			{
				if(city.County != null)
				{
					counrty = CreateCountry(city.County, context);
				}
			}

			return context.GetOrAdd(city.Id, () => new CityImpl(city.Name, counrty));
		}

		internal static ICounty CreateCountry(County county, IRepresentationContext context, ReprecentType type = ReprecentType.Full)
		{
			if(county == null) return null;

			var continent = default(IContinent);
			var cities = new List<ICity>();
			switch(type)
			{
				case ReprecentType.Full:
					continent = CreateContinent(county.Continent, context, ReprecentType.OnlyEntity);
					CreateCities(county.Cities, context, ReprecentType.OnlyEntity);
					break;
				case ReprecentType.Partial:
					CreateCities(county.Cities, context, ReprecentType.OnlyEntity);
					break;
			}

			return context.GetOrAdd(county.Id, () => new CountyImpl(county.Name, county.Code, continent, cities));
		}

		internal static IContinent CreateContinent(Continent continent, IRepresentationContext context, ReprecentType type = ReprecentType.Full)
		{
			if(continent == null) return null;
			var countries = new List<ICounty>();
			if(type == ReprecentType.Full)
			{
				if(continent.Counties.Count > 0)
				{
					for(var i = 0; i < continent.Counties.Count; i++)
					{
						countries.Add(CreateCountry(continent.Counties[i], context, ReprecentType.Partial));
					}
				}
			}

			return context.GetOrAdd(continent.Id, () => new ContinentImpl(continent.Name, continent.Code, countries));
		}

		internal static ILocation CreateLocation(Location location, IRepresentationContext context)
		{
			if(location == null) return null;

			return context.GetOrAdd(location.Id, () => new LocationImpl(
				 location.AccuracyRadius,
				 location.Latitude,
				 location.Longitude,
				 location.TimeZone));
		}

		internal static IIpV4Client CreateIpClient(IpV4Client client, IRepresentationContext context, ReprecentType type = ReprecentType.Full)
		{
			var city = default(ICity);
			var country = default(ICounty);
			var continent = default(IContinent);
			var location = default(ILocation);

			switch(type)
			{
				case ReprecentType.Full:
					city = CreateCity(client.City, context);
					country = CreateCountry(client.County, context);
					continent = CreateContinent(client.Continent, context, ReprecentType.OnlyEntity);
					location = CreateLocation(client.Location, context);
					break;
				case ReprecentType.Partial:
					location = CreateLocation(client.Location, context);
					break;
			}

			return context.GetOrAdd(client.Id, () => new IpV4ClientImpl(client.IpV4, city, country, continent, location));
		}

		internal static IIpV6Client CreateIpClient(IpV6Client client, IRepresentationContext context, ReprecentType type = ReprecentType.Full)
		{
			var city = default(ICity);
			var country = default(ICounty);
			var continent = default(IContinent);
			var location = default(ILocation);

			switch(type)
			{
				case ReprecentType.Full:
					city = CreateCity(client.City, context);
					country = CreateCountry(client.County, context);
					continent = CreateContinent(client.Continent, context, ReprecentType.OnlyEntity);
					location = CreateLocation(client.Location, context);
					break;
				case ReprecentType.Partial:
					location = CreateLocation(client.Location, context);
					break;
			}

			return context.GetOrAdd(client.Id, () => new IpV6ClientImpl(client.IpV6, city, country, continent, location));
		}

		private static IReadOnlyList<ICity> CreateCities(List<City> cities, IRepresentationContext context, ReprecentType type)
		{
			var result = new List<ICity>(capacity: cities.Count);
			if(cities.Count > 0)
			{
				for(var i = 0; i < cities.Count; i++)
				{
					result.Add(CreateCity(cities[i], context, type));
				}
			}
			return result;
		}
	}
}
