namespace Puro.Statements.Rename.Table;

/// <summary>
/// Migration statement for renaming a table.
/// </summary>
public interface IRenameTableMigrationStatement : IMigrationStatement
{
	/// <summary>
	/// Gets the name of the schema.
	/// </summary>
	public string? Schema { get; }

	/// <summary>
	/// Gets the current name of the table.
	/// </summary>
	public string CurrentName { get; }

	/// <summary>
	/// Gets the new name of the table.
	/// </summary>
	public string? NewName { get; }
}