namespace Puro.Statements.Create.PrimaryKey;

/// <summary>
/// Methods for defining the schema.
/// </summary>
public interface ICreatePrimaryKeyTableStatement
{
	/// <summary>
	/// Defines the schema for the primary key constraint.
	/// </summary>
	/// <param name="name">Name of the schema.</param>
	/// <returns>Interface for defining the columns.</returns>
	public ICreatePrimaryKeySchemaStatement InSchema(string name);
}