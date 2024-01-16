namespace Puro.Statements.Create.Index;

/// <summary>
/// Column definition for an index.
/// </summary>
public interface IIndexColumn
{
	/// <summary>
	/// Name of the column.
	/// </summary>
	public string Name { get; }

	/// <summary>
	/// True if the sort direction for the column is descending.
	/// </summary>
	public bool? Descending { get; }
}
