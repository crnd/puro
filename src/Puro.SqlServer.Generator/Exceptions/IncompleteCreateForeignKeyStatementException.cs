namespace Puro.SqlServer.Generator.Exceptions;

/// <summary>
/// Exception that is thrown when create foreign key migration statement is incomplete.
/// </summary>
public class IncompleteCreateForeignKeyStatementException : IncompleteMigrationStatementException
{
	/// <summary>
	/// Initializes a new instance of <see cref="IncompleteCreateForeignKeyStatementException"/> class.
	/// </summary>
	/// <param name="foreignKey">Name of the foreign key constraint.</param>
	public IncompleteCreateForeignKeyStatementException(string foreignKey)
		: base($"Create foreign key statement for foreign key {foreignKey} is incomplete.")
	{
	}
}
