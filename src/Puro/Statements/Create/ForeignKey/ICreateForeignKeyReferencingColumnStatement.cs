namespace Puro.Statements.Create.ForeignKey;

/// <summary>
/// Methods for defining the referenced table.
/// </summary>
public interface ICreateForeignKeyReferencingColumnStatement : ICreateForeignKeyReferencingTableSchemaStatement
{
	/// <summary>
	/// Defines the referenced table for the foreign key constraint.
	/// </summary>
	/// <param name="name">Name of the table.</param>
	/// <returns>Interface for defining the schema.</returns>
	public ICreateForeignKeyReferencedTableStatement ToTable(string name);
}