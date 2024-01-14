namespace Puro.Statements.Create.Table;

/// <summary>
/// Methods for defining column nullability.
/// </summary>
public interface ICreateTableColumnTypeStatement
{
	/// <summary>
	/// Defines that the column can contain null values.
	/// </summary>
	/// <returns>Interface to define columns.</returns>
	public ICreateTableSchemaStatement Null();

	/// <summary>
	/// Defines that the column cannot contain null values.
	/// </summary>
	/// <returns>Interface to define columns.</returns>
	public ICreateTableSchemaStatement NotNull();
}