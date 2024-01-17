namespace Puro.Statements.Create.Index;

/// <summary>
/// Methods to define a column for an index.
/// </summary>
public interface ICreateIndexTableSchemaStatement
{
	/// <summary>
	/// Defines a column for the index.
	/// </summary>
	/// <param name="name">Name of the column.</param>
	/// <returns>Interface to define the sorting order for the column.</returns>
	public ICreateIndexColumnStatement OnColumn(string name);
}