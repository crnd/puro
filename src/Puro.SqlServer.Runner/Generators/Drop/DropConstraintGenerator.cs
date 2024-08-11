using Puro.SqlServer.Runner.Exceptions;
using Puro.Statements.Drop.Constraint;

namespace Puro.SqlServer.Runner.Generators.Drop;

/// <summary>
/// T-SQL generator for drop constraint statements.
/// </summary>
internal static class DropConstraintGenerator
{
	/// <summary>
	/// Generates T-SQL from <paramref name="statement"/> to drop a constraint.
	/// </summary>
	/// <param name="statement">Migration statement definition.</param>
	/// <param name="schema">Schema name from the migration.</param>
	/// <returns>T-SQL for dropping the defined constraint.</returns>
	/// <exception cref="IncompleteDropConstraintStatementException">Thrown if <paramref name="statement"/> is not correctly defined.</exception>
	public static string Generate(IDropConstraintMigrationStatement statement, string schema)
	{
		if (statement.Table is null)
		{
			throw new IncompleteDropConstraintStatementException(statement.Constraint);
		}

		if (string.IsNullOrWhiteSpace(schema))
		{
			throw new ArgumentNullException(nameof(schema));
		}

		return $"ALTER TABLE [{statement.Schema ?? schema}].[{statement.Table}] DROP CONSTRAINT [{statement.Constraint}];";
	}
}
