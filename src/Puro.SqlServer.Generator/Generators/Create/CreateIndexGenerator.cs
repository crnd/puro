using Puro.SqlServer.Generator.Exceptions;
using Puro.Statements.Create.Index;
using System.Text;

namespace Puro.SqlServer.Generator.Generators.Create;

/// <summary>
/// T-SQL generator for create index statements.
/// </summary>
internal static class CreateIndexGenerator
{
	/// <summary>
	/// Generates T-SQL from <paramref name="statement"/> to create an index.
	/// </summary>
	/// <param name="statement">Migration statement definition.</param>
	/// <returns>T-SQL for creating an index.</returns>
	/// <exception cref="IncompleteCreateIndexStatementException">Thrown if <paramref name="statement"/> is not correctly defined.</exception>
	public static string Generate(ICreateIndexMigrationStatement statement)
	{
		if (!IsComplete(statement))
		{
			throw new IncompleteCreateIndexStatementException(statement.Index);
		}

		var builder = new StringBuilder();
		if (statement.Unique)
		{
			builder.AppendLine($"CREATE UNIQUE INDEX [{statement.Index}]");
		}
		else
		{
			builder.AppendLine($"CREATE INDEX [{statement.Index}]");
		}

		builder.Append($"ON [{statement.Schema}].[{statement.Table}] ([{BuildColumns(statement.Columns)})");

		if (statement.Filter is not null)
		{
			builder.AppendLine().Append($"WHERE {statement.Filter}");
		}

		return builder.Append(';').ToString();
	}

	private static bool IsComplete(ICreateIndexMigrationStatement statement)
	{
		if (statement.Schema is null || statement.Table is null || statement.Columns.Count == 0)
		{
			return false;
		}

		if (statement.Columns.Any(c => c.Descending is null))
		{
			return false;
		}

		return true;
	}

	private static string BuildColumns(IReadOnlyList<IIndexColumn> indexColumns)
	{
		var columns = indexColumns
			.Select(c => $"{c.Name}] {(c.Descending!.Value ? "DESC" : "ASC")}")
			.ToArray();

		return string.Join(", [", columns);
	}
}
