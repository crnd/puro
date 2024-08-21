namespace Puro.Statements.Rename.Table;

/// <summary>
/// Methods for defining the schema.
/// </summary>
public interface IRenameTableStatement : IRenameTableInSchemaStatement
{
	/// <summary>
	/// Defines the schema for the rename table statement.
	/// </summary>
	/// <param name="name">Name of the schema.</param>
	/// <returns>Interface for defining the new name for the table.</returns>
	public IRenameTableInSchemaStatement InSchema(string name);
}