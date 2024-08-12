namespace Puro.Statements.Rename.Column;

/// <summary>
/// Methods for defining the table.
/// </summary>
public interface IRenameColumnStatement
{
	/// <summary>
	/// Defines the table for the rename column statement.
	/// </summary>
	/// <param name="name">Name of the table.</param>
	/// <returns>Interface for defining the schema.</returns>
	public IRenameColumnInTableStatement InTable(string name);
}
