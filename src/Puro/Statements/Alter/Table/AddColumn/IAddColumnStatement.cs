namespace Puro.Statements.Alter.Table.AddColumn;

/// <summary>
/// Methods for defining column type.
/// </summary>
public interface IAddColumnStatement
{
	/// <summary>
	/// Defines the column type as boolean.
	/// </summary>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public IAddColumnTypeStatement AsBool();

	/// <summary>
	/// Defines the column type as short integer.
	/// </summary>
	/// <returns>Interface for defining identity column.</returns>
	public IAddColumnTypeStatement AsShort();

	/// <summary>
	/// Defines the column type as integer.
	/// </summary>
	/// <returns>Interface for defining identity column.</returns>
	public IAddColumnTypeStatement AsInt();

	/// <summary>
	/// Defines the column type as long integer.
	/// </summary>
	/// <returns>Interface for defining identity column.</returns>
	public IAddColumnTypeStatement AsLong();

	/// <summary>
	/// Defines the column type as double.
	/// </summary>
	/// <returns>Interface for defining identity column.</returns>
	public IAddColumnTypeStatement AsDouble();

	/// <summary>
	/// Defines the column type as decimal.
	/// </summary>
	/// <returns>Interface for defining decimal column precision.</returns>
	public IAddColumnDecimalTypeStatement AsDecimal();

	/// <summary>
	/// Defines the column type as guid.
	/// </summary>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public IAddColumnTypeStatement AsGuid();

	/// <summary>
	/// Defines the column type as string.
	/// </summary>
	/// <returns>Interface for defining string column length.</returns>
	public IAddColumnStringTypeStatement AsString();

	/// <summary>
	/// Defines the column type as date.
	/// </summary>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public IAddColumnTypeStatement AsDate();

	/// <summary>
	/// Defines the column type as time.
	/// </summary>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public IAddColumnTypeStatement AsTime();

	/// <summary>
	/// Defines the column type as date time.
	/// </summary>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public IAddColumnTypeStatement AsDateTime();

	/// <summary>
	/// Defines the column type as date time with offset.
	/// </summary>
	/// <returns>Interface for defining the nullability of the column.</returns>
	public IAddColumnTypeStatement AsDateTimeOffset();
}