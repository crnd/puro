namespace Puro.Statements.Drop.Table;

/// <summary>
/// Migration statement for dropping a table.
/// </summary>
public interface IDropTableMigrationStatement : IMigrationStatement
{
	/// <summary>
	/// Gets the name of the schema where the table is located in.
	/// </summary>
	public string? Schema { get; }

	/// <summary>
	/// Gets the name of the table.
	/// </summary>
	public string Table { get; }
}
