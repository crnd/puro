namespace Puro.Exceptions;

/// <summary>
/// Base exception class to use for custom exceptions.
/// </summary>
public abstract class PuroException : Exception
{
	/// <summary>
	/// Initializes a new instance of the <see cref="PuroException"/> class with a specified error message.
	/// </summary>
	/// <param name="message">Error message.</param>
	protected PuroException(string message)
		: base(message)
	{
	}
}
