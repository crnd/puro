using Puro.Statements.Drop.Table;

namespace Puro.SqlServer.Runner.Generators.Drop;

/// <summary>
/// T-SQL generator for drop table statements.
/// </summary>
internal static class DropTableGenerator
{
	/// <summary>
	/// Generates T-SQL from <paramref name="statement"/> to drop a table.
	/// </summary>
	/// <param name="statement">Migration statement definition.</param>
	/// <param name="schema">Schema name from the migration.</param>
	/// <returns>T-SQL for dropping the defined table.</returns>
	public static string Generate(IDropTableMigrationStatement statement, string schema)
	{
		if (string.IsNullOrWhiteSpace(schema))
		{
			throw new ArgumentNullException(nameof(schema));
		}

		return $"DROP TABLE [{statement.Schema ?? schema}].[{statement.Table}];";
	}
}
