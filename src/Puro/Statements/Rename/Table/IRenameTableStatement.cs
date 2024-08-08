namespace Puro.Statements.Rename.Table;

/// <summary>
/// Methods for defining the schema.
/// </summary>
public interface IRenameTableStatement
{
	/// <summary>
	/// Defines the schema for the rename table statement.
	/// </summary>
	/// <param name="name">Name of the schema.</param>
	public IRenameTableInSchemaStatement InSchema(string name);
}