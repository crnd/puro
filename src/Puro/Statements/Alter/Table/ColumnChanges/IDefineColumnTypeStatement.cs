namespace Puro.Statements.Alter.Table.ColumnChanges;

/// <summary>
/// Methods for defining column nullability.
/// </summary>
public interface IDefineColumnTypeStatement
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