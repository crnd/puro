using Puro.Exceptions;

namespace Puro.SqlServer.Runner.Exceptions;

/// <summary>
/// Exception that is thrown when a migration with a specified name is not found.
/// </summary>
public class MigrationNotFoundException : PuroException
{
	/// <summary>
	/// Initializes a new instance of the <see cref="MigrationNotFoundException"/> class.
	/// </summary>
	/// <param name="migrationName">Name of the migration.</param>
	public MigrationNotFoundException(string migrationName)
		: base($"Migration with name {migrationName} was not found.")
	{
	}
}
