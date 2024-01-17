namespace Puro.Statements.Create.Index;

/// <summary>
/// Methods to define the sorting order for a column.
/// </summary>
public interface ICreateIndexColumnStatement
{
	/// <summary>
	/// Defines column sorting order as ascending.
	/// </summary>
	/// <returns>Interface for defining a filter.</returns>
	public ICreateIndexColumnDirectionStatement Ascending();

	/// <summary>
	/// Defines column sorting order as descending.
	/// </summary>
	/// <returns>Interface for defining a filter.</returns>
	public ICreateIndexColumnDirectionStatement Descending();
}