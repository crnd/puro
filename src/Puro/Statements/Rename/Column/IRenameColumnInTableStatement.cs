namespace Puro.Statements.Rename.Column;

/// <summary>
/// Methods for defining the schema.
/// </summary>
public interface IRenameColumnInTableStatement : IRenameColumnInSchemaStatement
{
	/// <summary>
	/// Defines the schema for the rename column statement.
	/// </summary>
	/// <param name="name">Name of the schema.</param>
	/// <returns>Interface for defining the new name for the column.</returns>
	public IRenameColumnInSchemaStatement InSchema(string name);
}