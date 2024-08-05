namespace Puro.Statements.Alter.Table.ColumnChanges;

/// <summary>
/// Methods for defining column type.
/// </summary>
public interface IDefineColumnStatement
{
	/// <summary>
	/// Defines the column type as boolean.
	/// </summary>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public IDefineColumnTypeStatement AsBool();

	/// <summary>
	/// Defines the column type as short integer.
	/// </summary>
	/// <returns>Interface for defining identity column.</returns>
	public IDefineColumnTypeStatement AsShort();

	/// <summary>
	/// Defines the column type as integer.
	/// </summary>
	/// <returns>Interface for defining identity column.</returns>
	public IDefineColumnTypeStatement AsInt();

	/// <summary>
	/// Defines the column type as long integer.
	/// </summary>
	/// <returns>Interface for defining identity column.</returns>
	public IDefineColumnTypeStatement AsLong();

	/// <summary>
	/// Defines the column type as double.
	/// </summary>
	/// <returns>Interface for defining identity column.</returns>
	public IDefineColumnTypeStatement AsDouble();

	/// <summary>
	/// Defines the column type as decimal.
	/// </summary>
	/// <returns>Interface for defining decimal column precision.</returns>
	public IDefineColumnDecimalTypeStatement AsDecimal();

	/// <summary>
	/// Defines the column type as guid.
	/// </summary>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public IDefineColumnTypeStatement AsGuid();

	/// <summary>
	/// Defines the column type as string.
	/// </summary>
	/// <returns>Interface for defining string column length.</returns>
	public IDefineColumnStringTypeStatement AsString();

	/// <summary>
	/// Defines the column type as date.
	/// </summary>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public IDefineColumnTypeStatement AsDate();

	/// <summary>
	/// Defines the column type as time.
	/// </summary>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public IDefineColumnTypeStatement AsTime();

	/// <summary>
	/// Defines the column type as date time.
	/// </summary>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public IDefineColumnTypeStatement AsDateTime();

	/// <summary>
	/// Defines the column type as date time with offset.
	/// </summary>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public IDefineColumnTypeStatement AsDateTimeOffset();
}