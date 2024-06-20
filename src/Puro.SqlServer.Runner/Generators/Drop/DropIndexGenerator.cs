using Puro.SqlServer.Runner.Exceptions;
using Puro.Statements.Drop.Index;

namespace Puro.SqlServer.Runner.Generators.Drop;

/// <summary>
/// T-SQL generator for drop index statements.
/// </summary>
internal static class DropIndexGenerator
{
	/// <summary>
	/// Generates T-SQL from <paramref name="statement"/> to drop an index.
	/// </summary>
	/// <param name="statement">Migration statement definition.</param>
	/// <returns>T-SQL for dropping the defined index.</returns>
	/// <exception cref="IncompleteDropIndexStatementException">Thrown if <paramref name="statement"/> is not correctly defined.</exception>
	public static string Generate(IDropIndexMigrationStatement statement)
	{
		if (statement.Schema is null || statement.Table is null)
		{
			throw new IncompleteDropIndexStatementException(statement.Index);
		}

		return $"DROP INDEX [{statement.Index}] ON [{statement.Schema}].[{statement.Table}];";
	}
}
