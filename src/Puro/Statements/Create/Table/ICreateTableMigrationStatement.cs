namespace Puro.Statements.Create.Table;

/// <summary>
/// Migration statement for creating a new table.
/// </summary>
public interface ICreateTableMigrationStatement : IMigrationStatement
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
	/// Gets the columns for the table.
	/// </summary>
	public IReadOnlyList<ITableColumn> Columns { get; }
}