namespace Puro.SqlServer.Runner.Exceptions;

/// <summary>
/// Exception that is thrown when rename table migration statement is incomplete.
/// </summary>
public class IncompleteRenameTableStatementException : IncompleteMigrationStatementException
{
	/// <summary>
	/// Initializes a new instance of <see cref="IncompleteRenameTableStatementException"/> class.
	/// </summary>
	/// <param name="table">Name of the table being renamed.</param>
	public IncompleteRenameTableStatementException(string table)
		: base($"Rename table statement for table {table} is incomplete.")
	{
	}
}
