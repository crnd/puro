namespace Puro.Exceptions;

/// <summary>
/// Exception that is thrown when a column already exists in an index..
/// </summary>
public class IndexColumnExistsException : PuroException
{
	/// <summary>
	/// Initializes a new instance of the <see cref="IndexColumnExistsException"/> class.
	/// </summary>
	/// <param name="index">Name of the index.</param>
	/// <param name="column">Name of the column.</param>
	public IndexColumnExistsException(string index, string column)
		: base($"Column {column} is included in index {index} more than once.")
	{
	}
}
