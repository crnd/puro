namespace Puro.Statements.Create.PrimaryKey;

/// <summary>
/// Migration statement for creating a new primary key.
/// </summary>
public interface ICreatePrimaryKeyMigrationStatement : IMigrationStatement
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
	/// Gets the name of the primary key constraint.
	/// </summary>
	public string PrimaryKey { get; }

	/// <summary>
	/// Gets the list of columns that the primary key consists of.
	/// </summary>
	public IReadOnlyList<string> Columns { get; }
}