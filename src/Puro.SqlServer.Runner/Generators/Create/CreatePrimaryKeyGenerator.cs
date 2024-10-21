using Puro.SqlServer.Runner.Exceptions;
using Puro.Statements.Create.PrimaryKey;

namespace Puro.SqlServer.Runner.Generators.Create;

internal static class CreatePrimaryKeyGenerator
{
	public static string Generate(ICreatePrimaryKeyMigrationStatement statement, string schema)
	{
		if (statement.Table is null || statement.Columns.Count == 0)
		{
			throw new IncompleteCreatePrimaryKeyStatementException(statement.PrimaryKey);
		}

		if (string.IsNullOrWhiteSpace(schema))
		{
			throw new ArgumentNullException(nameof(schema));
		}

		return $"""
			ALTER TABLE [{statement.Schema ?? schema}].[{statement.Table}]
			ADD CONSTRAINT [{statement.PrimaryKey}] PRIMARY KEY CLUSTERED ([{GetColumns(statement.Columns)}]);
			""";
	}

	private static string GetColumns(IReadOnlyList<string> columns) => string.Join("], [", columns);
}
