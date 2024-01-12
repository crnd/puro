using Puro.Exceptions;

namespace Puro.SqlServer.Generator.Exceptions;

/// <summary>
/// Exception that is thrown when decimal scale definition of a column is invalid.
/// </summary>
public class InvalidDecimalScaleException : PuroException
{
	/// <summary>
	/// Initializes a new instance of the <see cref="InvalidDecimalScaleException"/> class.
	/// </summary>
	/// <param name="scale">Decimal scale value.</param>
	public InvalidDecimalScaleException(int scale)
		: base($"Decimal scale {scale} value is negative. ")
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="InvalidDecimalScaleException"/> class.
	/// </summary>
	/// <param name="scale">Decimal scale value.</param>
	/// <param name="precision">Decimal precision value.</param>
	public InvalidDecimalScaleException(int scale, int precision)
		: base($"Decimal scale {scale} value is bigger than precision {precision} value.")
	{
	}
}
