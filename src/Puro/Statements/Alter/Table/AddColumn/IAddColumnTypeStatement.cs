namespace Puro.Statements.Alter.Table.AddColumn;

/// <summary>
/// Methods for defining column type.
/// </summary>
public interface IAddColumnTypeStatement
{
	/// <summary>
	/// Defines the column type as boolean.
	/// </summary>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public IAlterTableAddColumnTypeStatement AsBool();

	/// <summary>
	/// Defines the column type as short integer.
	/// </summary>
	/// <returns>Interface for defining identity column.</returns>
	public IAlterTableAddColumnTypeStatement AsShort();

	/// <summary>
	/// Defines the column type as integer.
	/// </summary>
	/// <returns>Interface for defining identity column.</returns>
	public IAlterTableAddColumnTypeStatement AsInt();

	/// <summary>
	/// Defines the column type as long integer.
	/// </summary>
	/// <returns>Interface for defining identity column.</returns>
	public IAlterTableAddColumnTypeStatement AsLong();

	/// <summary>
	/// Defines the column type as double.
	/// </summary>
	/// <returns>Interface for defining identity column.</returns>
	public IAlterTableAddColumnTypeStatement AsDouble();

	/// <summary>
	/// Defines the column type as decimal.
	/// </summary>
	/// <returns>Interface for defining decimal column precision.</returns>
	public IAlterTableAddColumnDecimalTypeStatement AsDecimal();

	/// <summary>
	/// Defines the column type as guid.
	/// </summary>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public IAlterTableAddColumnTypeStatement AsGuid();

	/// <summary>
	/// Defines the column type as string.
	/// </summary>
	/// <returns>Interface for defining string column length.</returns>
	public IAlterTableAddColumnStringTypeStatement AsString();

	/// <summary>
	/// Defines the column type as date.
	/// </summary>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public IAlterTableAddColumnTypeStatement AsDate();

	/// <summary>
	/// Defines the column type as time.
	/// </summary>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public IAlterTableAddColumnTypeStatement AsTime();

	/// <summary>
	/// Defines the column type as date time.
	/// </summary>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public IAlterTableAddColumnTypeStatement AsDateTime();

	/// <summary>
	/// Defines the column type as date time with offset.
	/// </summary>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public IAlterTableAddColumnTypeStatement AsDateTimeOffset();
}