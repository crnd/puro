namespace Puro.Statements.Create.ForeignKey;

/// <summary>
/// Methods for defining the referenced columns.
/// </summary>
public interface ICreateForeignKeyReferencedTableSchemaStatement
{
	/// <summary>
	/// Defines a referenced column for the foreign key constraint.
	/// </summary>
	/// <param name="name">Name of the column.</param>
	/// <returns>Interface for defining delete behaviors.</returns>
	public ICreateForeignKeyReferencedColumnStatement ToColumn(string name);
}