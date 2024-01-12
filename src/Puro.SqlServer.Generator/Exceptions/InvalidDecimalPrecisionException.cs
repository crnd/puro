using Puro.Exceptions;

namespace Puro.SqlServer.Generator.Exceptions;

/// <summary>
/// Exception that is thrown when decimal precision definition of a column is invalid.
/// </summary>
public class InvalidDecimalPrecisionException : PuroException
{
	/// <summary>
	/// Initializes a new instance of the <see cref="InvalidDecimalPrecisionException"/> class.
	/// </summary>
	/// <param name="precision">Decimal precision value.</param>
	public InvalidDecimalPrecisionException(int precision)
		: base($"Decimal precision value {precision} is not valid. Minimum precision is 1 and maximum is 38.")
	{
	}
}
