namespace Puro.Statements.Create.ForeignKey;

/// <summary>
/// Methods for defining the schema for the referenced table.
/// </summary>
public interface ICreateForeignKeyReferencedTableStatement
{
	/// <summary>
	/// Defines the schema for the referenced table for the foreign key constraint.
	/// </summary>
	/// <param name="name">Name of the schema.</param>
	/// <returns>Interface for defining the referencing columns.</returns>
	public ICreateForeignKeyReferencedTableSchemaStatement ToSchema(string name);
}