namespace Puro.Statements.Drop.Index;

/// <summary>
/// Methods for defining the schema.
/// </summary>
public interface IDropIndexFromTableStatement
{
	/// <summary>
	/// Defines the schema for the drop index statement.
	/// </summary>
	/// <param name="name">Name of the schema.</param>
	public void InSchema(string name);
}