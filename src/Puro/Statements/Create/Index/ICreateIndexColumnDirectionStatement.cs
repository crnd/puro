namespace Puro.Statements.Create.Index;

/// <summary>
/// Methods for defining a filter for an index.
/// </summary>
public interface ICreateIndexColumnDirectionStatement : ICreateIndexTableSchemaStatement
{
	/// <summary>
	/// Defines a filter for the index.
	/// </summary>
	/// <param name="filter">Filter definition.</param>
	public void WithFilter(string filter);
}