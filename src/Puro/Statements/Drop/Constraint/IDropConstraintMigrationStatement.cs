namespace Puro.Statements.Drop.Constraint;

/// <summary>
/// Migration statement for dropping a constraint.
/// </summary>
public interface IDropConstraintMigrationStatement : IMigrationStatement
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
	/// Gets the name of the constraint.
	/// </summary>
	public string Constraint { get; }
}
