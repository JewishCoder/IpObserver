using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace IPObserver.DataStorage.Providers
{
	public sealed class PostgreSqlConnectionProvoder : IConnectionProvider
	{
		#region Helpers

		static class Default
		{
			public static string DatabaseName = "IpObserverStorage";
			public static string UserName = "postgres";
			public static string Password = "admin";
			public static string Host = "127.0.0.1";
			public static int Port = 5432;
			public static int CommandTimeout = 60;
		}

		#endregion

		#region Data

		private string _databaseName;
		private string _userName;
		private string _password;
		private string _host;
		private int _port;
		private int _commandTimeout;
		private string _cacheConnectionString;

		#endregion

		#region Properties

		/// <summary>Устанавливает и возвращает наименования БД. По умолчанию "IpObserverStorage".</summary>
		public string DatabaseName
		{
			get => _databaseName;
			set
			{
				if(_databaseName != value)
				{
					_cacheConnectionString = null;
					_databaseName = value;
				}
			}
		}

		/// <summary>Устанавливает и возвращает имя пользователя. По умолчанию "postgres".</summary>
		public string UserName
		{
			get => _userName;
			set
			{
				if(_userName != value)
				{
					_cacheConnectionString = null;
					_userName = value;
				}
			}
		}

		/// <summary>Устанавливает и возвращает пароль. По умолчанию "admin".</summary>
		public string Password
		{
			get => _password;
			set
			{
				if(_password != value)
				{
					_cacheConnectionString = null;
					_password = value;
				}
			}
		}

		/// <summary>Устанавливает и возвращает хост. По умолчанию "127.0.0.1".</summary>
		public string Host
		{
			get => _host;
			set
			{
				if(_host != value)
				{
					_cacheConnectionString = null;
					_host = value;
				}
			}
		}

		/// <summary>Устанавливает и возвращает порт. По умолчанию "5432".</summary>
		public int Port
		{
			get => _port;
			set
			{
				if(_port != value)
				{
					_cacheConnectionString = null;
					_port = value;
				}
			}
		}

		/// <summary>Устанавливает и возвращает время выполнение команды. По умолчанию "60 секунд".</summary>
		public int CommandTimeout
		{
			get => _commandTimeout;
			set
			{
				if(_commandTimeout != value)
				{
					_cacheConnectionString = null;
					_commandTimeout = value;
				}
			}
		}

		#endregion

		#region .ctor

		public PostgreSqlConnectionProvoder()
		{
			DatabaseName = Default.DatabaseName;
			UserName = Default.UserName;
			Password = Default.Password;
			Host = Default.Host;
			Port = Default.Port;
			CommandTimeout = Default.CommandTimeout;
		}

		#endregion

		#region IConnectionProvider Implementation

		/// <summary>Возвращает строку подключение к БД.</summary>
		/// <exception cref="InvalidOperationException">
		/// <see cref="DatabaseName"/> is NULL or
		/// <see cref="Host"/> is NULL or
		/// <see cref="Port"/> is default value
		/// </exception>
		public string GetConnectionString()
		{
			if(_cacheConnectionString != null) return _cacheConnectionString;

			if(string.IsNullOrWhiteSpace(DatabaseName) || string.IsNullOrWhiteSpace(Host) || Port == default)
			{
				throw new InvalidOperationException("DatabaseName or connection address is null");
			}

			var builder = new NpgsqlConnectionStringBuilder
			{
				Database        = DatabaseName,
				Host            = Host,
				Port            = Port,
				CommandTimeout  =CommandTimeout,
				Encoding        = Encoding.UTF8.WebName,
				ClientEncoding  = Encoding.UTF8.WebName,
				ApplicationName = DatabaseName
			};
			if(!string.IsNullOrWhiteSpace(UserName))
			{
				builder.Username = UserName;
			}
			if(!string.IsNullOrWhiteSpace(Password))
			{
				builder.Password = Password;
			}

			_cacheConnectionString = builder.ToString();

			return _cacheConnectionString;
		}

		public DbConnection GetConnection()
		{
			return new NpgsqlConnection(GetConnectionString());
		}

		#endregion
	}
}
