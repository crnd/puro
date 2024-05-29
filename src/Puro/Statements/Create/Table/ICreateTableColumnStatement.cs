namespace Puro.Statements.Create.Table;

/// <summary>
/// Methods for defining column type.
/// </summary>
public interface ICreateTableColumnStatement
{
	/// <summary>
	/// Defines the column type as boolean.
	/// </summary>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public ICreateTableColumnTypeStatement AsBool();

	/// <summary>
	/// Defines the column type as short integer.
	/// </summary>
	/// <returns>Interface for defining identity column.</returns>
	public ICreateTableColumnNumberTypeStatement AsShort();

	/// <summary>
	/// Defines the column type as integer.
	/// </summary>
	/// <returns>Interface for defining identity column.</returns>
	public ICreateTableColumnNumberTypeStatement AsInt();

	/// <summary>
	/// Defines the column type as long integer.
	/// </summary>
	/// <returns>Interface for defining identity column.</returns>
	public ICreateTableColumnNumberTypeStatement AsLong();

	/// <summary>
	/// Defines the column type as double.
	/// </summary>
	/// <returns>Interface for defining identity column.</returns>
	public ICreateTableColumnTypeStatement AsDouble();

	/// <summary>
	/// Defines the column type as decimal.
	/// </summary>
	/// <returns>Interface for defining decimal column precision.</returns>
	public ICreateTableDecimalColumnTypeStatement AsDecimal();

	/// <summary>
	/// Defines the column type as guid.
	/// </summary>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public ICreateTableColumnTypeStatement AsGuid();

	/// <summary>
	/// Defines the column type as string.
	/// </summary>
	/// <returns>Interface for defining string column length.</returns>
	public ICreateTableStringColumnTypeStatement AsString();

	/// <summary>
	/// Defines the column type as date.
	/// </summary>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public ICreateTableColumnTypeStatement AsDate();

	/// <summary>
	/// Defines the column type as time.
	/// </summary>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public ICreateTableColumnTypeStatement AsTime();

	/// <summary>
	/// Defines the column type as date time.
	/// </summary>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public ICreateTableColumnTypeStatement AsDateTime();

	/// <summary>
	/// Defines the column type as date time with offset.
	/// </summary>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public ICreateTableColumnTypeStatement AsDateTimeOffset();
}