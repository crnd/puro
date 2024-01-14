namespace Puro.Statements.Create.Table;

/// <summary>
/// Methods for defining decimal column precision.
/// </summary>
public interface ICreateTableDecimalColumnTypeStatement
{
	/// <summary>
	/// Defines the precision for a decimal column.
	/// </summary>
	/// <param name="precision">Precision value.</param>
	/// <returns>Interface for defining decimal column scale.</returns>
	public ICreateTableDecimalColumnTypePrecisionStatement WithPrecision(short precision);
}