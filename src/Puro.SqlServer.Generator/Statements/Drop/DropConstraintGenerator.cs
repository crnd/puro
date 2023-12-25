using Puro.SqlServer.Generator.Exceptions;
using Puro.Statements.Drop.Constraint;

namespace Puro.SqlServer.Generator.Statements.Drop;

/// <summary>
/// T-SQL generator for drop constraint statements.
/// </summary>
internal static class DropConstraintGenerator
{
	/// <summary>
	/// Generates T-SQL from <paramref name="statement"/> to drop a constraint.
	/// </summary>
	/// <param name="statement"></param>
	/// <returns>T-SQL for dropping the defined constraint.</returns>
	/// <exception cref="IncompleteDropConstraintStatementException">Thrown if <paramref name="statement"/> is not correctly defined.</exception>
	public static string Generate(IDropConstraintMigrationStatement statement)
	{
		if (statement.Schema is null || statement.Table is null)
		{
			throw new IncompleteDropConstraintStatementException(statement.Constraint);
		}

		return $"ALTER TABLE [{statement.Schema}].[{statement.Table}] DROP CONSTRAINT [{statement.Constraint}];";
	}
}
