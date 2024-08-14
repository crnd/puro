namespace Puro.Statements.Rename.Index;

/// <summary>
/// Methods for defining the schema.
/// </summary>
public interface IRenameIndexInTableStatement : IRenameIndexInSchemaStatement
{
	/// <summary>
	/// Defines the schema for the rename index statement.
	/// </summary>
	/// <param name="name">Name of the schema.</param>
	/// <returns>Interface for defining the new name for the index.</returns>
	public IRenameIndexInSchemaStatement InSchema(string name);
}