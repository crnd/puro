namespace Puro.Statements.Create.ForeignKey;

/// <summary>
/// Methods for defining the delete behavior.
/// </summary>
public interface ICreateForeignKeyReferencedColumnStatement : ICreateForeignKeyReferencedTableSchemaStatement
{
	/// <summary>
	/// Defines behavior to delete all referenced entries.
	/// </summary>
	public void OnDeleteCascade();

	/// <summary>
	/// Defines behavior to prevent delete of the referencing entry.
	/// </summary>
	public void OnDeleteRestrict();

	/// <summary>
	/// Defines behavior to set the referenced columns to null.
	/// </summary>
	public void OnDeleteSetNull();
}