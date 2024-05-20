namespace Puro.Statements.Alter.Table;

/// <summary>
/// Migration statement for altering an existing table.
/// </summary>
public interface IAlterTableMigrationStatement : IMigrationStatement
{
	/// <summary>
	/// Gets the name of the schema.
	/// </summary>
	public string? Schema { get; }

	/// <summary>
	/// Gets the name of the table.
	/// </summary>
	public string Table { get; }

	/// <summary>
	/// Gets the names of the columns to drop.
	/// </summary>
	public IReadOnlyList<string> DropColumns { get; }
}
