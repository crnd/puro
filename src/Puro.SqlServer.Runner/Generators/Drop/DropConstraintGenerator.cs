using Puro.SqlServer.Runner.Exceptions;
using Puro.Statements.Drop.Constraint;

namespace Puro.SqlServer.Runner.Generators.Drop;

internal static class DropConstraintGenerator
{
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

		return $"""
			ALTER TABLE [{statement.Schema ?? schema}].[{statement.Table}]
			DROP CONSTRAINT [{statement.Constraint}];
			""";
	}
}
