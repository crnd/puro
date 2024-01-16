namespace Puro.Statements.Create.Index;

/// <summary>
/// Migration statement for creating a new index.
/// </summary>
public interface ICreateIndexMigrationStatement : IMigrationStatement
{
	/// <summary>
	/// Gets the name of the index.
	/// </summary>
	public string Index { get; }

	/// <summary>
	/// Gets the name of the table
	/// </summary>
	public string? Table { get; }

	/// <summary>
	/// Gets the name of the schema.
	/// </summary>
	public string? Schema { get; }

	/// <summary>
	/// True if the index is a unique index.
	/// </summary>
	public bool Unique { get; }

	/// <summary>
	/// Gets the filter if one has been defined.
	/// </summary>
	public string? Filter { get; }

	/// <summary>
	/// Gets the columns for the index.
	/// </summary>
	public IReadOnlyList<IIndexColumn> Columns { get; }
}