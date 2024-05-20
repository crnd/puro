namespace Puro.Statements.Alter.Table;

/// <summary>
/// Methods for dropping columns from a table.
/// </summary>
public interface IDropColumnStatement
{
	/// <summary>
	/// Drops a column from the table.
	/// </summary>
	/// <param name="name">Name of the column.</param>
	/// <returns>Interface for dropping more columns.</returns>
	public IDropColumnStatement DropColumn(string name);
}