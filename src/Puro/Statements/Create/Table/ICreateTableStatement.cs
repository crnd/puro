namespace Puro.Statements.Create.Table;

/// <summary>
/// Methods for defining the schema for the table.
/// </summary>
public interface ICreateTableStatement : ICreateTableSchemaStatement
{
	/// <summary>
	/// Defines the schema for the table.
	/// </summary>
	/// <param name="name">Name of the schema.</param>
	/// <returns>Interface to define columns.</returns>
	public ICreateTableSchemaStatement InSchema(string name);
}
