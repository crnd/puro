namespace Puro.SqlServer.Generator.Exceptions;

/// <summary>
/// Exception that is thrown when create index statement is incomplete.
/// </summary>
public class IncompleteCreateIndexStatementException : IncompleteMigrationStatementException
{
	/// <summary>
	/// Initializes a new instance of <see cref="IncompleteCreateIndexStatementException"/> class.
	/// </summary>
	/// <param name="index">Name of the index.</param>
	public IncompleteCreateIndexStatementException(string index)
		: base($"Create index statement for index {index} is incomplete.")
	{
	}
}
