namespace Puro.Exceptions;

/// <summary>
/// Exception that is thrown when defined string length is invalid.
/// </summary>
public class InvalidStringLengthException : PuroException
{
	/// <summary>
	/// Initializes a new instance of the <see cref="InvalidStringLengthException"/> class.
	/// </summary>
	/// <param name="length">String length value.</param>
	public InvalidStringLengthException(int length)
		: base($"String length {length} is invalid.")
	{
	}
}
