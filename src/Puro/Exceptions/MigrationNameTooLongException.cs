namespace Puro.Exceptions;

/// <summary>
/// Exception that is thrown when migration name is too long.
/// </summary>
public class MigrationNameTooLongException : PuroException
{
	/// <summary>
	/// Initializes a new instance of the <see cref="MigrationNameTooLongException"/> class.
	/// </summary>
	/// <param name="maximumLength">Maximum allowed migration name length.</param>
	public MigrationNameTooLongException(int maximumLength)
		: base($"Maximum migration name length is {maximumLength} characters.")
	{
	}
}
