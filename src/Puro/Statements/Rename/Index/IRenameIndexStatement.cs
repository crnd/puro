namespace Puro.Statements.Rename.Index;

/// <summary>
/// Methods for defining the table.
/// </summary>
public interface IRenameIndexStatement
{
	/// <summary>
	/// Defines the table for the rename index statement.
	/// </summary>
	/// <param name="name">Name of the table.</param>
	/// <returns>Interface for defining the schema.</returns>
	public IRenameIndexInTableStatement InTable(string name);
}
