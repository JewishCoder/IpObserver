
namespace IPObserver.DataStorage
{
	public sealed class CityData : ICity
	{
		public string Name { get; }

		public ICounty County { get; }

		public CityData(string name, ICounty county)
		{
			Name = name;
			County = county;
		}
	}
}
