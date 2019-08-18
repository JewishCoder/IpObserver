using System;
using System.IO;

namespace IPObserver
{
	public static class Verify
	{
		public static class Argument
		{
			public static void IsNull<T>(T obj, string message = null) where T : class
			{
				if(obj != null)
				{
					var m = GetMessage(obj, "Argument is not null", message);
					throw new ArgumentNullException(m.Item1, m.Item2);
				}
			}

			public static void IsNotNull<T>(T obj, string message = null) where T : class
			{
				if(obj == null)
				{
					var m = GetMessage(obj, "Argument is null", message);
					throw new ArgumentNullException(m.Item1, m.Item2);
				}
			}

			public static void IsTrue(bool predicate, string message = null)
			{
				if(!predicate)
				{
					throw new ArgumentException(message ?? "The condition is not fulfilled");
				}
			}

			public static void IsTrue<T>(Func<T, bool> predicate, T obj, string message = null)
			{
				if(predicate == null)
				{
					throw new ArgumentNullException("Predicate is Null");
				}

				if(!predicate.Invoke(obj))
				{
					var m = GetMessage(obj, "The condition is not fulfilled", message);
					throw new ArgumentException(m.Item1, m.Item2);
				}
			}

			public static void IsFalse(bool predicate, string message = null)
			{
				if(predicate)
				{
					throw new ArgumentException(message ?? "The condition is not fulfilled");
				}
			}

			public static void IsFalse<T>(Func<T, bool> predicate, T obj, string message = null)
			{
				if(predicate == null)
				{
					throw new ArgumentNullException("Predicate is Null");
				}

				if(predicate.Invoke(obj))
				{
					var m = GetMessage(obj, "The condition is not fulfilled", message);
					throw new ArgumentException(m.Item1, m.Item2);
				}
			}

			public static void IsNullOrEmpty(string value, string message = null)
			{
				if(!string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentException(message ?? "Argument is not null or empty");
				}
			}

			public static void IsNotNullOrEmpty(string value, string message = null)
			{
				if(string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentException(message ?? "Argument is null or empty");
				}
			}

			private static Tuple<string, string> GetMessage<T>(T obj, string defaultMessage, string message = null)
			{
				return new Tuple<string, string>(obj.GetType().Name, message ?? defaultMessage);
			}
		}

		public static class FileSystem
		{
			public static void IsFileExists(string path, string message = null)
			{
				if(string.IsNullOrWhiteSpace(path))
				{
					throw new ArgumentException("Path must not be empty");
				}

				if(!File.Exists(path))
				{
					throw new FileNotFoundException(message ?? "File not found");
				}
			}

			public static void IsFileNotExists(string path, string message = null)
			{
				if(string.IsNullOrWhiteSpace(path))
				{
					throw new ArgumentException("Path must not be empty");
				}

				if(File.Exists(path))
				{
					throw new FileNotFoundException(message ?? "File already exists");
				}
			}

			public static void IsDirectoryExists(string path, string message = null)
			{
				if(string.IsNullOrWhiteSpace(path))
				{
					throw new ArgumentException("Path must not be empty");
				}

				if(!Directory.Exists(path))
				{
					throw new DirectoryNotFoundException(message ?? "Directory not found");
				}
			}

			public static void IsDirectoryNotExists(string path, string message = null)
			{
				if(string.IsNullOrWhiteSpace(path))
				{
					throw new ArgumentException("Path must not be empty");
				}

				if(Directory.Exists(path))
				{
					throw new DirectoryNotFoundException(message ?? "Directory already exists");
				}
			}
		}

	}
}
