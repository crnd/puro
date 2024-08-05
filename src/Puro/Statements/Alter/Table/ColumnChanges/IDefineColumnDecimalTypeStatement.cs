namespace Puro.Statements.Alter.Table.ColumnChanges;

/// <summary>
/// Methods for defining decimal column precision.
/// </summary>
public interface IDefineColumnDecimalTypeStatement
{
	/// <summary>
	/// Defines the precision for a decimal column.
	/// </summary>
	/// <param name="precision">Precision value.</param>
	/// <returns>Interface for defining decimal column scale.</returns>
	public IDefineColumnDecimalTypePrecisionStatement WithPrecision(short precision);
}