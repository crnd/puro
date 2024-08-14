namespace Puro.Statements.Rename.Index;

/// <summary>
/// Methods for defining the new name for the index.
/// </summary>
public interface IRenameIndexInSchemaStatement
{
	/// <summary>
	/// Defines the new index name for the rename column statement.
	/// </summary>
	/// <param name="name">New name for the index.</param>
	public void To(string name);
}