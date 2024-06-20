namespace Puro.SqlServer.Runner.Exceptions;

/// <summary>
/// Exception that is thrown when create primary key migration statement is incomplete.
/// </summary>
public class IncompleteCreatePrimaryKeyStatementException : IncompleteMigrationStatementException
{
	/// <summary>
	/// Initializes a new instance of <see cref="IncompleteCreatePrimaryKeyStatementException"/> class.
	/// </summary>
	/// <param name="primaryKey">Name of the primary key constraint.</param>
	public IncompleteCreatePrimaryKeyStatementException(string primaryKey)
		: base($"Create primary key statement for primary key {primaryKey} is incomplete.")
	{
	}
}
