namespace Puro.Statements.Create.Index;

/// <summary>
/// Methods to define a table for an index. 
/// </summary>
public interface ICreateIndexStatement
{
	/// <summary>
	/// Defines the table where the index belongs to.
	/// </summary>
	/// <param name="name">Name of the table.</param>
	/// <returns>Interface for defining the schema for the table.</returns>
	public ICreateIndexTableStatement OnTable(string name);
}