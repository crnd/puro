namespace Puro.Exceptions;

/// <summary>
/// Exception that is thrown when a column already exists in a table.
/// </summary>
public class TableColumnExistsException : PuroException
{
	/// <summary>
	/// Initializes a new instance of the <see cref="TableColumnExistsException"/> class.
	/// </summary>
	/// <param name="table">Name of the table.</param>
	/// <param name="column">Name of the column.</param>
	public TableColumnExistsException(string table, string column)
		: base($"Column {column} is included in table {table} more than once.")
	{
	}
}
