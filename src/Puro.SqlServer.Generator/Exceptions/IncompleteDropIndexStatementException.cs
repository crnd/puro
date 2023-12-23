namespace Puro.SqlServer.Generator.Exceptions;

public class IncompleteDropIndexStatementException : IncompleteMigrationStatementException
{
	public IncompleteDropIndexStatementException(string index)
		: base($"Drop index statement for index {index} is incomplete.")
	{
	}
}
