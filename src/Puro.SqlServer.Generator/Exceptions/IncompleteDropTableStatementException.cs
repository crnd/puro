namespace Puro.SqlServer.Generator.Exceptions;

/// <summary>
/// Exception that is thrown when drop table migration statement is incomplete.
/// </summary>
public class IncompleteDropTableStatementException : IncompleteMigrationStatementException
{
	/// <summary>
	/// Initializes a new instance of <see cref="IncompleteDropTableStatementException"/> class.
	/// </summary>
	/// <param name="table">Name of the table.</param>
	public IncompleteDropTableStatementException(string table)
		: base($"Drop table statement for table {table} is incomplete.")
	{
	}
}
