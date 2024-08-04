namespace Puro.Statements.Alter.Table.AddColumn;

/// <summary>
/// Methods for adding columns to a table.
/// </summary>
public interface IAlterTableAddColumnStatement
{
	/// <summary>
	/// Adds a column to the table.
	/// </summary>
	/// <param name="name">Name of the column.</param>
	/// <returns>Interface for defining the type for the column.</returns>
	public IAddColumnTypeStatement AddColumn(string name);
}