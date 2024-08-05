namespace Puro.Statements.Alter.Table.ColumnChanges;

/// <summary>
/// Methods for defining decimal column scale.
/// </summary>
public interface IDefineColumnDecimalTypePrecisionStatement
{
	/// <summary>
	/// Defines the scale for a decimal column.
	/// </summary>
	/// <param name="scale">Scale value.</param>
	/// <returns>Interface for defining identity column.</returns>
	public IDefineColumnTypeStatement WithScale(short scale);
}