namespace Puro.SqlServer.Generator.Exceptions;

public class IncompleteDropTableStatementException : IncompleteMigrationStatementException
{
	public IncompleteDropTableStatementException(string table)
		: base($"Drop table statement for table {table} is incomplete.")
	{
	}
}
