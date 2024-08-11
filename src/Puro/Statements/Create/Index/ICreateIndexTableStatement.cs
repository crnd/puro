namespace Puro.Statements.Create.Index;

/// <summary>
/// Methods to define a schema for a table where an index belongs to.
/// </summary>
public interface ICreateIndexTableStatement : ICreateIndexTableSchemaStatement
{
	/// <summary>
	/// Defines the schema for the table where the index belongs to.
	/// </summary>
	/// <param name="name">Name of the schema.</param>
	/// <returns>Interface to define a column for the index.</returns>
	public ICreateIndexTableSchemaStatement InSchema(string name);
}