using Microsoft.Data.SqlClient;
using System.Reflection;

namespace Puro.SqlServer.Runner;

public sealed class Program
{
	public static async Task<int> Main(string[] args)
	{
		var settings = ParseArguments(args);
		if (settings is null)
		{
			Console.WriteLine("Invalid command line arguments.");
			return 1;
		}

		Type[] migrationTypes;
		try
		{
			var assembly = Assembly.LoadFrom(settings.AssemblyLocation!);
			migrationTypes = assembly.GetExportedTypes()
				.Where(t => typeof(Migration).IsAssignableFrom(t))
				.ToArray();
		}
		catch
		{
			Console.WriteLine("Unable to read migrations from assembly.");
			return 1;
		}

		Console.WriteLine($"Found {migrationTypes.Length} migrations.");
		var migrations = new List<Migration>(migrationTypes.Length);
		foreach (var migrationType in migrationTypes)
		{
			if (Activator.CreateInstance(migrationType) is not Migration migration)
			{
				continue;
			}

			migrations.Add(migration);
		}

		var orderedMigrations = migrations.OrderBy(m => m.Name).ToList();

		using var connection = new SqlConnection(settings.ConnectionString);
		using var command = new SqlCommand("SELECT [TABLE_NAME] FROM [INFORMATION_SCHEMA].[TABLES];", connection);

		await connection.OpenAsync();
		await command.ExecuteNonQueryAsync();

		return 0;
	}

	private static RunnerSettings? ParseArguments(string[] arguments)
	{
		const int expectedArgumentsCount = 4;
		const string assemblyLocationParameter = "--assembly-location";
		const string connectionStringParameter = "--connection-string";

		if (arguments.Length != expectedArgumentsCount)
		{
			return null;
		}

		var settings = new RunnerSettings();

		for (var i = 0; i < expectedArgumentsCount; i += 2)
		{
			if (arguments[i] == assemblyLocationParameter)
			{
				settings.AssemblyLocation = arguments[i + 1];
				continue;
			}

			if (arguments[i] == connectionStringParameter)
			{
				settings.ConnectionString = arguments[i + 1];
				continue;
			}
		}

		if (settings.AssemblyLocation is null || settings.ConnectionString is null)
		{
			return null;
		}

		return settings;
	}
}
