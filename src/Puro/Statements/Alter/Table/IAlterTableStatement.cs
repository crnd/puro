namespace Puro.Statements.Alter.Table;

/// <summary>
/// Methods for defining the schema for the table.
/// </summary>
public interface IAlterTableStatement : IAlterTableSchemaStatement
{
	/// <summary>
	/// Defines the schema for the table.
	/// </summary>
	/// <param name="name">Name of the schema.</param>
	/// <returns>Interface to define modifications to the table.</returns>
	public IAlterTableSchemaStatement InSchema(string name);
}
