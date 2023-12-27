using Puro.SqlServer.Generator.Exceptions;
using Puro.Statements.Drop.Table;

namespace Puro.SqlServer.Generator.Generators.Drop;

/// <summary>
/// T-SQL generator for drop table statements.
/// </summary>
internal static class DropTableGenerator
{
	/// <summary>
	/// Generates T-SQL from <paramref name="statement"/> to drop a table.
	/// </summary>
	/// <param name="statement">Migration statement definition.</param>
	/// <returns>T-SQL for dropping the defined table.</returns>
	/// <exception cref="IncompleteDropTableStatementException">Thrown if <paramref name="statement"/> is not correctly defined.</exception>
	public static string Generate(IDropTableMigrationStatement statement)
	{
		if (statement.Schema is null)
		{
			throw new IncompleteDropTableStatementException(statement.Table);
		}

		return $"DROP TABLE [{statement.Schema}].[{statement.Table}];";
	}
}
