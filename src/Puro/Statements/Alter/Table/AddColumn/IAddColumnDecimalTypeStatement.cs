namespace Puro.Statements.Alter.Table.AddColumn;

/// <summary>
/// Methods for defining decimal column precision.
/// </summary>
public interface IAddColumnDecimalTypeStatement
{
	/// <summary>
	/// Defines the precision for a decimal column.
	/// </summary>
	/// <param name="precision">Precision value.</param>
	/// <returns>Interface for defining decimal column scale.</returns>
	public IAddColumnDecimalTypePrecisionStatement WithPrecision(short precision);
}