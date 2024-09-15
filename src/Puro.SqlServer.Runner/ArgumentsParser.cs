using Puro.SqlServer.Runner.Exceptions;

namespace Puro.SqlServer.Runner;

internal static class ArgumentsParser
{
	public static RunnerSettings Parse(string[] args)
	{
		if (args.Length == 0 || args.Length % 2 != 0 || args.Length > 8 || (args[0] != Constants.AssemblyShortForm && args[0] != Constants.AssemblyLongForm))
		{
			throw new InvalidRunnerArgumentsException();
		}

		var settings = new RunnerSettings { AssemblyLocation = args[1] };

		for (var i = 2; i < args.Length; i += 2)
		{
			switch (args[i])
			{
				case Constants.FromMigrationShortForm:
				case Constants.FromMigrationLongForm:
					if (settings.FromMigration is not null)
					{
						throw new InvalidRunnerArgumentsException();
					}
					settings.FromMigration = args[i + 1];
					break;
				case Constants.ToMigrationShortForm:
				case Constants.ToMigrationLongForm:
					if (settings.ToMigration is not null)
					{
						throw new InvalidRunnerArgumentsException();
					}
					settings.ToMigration = args[i + 1];
					break;
				case Constants.ConnectionShortFrom:
				case Constants.ConnectionLongForm:
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
