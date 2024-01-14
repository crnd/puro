namespace Puro.Statements.Create.Table;

/// <summary>
/// Methods for defining string column length.
/// </summary>
public interface ICreateTableStringColumnTypeStatement : ICreateTableColumnTypeStatement
{
	/// <summary>
	/// Defines the string column to have a fixed length.
	/// </summary>
	/// <param name="length">Fixed length of the column.</param>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public ICreateTableColumnTypeStatement FixedLength(int length);

	/// <summary>
	/// Defines the string column to have a maximum length.
	/// </summary>
	/// <param name="length">Maximum length of the column.</param>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public ICreateTableColumnTypeStatement MaximumLength(int length);
}