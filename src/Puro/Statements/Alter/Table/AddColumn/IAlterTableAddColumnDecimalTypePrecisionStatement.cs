namespace Puro.Statements.Alter.Table.AddColumn;

/// <summary>
/// Methods for defining decimal column scale.
/// </summary>
public interface IAlterTableAddColumnDecimalTypePrecisionStatement
{
	/// <summary>
	/// Defines the scale for a decimal column.
	/// </summary>
	/// <param name="scale">Scale value.</param>
	/// <returns>Interface for defining identity column.</returns>
	public IAlterTableAddColumnTypeStatement WithScale(short scale);
}