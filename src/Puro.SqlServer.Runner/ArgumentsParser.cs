using Puro.SqlServer.Runner.Exceptions;

namespace Puro.SqlServer.Runner;

internal static class ArgumentsParser
{
	private const string AssemblyShortForm = "-a";
	private const string AssemblyLongForm = "--assembly";

	private const string FromMigrationShortForm = "-f";
	private const string FromMigrationLongForm = "--from-migration";

	private const string ToMigrationShortForm = "-t";
	private const string ToMigrationLongForm = "--to-migration";

	private const string ConnectionShortFrom = "-c";
	private const string ConnectionLongForm = "--connection";

	public static RunnerSettings Parse(string[] arguments)
	{
		if (arguments.Length == 0 || arguments.Length % 2 != 0 || arguments.Length > 8)
		{
			throw new InvalidRunnerArgumentsException();
		}

		string? assemblyLocation = null;
		string? fromMigration = null;
		string? toMigration = null;
		string? connectionString = null;

		for (var i = 0; i < arguments.Length; i += 2)
		{
			switch (arguments[i])
			{
				case AssemblyShortForm:
				case AssemblyLongForm:
					if (assemblyLocation is not null)
					{
						throw new InvalidRunnerArgumentsException();
					}
					assemblyLocation = arguments[i + 1];
					break;
				case FromMigrationShortForm:
				case FromMigrationLongForm:
					if (fromMigration is not null)
					{
						throw new InvalidRunnerArgumentsException();
					}
					fromMigration = arguments[i + 1];
					break;
				case ToMigrationShortForm:
				case ToMigrationLongForm:
					if (toMigration is not null)
					{
						throw new InvalidRunnerArgumentsException();
					}
					toMigration = arguments[i + 1];
					break;
				case ConnectionShortFrom:
				case ConnectionLongForm:
					if (connectionString is not null)
					{
						throw new InvalidRunnerArgumentsException();
					}
					connectionString = arguments[i + 1];
					break;
				default:
					throw new InvalidRunnerArgumentsException();
			}
		}

		if (assemblyLocation is null)
		{
			throw new InvalidRunnerArgumentsException();
		}

		return new RunnerSettings
		{
			AssemblyLocation = assemblyLocation,
			FromMigration = fromMigration,
			ToMigration = toMigration,
			ConnectionString = connectionString
		};
	}
}
