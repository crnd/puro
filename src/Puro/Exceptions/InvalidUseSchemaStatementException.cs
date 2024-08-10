namespace Puro.Exceptions;

/// <summary>
/// Exception that is thrown when use schema statement is not the first statement in the migration.
/// </summary>
public class InvalidUseSchemaStatementException : PuroException
{
	/// <summary>
	/// Initializes a new instance of the <see cref="InvalidUseSchemaStatementException"/> class.
	/// </summary>
	public InvalidUseSchemaStatementException()
		: base("Use schema statement must be the first statement in the migration.")
	{
	}
}
