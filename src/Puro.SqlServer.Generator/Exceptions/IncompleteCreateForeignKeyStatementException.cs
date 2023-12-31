namespace Puro.SqlServer.Generator.Exceptions;

public class IncompleteCreateForeignKeyStatementException : IncompleteMigrationStatementException
{
	public IncompleteCreateForeignKeyStatementException(string foreignKey)
		: base($"Create foreign key statement for foreign key {foreignKey} is incomplete.")
	{
	}
}
