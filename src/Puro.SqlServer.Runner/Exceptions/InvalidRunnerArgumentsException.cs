using Puro.Exceptions;

namespace Puro.SqlServer.Runner.Exceptions;

/// <summary>
/// Exception that is thrown when startup arguments are invalid.
/// </summary>
public class InvalidRunnerArgumentsException : PuroException
{
	/// <summary>
	/// Initializes a new instance of the <see cref="InvalidRunnerArgumentsException"/> class.
	/// </summary>
	public InvalidRunnerArgumentsException()
		: base("Invalid startup arguments.")
	{
	}
}
