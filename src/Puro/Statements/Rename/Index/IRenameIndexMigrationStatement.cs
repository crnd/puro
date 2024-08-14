namespace Puro.Statements.Rename.Index;

/// <summary>
/// Migration statement for renaming an index.
/// </summary>
public interface IRenameIndexMigrationStatement : IMigrationStatement
{
	/// <summary>
	/// Gets the name of the schema.
	/// </summary>
	public string? Schema { get; }

	/// <summary>
	/// Gets the name of the table.
	/// </summary>
	public string? Table { get; }

	/// <summary>
	/// Gets the current name of the index.
	/// </summary>
	public string CurrentName { get; }

	/// <summary>
	/// Gets the new name of the index.
	/// </summary>
	public string? NewName { get; }
}
