namespace Puro.Statements.Create.PrimaryKey;

/// <summary>
/// Methods for defining the table.
/// </summary>
public interface ICreatePrimaryKeyStatement
{
	/// <summary>
	/// Defines the table for the primary key statement.
	/// </summary>
	/// <param name="name">Name of the table.</param>
	/// <returns>Interface for defining the schema.</returns>
	public ICreatePrimaryKeyTableStatement OnTable(string name);
}