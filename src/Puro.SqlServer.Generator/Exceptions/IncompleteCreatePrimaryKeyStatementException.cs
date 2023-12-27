namespace Puro.SqlServer.Generator.Exceptions;

public class IncompleteCreatePrimaryKeyStatementException : IncompleteMigrationStatementException
{
	public IncompleteCreatePrimaryKeyStatementException(string primaryKey)
		: base($"Create primary key statement for primary key {primaryKey} is incomplete.")
	{
	}
}
