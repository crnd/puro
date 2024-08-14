namespace Puro.SqlServer.Runner.Exceptions;

/// <summary>
/// Exception that is thrown when rename index migration statement is incomplete.
/// </summary>
public class IncompleteRenameIndexStatementException : IncompleteMigrationStatementException
{
	/// <summary>
	/// Initializes a new instance of <see cref="IncompleteRenameIndexStatementException"/> class.
	/// </summary>
	/// <param name="index">Name of the index being renamed.</param>
	public IncompleteRenameIndexStatementException(string index)
		: base($"Rename index statement for index {index} is incomplete.")
	{
	}
}
