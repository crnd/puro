using Puro.SqlServer.Runner.Exceptions;
using Puro.Statements;
using Puro.Statements.Alter.Table;
using System.Text;

namespace Puro.SqlServer.Runner.Generators.Alter;

/// <summary>
/// T-SQL generator for alter table statements.
/// </summary>
internal static class AlterTableGenerator
{
	/// <summary>
	/// Generates T-SQL from <paramref name="statement"/> to alter an existing table.
	/// </summary>
	/// <param name="statement">Migration statement definition.</param>
	/// <param name="schema">Schema name from the migration.</param>
	/// <returns>T-SQL for altering a table.</returns>
	/// <exception cref="IncompleteAlterTableStatementException">Thrown if <paramref name="statement"/> is not correctly defined.</exception>
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
						builder.AppendLine(tableSql);
					}

					if (previousChangeType != TableColumnChangeType.Add)
					{
						builder.AppendLine("ADD");
					}
					else
					{
						builder.Append(',').AppendLine();
					}

					builder.Append(BuildAddColumn(column));
					break;
				case TableColumnChangeType.Alter:
					if (previousChangeType is not null)
					{
						builder.AppendLine(";").AppendLine();
						builder.AppendLine(tableSql);
					}

					builder.Append($"ALTER COLUMN {BuildAltercolumn(column)}");
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
						builder.AppendLine(tableSql);
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

	private static string BuildAddColumn(ITableColumn column)
	{
		return ColumnGenerator.BuildColumnRowSql(column);
	}

	private static string BuildAltercolumn(ITableColumn column)
	{
		return ColumnGenerator.BuildColumnRowSql(column);
	}
}
