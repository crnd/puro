using Puro.SqlServer.Runner.Exceptions;
using Puro.Statements;
using Puro.Statements.Create.ForeignKey;

namespace Puro.SqlServer.Runner.Generators.Create;

internal static class CreateForeignKeyGenerator
{
	public static string Generate(ICreateForeignKeyMigrationStatement statement, string schema)
	{
		if (!IsComplete(statement))
		{
			throw new IncompleteCreateForeignKeyStatementException(statement.ForeignKey);
		}

		if (string.IsNullOrWhiteSpace(schema))
		{
			throw new ArgumentNullException(nameof(schema));
		}

		return $"""
			ALTER TABLE [{statement.ReferencingTableSchema ?? schema}].[{statement.ReferencingTable}]
			ADD CONSTRAINT [{statement.ForeignKey}] FOREIGN KEY ([{GetColumns(statement.ReferencingColumns)}])
			REFERENCES [{statement.ReferencedTableSchema ?? schema}].[{statement.ReferencedTable}] ([{GetColumns(statement.ReferencedColumns)}])
			ON DELETE {ConvertOnDeleteToString(statement.OnDelete!.Value)};
			""";
	}

	private static bool IsComplete(ICreateForeignKeyMigrationStatement statement)
	{
		if (statement.ReferencingTable is null || statement.ReferencedTable is null)
		{
			return false;
		}

		if (statement.OnDelete is null)
		{
			return false;
		}

		if (statement.ReferencedColumns.Count == 0)
		{
			return false;
		}

		return statement.ReferencingColumns.Count == statement.ReferencedColumns.Count;
	}

	private static string GetColumns(IReadOnlyList<string> columns) => string.Join("], [", columns);

	private static string ConvertOnDeleteToString(OnDeleteBehavior behavior)
	{
		return behavior switch
		{
			OnDeleteBehavior.Cascade => "CASCADE",
			OnDeleteBehavior.Restrict => "NO ACTION",
			OnDeleteBehavior.SetNull => "SET NULL",
			_ => throw new ArgumentOutOfRangeException(nameof(behavior)),
		};
	}
}
