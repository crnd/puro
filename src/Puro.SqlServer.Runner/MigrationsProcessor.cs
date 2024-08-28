using Puro.SqlServer.Runner.Exceptions;

namespace Puro.SqlServer.Runner;

internal static class MigrationsProcessor
{
	public static List<Migration> Prepare(Type[] migrationTypes, string? fromMigration, string? toMigration)
	{
		var migrations = InstantiateMigrations(migrationTypes);
		var fromMigrationIndex = FindMigrationIndex(fromMigration, migrations);
		var toMigrationIndex = FindMigrationIndex(toMigration, migrations);

		if (IsUpDirection(fromMigrationIndex, toMigrationIndex))
		{
			if (fromMigrationIndex is not null || toMigrationIndex is not null)
			{
				// If starting migration has been defined, increment it by one so the first migration
				// being applied is the one after the starting migration.
				if (fromMigrationIndex is not null)
				{
					fromMigrationIndex++;
				}

				// If there is a to migration defined, calculate how many migrations should
				// be included. Otherwise include all the rest of the migrations.
				var amount = migrations.Count;
				if (toMigrationIndex is not null)
				{
					amount = toMigrationIndex.Value + 1 - (fromMigrationIndex ?? 0);
				}

				migrations = migrations
					.Skip(fromMigrationIndex ?? 0)
					.Take(amount)
					.ToList();
			}

			foreach (var migration in migrations)
			{
				migration.Up();
			}
		}
		else
		{
			// From and to migrations indexes are guaranteed to exist for the down direction.
			migrations = migrations
				.Skip(toMigrationIndex!.Value + 1)
				.Take(fromMigrationIndex!.Value - toMigrationIndex!.Value)
				.Reverse()
				.ToList();

			foreach (var migration in migrations)
			{
				migration.Down();
			}
		}

		return migrations;
	}

	public static List<Migration> InstantiateMigrations(Type[] migrationTypes)
	{
		var migrations = new List<Migration>(migrationTypes.Length);
		foreach (var migrationType in migrationTypes)
		{
			if (Activator.CreateInstance(migrationType) is not Migration migration)
			{
				continue;
			}

			migrations.Add(migration);
		}

		return [.. migrations.OrderBy(m => m.Name)];
	}

	public static int? FindMigrationIndex(string? migrationName, List<Migration> migrations)
	{
		if (migrationName is null)
		{
			return null;
		}

		var index = migrations.FindIndex(m => m.Name == migrationName);
		if (index == -1)
		{
			throw new MigrationNotFoundException(migrationName);
		}

		return index;
	}

	public static bool IsUpDirection(int? fromIndex, int? toIndex)
	{
		if (fromIndex is null || toIndex is null)
		{
			return true;
		}

		return fromIndex <= toIndex;
	}
}
