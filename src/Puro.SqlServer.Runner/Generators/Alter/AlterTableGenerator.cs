using Puro.SqlServer.Runner.Exceptions;
using Puro.Statements.Alter.Table;
using System.Text;

namespace Puro.SqlServer.Runner.Generators.Alter;

internal static class AlterTableGenerator
{
	public static string Generate(IAlterTableMigrationStatement statement, string schema)
	{
		if (!IsComplete(statement))
		{
			throw new IncompleteAlterTableStatementException(statement.Table);
		}

		if (string.IsNullOrWhiteSpace(schema))
		{
			throw new ArgumentNullException(nameof(schema));
		}

		var tableSql = $"ALTER TABLE [{statement.Schema ?? schema}].[{statement.Table}]" + Environment.NewLine;
		var builder = new StringBuilder(tableSql);

		TableColumnChangeType? previousChangeType = null;

		foreach (var (changeType, column) in statement.ColumnChanges)
		{
			switch (changeType)
			{
				case TableColumnChangeType.Add:
					if (previousChangeType is not null && previousChangeType != TableColumnChangeType.Add)
					{
						builder.AppendLine(";").AppendLine();
						builder.Append(tableSql);
					}

					if (previousChangeType != TableColumnChangeType.Add)
					{
						builder.AppendLine("ADD");
					}
					else
					{
						builder.Append(',').AppendLine();
					}

					builder.Append(ColumnGenerator.BuildColumnRowSql(column));
					break;
				case TableColumnChangeType.Alter:
					if (previousChangeType is not null)
					{
						builder.AppendLine(";").AppendLine();
						builder.Append(tableSql);
					}

					builder.Append($"ALTER COLUMN {ColumnGenerator.BuildColumnRowSql(column)}");
					break;
				case TableColumnChangeType.Drop:
					if (previousChangeType is null)
					{
						builder.Append("DROP COLUMN");
					}
					else if (previousChangeType == TableColumnChangeType.Drop)
					{
						builder.Append(',');
					}
					else
					{
						builder.AppendLine(";").AppendLine();
						builder.Append(tableSql);
						builder.Append("DROP COLUMN");
					}

					builder.Append($" [{column.Name}]");

					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(statement));
			}

			previousChangeType = changeType;
		}

		return builder.Append(';').ToString();
	}

	private static bool IsComplete(IAlterTableMigrationStatement statement)
	{
		if (statement.ColumnChanges.Count == 0)
		{
			return false;
		}

		var incompleteColumnChangeDefinitions = statement.ColumnChanges
			.Where(c => c.ChangeType == TableColumnChangeType.Add || c.ChangeType == TableColumnChangeType.Alter)
			.Any(c => !ColumnGenerator.ColumnIsComplete(c.Column));
		if (incompleteColumnChangeDefinitions)
		{
			return false;
		}

		return true;
	}
}
