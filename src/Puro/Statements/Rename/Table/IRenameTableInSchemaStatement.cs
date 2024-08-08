namespace Puro.Statements.Rename.Table;

/// <summary>
/// Methods for defining a new name for a table.
/// </summary>
public interface IRenameTableInSchemaStatement
{
	/// <summary>
	/// Defines a new name for the table.
	/// </summary>
	/// <param name="name">New name for the table.</param>
	public void To(string name);
}