namespace Puro.Statements.Drop.Table;

/// <summary>
/// Methods for defining the schema.
/// </summary>
public interface IDropTableStatement
{
	/// <summary>
	/// Defines the schema for the drop table statement.
	/// </summary>
	/// <param name="name">Name of the schema.</param>
	public void InSchema(string name);
}
