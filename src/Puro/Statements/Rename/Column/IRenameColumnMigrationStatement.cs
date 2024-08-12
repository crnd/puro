namespace Puro.Statements.Rename.Column;

/// <summary>
/// Migration statement for renaming a table column.
/// </summary>
public interface IRenameColumnMigrationStatement : IMigrationStatement
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
	/// Gets the current name of the table column.
	/// </summary>
	public string CurrentName { get; }

	/// <summary>
	/// Gets the new name of the table column.
	/// </summary>
	public string? NewName { get; }
}