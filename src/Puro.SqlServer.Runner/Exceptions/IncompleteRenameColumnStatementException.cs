namespace Puro.SqlServer.Runner.Exceptions;

/// <summary>
/// Exception that is thrown when rename column migration statement is incomplete.
/// </summary>
public class IncompleteRenameColumnStatementException : IncompleteMigrationStatementException
{
	/// <summary>
	/// Initializes a new instance of <see cref="IncompleteRenameColumnStatementException"/> class.
	/// </summary>
	/// <param name="column">Name of the column being renamed.</param>
	public IncompleteRenameColumnStatementException(string column)
		: base($"Rename column statement for column {column} is incomplete.")
	{
	}
}
