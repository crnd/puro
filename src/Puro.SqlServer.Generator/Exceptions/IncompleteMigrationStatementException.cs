using Puro.Exceptions;

namespace Puro.SqlServer.Generator.Exceptions;

/// <summary>
/// Base exception class to use for incomplete migration statement exceptions.
/// </summary>
public abstract class IncompleteMigrationStatementException : PuroException
{
	/// <summary>
	/// Initializes a new instance of <see cref="IncompleteMigrationStatementException"/> class with a specified error message.
	/// </summary>
	/// <param name="message">Error message.</param>
	protected IncompleteMigrationStatementException(string message)
		: base(message)
	{
	}
}
