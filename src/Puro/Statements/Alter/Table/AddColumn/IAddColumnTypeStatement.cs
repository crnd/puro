namespace Puro.Statements.Alter.Table.AddColumn;

/// <summary>
/// Methods for defining column nullability.
/// </summary>
public interface IAddColumnTypeStatement
{
	/// <summary>
	/// Defines that the column can contain null values.
	/// </summary>
	/// <returns>Interface to define columns.</returns>
	public IAlterTableSchemaStatement Null();

	/// <summary>
	/// Defines that the column cannot contain null values.
	/// </summary>
	/// <returns>Interface to define columns.</returns>
	public IAlterTableSchemaStatement NotNull();
}