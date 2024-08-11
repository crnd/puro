namespace Puro.Statements.Create.ForeignKey;

/// <summary>
/// Methods for defining the schema for the referencing table.
/// </summary>
public interface ICreateForeignKeyReferencingTableStatement : ICreateForeignKeyReferencingTableSchemaStatement
{
	/// <summary>
	/// Defines the schema for the referencing table for the foreign key constraint.
	/// </summary>
	/// <param name="name">Name of the schema.</param>
	/// <returns>Interface for defining the referenced columns.</returns>
	public ICreateForeignKeyReferencingTableSchemaStatement FromSchema(string name);
}