using Puro.Exceptions;

namespace Puro.SqlServer.Runner.Exceptions;

/// <summary>
/// Exception that is thrown when table contains more than one identity column.
/// </summary>
public class MultipleIdentityColumnsException : PuroException
{
	/// <summary>
	/// Initializes a new instance of the <see cref="InvalidDecimalScaleException"/> class.
	/// </summary>
	/// <param name="table">Name of the table.</param>
	public MultipleIdentityColumnsException(string table)
		: base($"Table {table} contains more than one identity column.")
	{
	}
}
