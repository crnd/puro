namespace Puro.SqlServer.Generator.Exceptions;

/// <summary>
/// Exception that is thrown when drop index migration statement is incomplete.
/// </summary>
public class IncompleteDropIndexStatementException : IncompleteMigrationStatementException
{
	/// <summary>
	/// Initializes a new instance of <see cref="IncompleteDropIndexStatementException"/> class.
	/// </summary>
	/// <param name="index">Name of the index.</param>
	public IncompleteDropIndexStatementException(string index)
		: base($"Drop index statement for index {index} is incomplete.")
	{
	}
}
