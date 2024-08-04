using Puro.Statements.Alter.Table.AddColumn;

namespace Puro.Statements.Alter.Table;

/// <summary>
/// Methods for defining what to alter in a table.
/// </summary>
public interface IAlterTableSchemaStatement
{
	/// <summary>
	/// Adds a column to the table.
	/// </summary>
	/// <param name="name">Name of the column.</param>
	/// <returns>Interface for defining the type for the column.</returns>
	public IAddColumnStatement AddColumn(string name);

	/// <summary>
	/// Alters an existing column from the table.
	/// </summary>
	/// <param name="name">Name of the column.</param>
	/// <returns>Interface for defining a new type for the column.</returns>
	public IAddColumnStatement AlterColumn(string name);

	/// <summary>
	/// Drops a column from the table.
	/// </summary>
	/// <param name="name">Name of the column.</param>
	/// <returns>Interface altering a table.</returns>
	public IAlterTableSchemaStatement DropColumn(string name);
}