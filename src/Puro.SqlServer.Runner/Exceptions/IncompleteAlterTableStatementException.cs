namespace Puro.SqlServer.Runner.Exceptions;

/// <summary>
/// Exception that is thrown when alter table migration statement is incomplete.
/// </summary>
public class IncompleteAlterTableStatementException : IncompleteMigrationStatementException
{
	/// <summary>
	/// Initializes a new instance of <see cref="IncompleteAlterTableStatementException"/> class.
	/// </summary>
	/// <param name="table">Name of the table.</param>
	public IncompleteAlterTableStatementException(string table)
		: base($"Alter table statement for table {table} is incomplete.")
	{
	}
}
