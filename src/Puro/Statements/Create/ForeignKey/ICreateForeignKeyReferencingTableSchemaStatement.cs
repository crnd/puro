namespace Puro.Statements.Create.ForeignKey;

/// <summary>
/// Methods for defining the referencing columns.
/// </summary>
public interface ICreateForeignKeyReferencingTableSchemaStatement
{
	/// <summary>
	/// Defines a referencing column for the foreign key constraint.
	/// </summary>
	/// <param name="name">Name of the column.</param>
	/// <returns>Interface for defining the referenced table.</returns>
	public ICreateForeignKeyReferencingColumnStatement FromColumn(string name);
}