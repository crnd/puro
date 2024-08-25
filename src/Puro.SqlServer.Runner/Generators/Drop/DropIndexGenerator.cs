using Puro.SqlServer.Runner.Exceptions;
using Puro.Statements.Drop.Index;

namespace Puro.SqlServer.Runner.Generators.Drop;

internal static class DropIndexGenerator
{
	public static string Generate(IDropIndexMigrationStatement statement, string schema)
	{
		if (statement.Table is null)
		{
			throw new IncompleteDropIndexStatementException(statement.Index);
		}

		if (string.IsNullOrWhiteSpace(schema))
		{
			throw new ArgumentNullException(nameof(schema));
		}

		return $"DROP INDEX [{statement.Index}] ON [{statement.Schema ?? schema}].[{statement.Table}];";
	}
}
