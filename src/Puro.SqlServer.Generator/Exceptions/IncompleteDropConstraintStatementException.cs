namespace Puro.SqlServer.Generator.Exceptions;

public class IncompleteDropConstraintStatementException : IncompleteMigrationStatementException
{
	public IncompleteDropConstraintStatementException(string constraint)
		: base($"Drop constraint statement for constraint {constraint} is incomplete.")
	{
	}
}
