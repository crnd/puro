using Microsoft.Data.SqlClient;
using Puro.SqlServer.Runner.Generators;
using System.Reflection;

namespace Puro.SqlServer.Runner;

public sealed class Program
{
	public static async Task<int> Main(string[] args)
	{
		try
		{
			var settings = ArgumentsParser.Parse(args);

			Console.Write("Loading migrations from assembly... ");
			var migrationTypes = Assembly
				.LoadFrom(settings.AssemblyLocation)
				.GetExportedTypes()
				.Where(t => typeof(Migration).IsAssignableFrom(t) && !t.IsAbstract)
				.ToArray();
			Console.WriteLine("OK");

			Console.Write("Generating SQL from migrations... ");
			var (migrations, isUpDirection) = MigrationsProcessor.Prepare(migrationTypes, settings.FromMigration, settings.ToMigration);
			var sql = MigrationSqlGenerator.Generate(migrations, isUpDirection);
			Console.WriteLine("OK");

			if (settings.ConnectionString is null)
			{
				Console.WriteLine(sql);

				return 0;
			}

			using var connection = new SqlConnection(settings.ConnectionString);
			using var command = new SqlCommand(sql, connection);

			Console.Write("Connecting to database... ");
			await connection.OpenAsync();
			Console.WriteLine("OK");

			Console.Write("Executing migration SQL... ");
			await command.ExecuteNonQueryAsync();
			Console.WriteLine("OK");

			return 0;
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine(ex.Message);

			return 1;
		}
	}
}
