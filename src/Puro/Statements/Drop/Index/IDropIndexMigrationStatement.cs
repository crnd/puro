namespace Puro.Statements.Drop.Index;

/// <summary>
/// Migration statement for dropping an index.
/// </summary>
public interface IDropIndexMigrationStatement : IMigrationStatement
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
	/// Gets the name of the index.
	/// </summary>
	public string Index { get; }
}
