namespace Puro.Statements.Rename.Column;

/// <summary>
/// Methods for defining the new name for the column.
/// </summary>
public interface IRenameColumnInSchemaStatement
{
	/// <summary>
	/// Defines the new column name for the rename column statement.
	/// </summary>
	/// <param name="name">New name for the column.</param>
	public void To(string name);
}