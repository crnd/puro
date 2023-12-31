namespace Puro.Statements.Create.ForeignKey;

/// <summary>
/// Migration statement for creating a new foreign key.
/// </summary>
public interface ICreateForeignKeyMigrationStatement : IMigrationStatement
{
	/// <summary>
	/// Gets the name of the foreign key.
	/// </summary>
	public string ForeignKey { get; }

	/// <summary>
	/// Gets the name of the referencing table.
	/// </summary>
	public string? ReferencingTable { get; }

	/// <summary>
	/// Gets the schema of the referencing table.
	/// </summary>
	public string? ReferencingTableSchema { get; }

	/// <summary>
	/// Gets the names of the referencing columns.
	/// </summary>
	public IReadOnlyList<string> ReferencingColumns { get; }

	/// <summary>
	/// Gets the name of the referenced table.
	/// </summary>
	public string? ReferencedTable { get; }

	/// <summary>
	/// Gets the schema of the referenced table.
	/// </summary>
	public string? ReferencedTableSchema { get; }

	/// <summary>
	/// Gets the names of the referenced columns.
	/// </summary>
	public IReadOnlyList<string> ReferencedColumns { get; }

	/// <summary>
	/// Gets the delete behavior for the reference.
	/// </summary>
	public OnDeleteBehavior? OnDelete { get; }
}