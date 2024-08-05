namespace Puro.Statements.Alter.Table.ColumnChanges;

/// <summary>
/// Methods for defining string column length.
/// </summary>
public interface IDefineColumnStringTypeStatement : IDefineColumnTypeStatement
{
	/// <summary>
	/// Defines the string column to have a fixed length.
	/// </summary>
	/// <param name="length">Fixed length of the column.</param>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public IDefineColumnTypeStatement FixedLength(int length);

	/// <summary>
	/// Defines the string column to have a maximum length.
	/// </summary>
	/// <param name="length">Maximum length of the column.</param>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public IDefineColumnTypeStatement MaximumLength(int length);
}