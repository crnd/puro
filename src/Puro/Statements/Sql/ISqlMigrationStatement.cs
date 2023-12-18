namespace Puro.Statements.Sql;

/// <summary>
/// Migration statement for raw SQL.
/// </summary>
public interface ISqlMigrationStatement : IMigrationStatement
{
	/// <summary>
	/// Returns the defined SQL statement.
	/// </summary>
	public string Sql { get; }
}
