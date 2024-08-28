using Puro.Exceptions;

namespace Puro.SqlServer.Runner.Exceptions;

/// <summary>
/// Exception that is thrown when an unsupported migration statement is being processed.
/// </summary>
public class UnsupportedMigrationStatementException : PuroException
{
	/// <summary>
	/// Initializes a new instance of the <see cref="UnsupportedMigrationStatementException"/> class.
	/// </summary>
	/// <param name="migrationStatementType">Migration statement type.</param>
	public UnsupportedMigrationStatementException(Type migrationStatementType)
		: base($"Migration statement type {migrationStatementType.Name} is not supported.")
	{
	}
}
