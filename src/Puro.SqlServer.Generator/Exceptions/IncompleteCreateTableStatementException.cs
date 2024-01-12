namespace Puro.SqlServer.Generator.Exceptions;

/// <summary>
/// Exception that is thrown when create table migration statement is incomplete.
/// </summary>
public class IncompleteCreateTableStatementException : IncompleteMigrationStatementException
{
	/// <summary>
	/// Initializes a new instance of <see cref="IncompleteCreateTableStatementException"/> class.
	/// </summary>
	/// <param name="table">Name of the table.</param>
	public IncompleteCreateTableStatementException(string table)
		: base($"Create table statement for table {table} is incomplete.")
	{
	}
}
