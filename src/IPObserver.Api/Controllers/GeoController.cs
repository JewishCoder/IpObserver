using IPObserver.Api.Models;
using IPObserver.Api.Services;
using IPObserver.DataStorage;
using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace IPObserver.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GeoController : ControllerBase
	{
		private readonly DatabaseReader _databaseReader;
		private readonly ICitiesRepository _citiesRepository;
		private readonly IContinentsRepository _continentsRepository;
		private readonly ICountriesRepository _countriesRepository;
		private readonly IIpV4ClientsRepository _ipV4ClientsRepository;
		private readonly IIpV6ClientsRepository _ipV6ClientsRepository;

		public GeoController(
			IConfiguration configuration,
			ICitiesRepository citiesRepository,
			IContinentsRepository continentsRepository,
			ICountriesRepository countriesRepository,
			IIpV4ClientsRepository ipV4ClientsRepository,
			IIpV6ClientsRepository ipV6ClientsRepository)
		{
			_citiesRepository      = citiesRepository;
			_continentsRepository  = continentsRepository;
			_countriesRepository   = countriesRepository;
			_ipV4ClientsRepository = ipV4ClientsRepository;
			_ipV6ClientsRepository = ipV6ClientsRepository;
			_databaseReader = new DatabaseReader(configuration.GetValue<string>("MaxMindDbPath"));
		}

		[HttpGet]
		[Route("[action]/{ip?}")]
		public async Task<JsonResult> GetCity(string ip = null)
		{
			if(string.IsNullOrWhiteSpace(ip))
			{
				return new JsonResult(new ResponseModel(null, "IP address should not be NULL"));
			}
			
			if(IPAddress.TryParse(ip, out var address))
			{
				if(address.AddressFamily != AddressFamily.InterNetwork)
				{
					return new JsonResult(new ResponseModel(null, "Ip address invalidate"));
				}

				try
				{
					var response = _databaseReader.City(address);
					if(response == null) return new JsonResult(new ResponseModel(null, "Data not found"));

					var existIps = await _ipV4ClientsRepository
							.FetchAsync(new IpV4ClientFilter(x => x.IpV4.Equals(ip)))
							.ConfigureAwait(continueOnCapturedContext: false);
					if(existIps.Count > 0)
					{
						return new JsonResult(new ResponseModel(ModelFactory.CreateIpClient(existIps[0])));
					}

					var ipAdded = await AddIpAddressAsync(address, response).ConfigureAwait(continueOnCapturedContext: false);
					if(ipAdded != null)
					{
						return new JsonResult(new ResponseModel(ModelFactory.CreateIpClient(ipAdded)));
					}
				}
				catch(Exception exc)
				{
					return new JsonResult(new ResponseModel(null, exc.ToString()));
				}

				return new JsonResult(new ResponseModel(null, "Save to database failde"));
			}

			return new JsonResult(new ResponseModel(null, "Ip address invalidate"));
		}

		private async Task<IIpV4Client> AddIpAddressAsync(IPAddress address, CityResponse response, CancellationToken cancellationToken = default)
		{
			var continentTask = _continentsRepository.FetchAsync(new ContinentFilter(response.Continent.Name));
			var countryTask = _countriesRepository.FetchAsync(new CountyFilter(response.Country.Name));
			var cityTask = _citiesRepository.FetchAsync(new CityFilter(response.City.Name));

			await Task.WhenAll(continentTask, countryTask, cityTask)
				.ConfigureAwait(continueOnCapturedContext: false);

			var continent = continentTask.Result.FirstOrDefault();
			var country = countryTask.Result.FirstOrDefault();
			var city = cityTask.Result.FirstOrDefault();
			if(continent == null)
			{
				continent = await _continentsRepository
					.AddAsync(new ContinentData(response.Continent.Name, response.Continent.Code))
					.ConfigureAwait(continueOnCapturedContext: false);
			}
			if(country == null)
			{
				country = await _countriesRepository
					.AddAsync(new CountyData(response.Country.Name, response.Country.IsoCode, continent))
					.ConfigureAwait(continueOnCapturedContext: false);
			}
			if(city == null && !string.IsNullOrWhiteSpace(response.City.Name))
			{
				city = await _citiesRepository
					.AddAsync(new CityData(response.City.Name, country))
					.ConfigureAwait(continueOnCapturedContext: false);
			}
			var location = new LocationData(response.Location.AccuracyRadius, response.Location.Latitude, response.Location.Longitude, response.Location.TimeZone);
			return await _ipV4ClientsRepository
				.AddAsync(new IpV4ClientData(address.ToString(), city, country, continent, location))
				.ConfigureAwait(continueOnCapturedContext: false);
		}
	}
}
