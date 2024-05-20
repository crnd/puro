namespace Puro.Exceptions;

/// <summary>
/// Exception that is thrown when an alter table statement tries to drop the same column multiple times.
/// </summary>
public class DuplicateDropColumnException : PuroException
{
	/// <summary>
	/// Initializes a new instance of the <see cref="DuplicateDropColumnException"/> class.
	/// </summary>
	/// <param name="columnName">Name of the column.</param>
	public DuplicateDropColumnException(string columnName)
		: base($"Duplicate drop column statements for column {columnName}.")
	{
	}
}
