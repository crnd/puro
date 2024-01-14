namespace Puro.Statements.Create.Table;

/// <summary>
/// Methods for defining decimal column scale.
/// </summary>
public interface ICreateTableDecimalColumnTypePrecisionStatement
{
	/// <summary>
	/// Defines the scale for a decimal column.
	/// </summary>
	/// <param name="scale">Scale value.</param>
	/// <returns>Interface for defining identity column.</returns>
	public ICreateTableColumnNumberTypeStatement WithScale(short scale);
}