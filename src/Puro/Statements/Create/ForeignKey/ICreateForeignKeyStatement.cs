namespace Puro.Statements.Create.ForeignKey;

/// <summary>
/// Methods for defining the referencing table.
/// </summary>
public interface ICreateForeignKeyStatement
{
	/// <summary>
	/// Defines the referencing table for the foreign key constraint.
	/// </summary>
	/// <param name="name">Name of the table.</param>
	/// <returns>Interface for defining the schema for the referencing table.</returns>
	public ICreateForeignKeyReferencingTableStatement FromTable(string name);
}