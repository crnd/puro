using Puro.SqlServer.Runner.Exceptions;
using Puro.Statements.Create.Table;
using System.Text;

namespace Puro.SqlServer.Runner.Generators.Create;

internal static class CreateTableGenerator
{
	public static string Generate(ICreateTableMigrationStatement statement, string schema)
	{
		if (!IsComplete(statement))
		{
			throw new IncompleteCreateTableStatementException(statement.Table);
		}

		if (string.IsNullOrWhiteSpace(schema))
		{
			throw new ArgumentNullException(nameof(schema));
		}

		var builder = new StringBuilder($"CREATE TABLE [{statement.Schema ?? schema}].[{statement.Table}] (").AppendLine();

		var columns = statement.Columns
			.Select(ColumnGenerator.BuildColumnRowSql)
			.ToList();

		builder.Append(string.Join("," + Environment.NewLine, columns));

		return builder.Append(");").ToString();
	}

	private static bool IsComplete(ICreateTableMigrationStatement statement)
	{
		if (statement.Columns.Count == 0)
		{
			return false;
		}

		if (statement.Columns.Any(c => !ColumnGenerator.ColumnIsComplete(c)))
		{
			return false;
		}

		// SQL Server does not support more than one identity column per table.
		var identityColumns = statement.Columns.Count(c => c.Identity);
		if (identityColumns > 1)
		{
			throw new MultipleIdentityColumnsException(statement.Table);
		}

		return true;
	}
}
