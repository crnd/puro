namespace Puro.Statements.Create.Table;

/// <summary>
/// Methods for defining identity columns.
/// </summary>
public interface ICreateTableColumnNumberTypeStatement : ICreateTableColumnTypeStatement
{
	/// <summary>
	/// Defines the column as an identity column.
	/// </summary>
	/// <returns>Interface for defining a column.</returns>
	public ICreateTableSchemaStatement Identity();
}