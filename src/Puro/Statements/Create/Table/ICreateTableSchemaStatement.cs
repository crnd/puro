namespace Puro.Statements.Create.Table;

/// <summary>
/// Methods for defining a column for a table.
/// </summary>
public interface ICreateTableSchemaStatement
{
	/// <summary>
	/// Defines a column for the table.
	/// </summary>
	/// <param name="name">Name of the column.</param>
	/// <returns>Interface for defining the column type.</returns>
	public ICreateTableColumnStatement WithColumn(string name);
}