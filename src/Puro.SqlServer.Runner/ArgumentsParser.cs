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

	public static RunnerSettings Parse(string[] args)
	{
		if (args.Length == 0 || args.Length % 2 != 0 || args.Length > 8 || (args[0] != AssemblyShortForm && args[0] != AssemblyLongForm))
		{
			throw new InvalidRunnerArgumentsException();
		}

		var settings = new RunnerSettings { AssemblyLocation = args[1] };

		for (var i = 2; i < args.Length; i += 2)
		{
			switch (args[i])
			{
				case FromMigrationShortForm:
				case FromMigrationLongForm:
					if (settings.FromMigration is not null)
					{
						throw new InvalidRunnerArgumentsException();
					}
					settings.FromMigration = args[i + 1];
					break;
				case ToMigrationShortForm:
				case ToMigrationLongForm:
					if (settings.ToMigration is not null)
					{
						throw new InvalidRunnerArgumentsException();
					}
					settings.ToMigration = args[i + 1];
					break;
				case ConnectionShortFrom:
				case ConnectionLongForm:
					if (settings.ConnectionString is not null)
					{
						throw new InvalidRunnerArgumentsException();
					}
					settings.ConnectionString = args[i + 1];
					break;
				default:
					throw new InvalidRunnerArgumentsException();
			}
		}

		return settings;
	}
}
