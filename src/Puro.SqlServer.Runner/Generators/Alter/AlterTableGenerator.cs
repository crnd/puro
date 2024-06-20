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
	/// <returns>T-SQL for altering a table.</returns>
	/// <exception cref="IncompleteAlterTableStatementException">Thrown if <paramref name="statement"/> is not correctly defined.</exception>
	public static string Generate(IAlterTableMigrationStatement statement)
	{
		if (!IsComplete(statement))
		{
			throw new IncompleteAlterTableStatementException(statement.Table);
		}

		var tableSql = $"ALTER TABLE [{statement.Schema}].[{statement.Table}]" + Environment.NewLine;
		var builder = new StringBuilder(tableSql);

		TableColumnChangeType? previousChangeType = null;

		foreach (var change in statement.ColumnChanges)
		{
			switch (change.Type)
			{
				case TableColumnChangeType.Add:
					if (previousChangeType != TableColumnChangeType.Add && previousChangeType is not null)
					{
						builder.AppendLine(";").AppendLine();
						builder.AppendLine(tableSql);
					}

					builder.Append(BuildAddColumn(change.Column));
					break;
				case TableColumnChangeType.Alter:
					if (previousChangeType is not null)
					{
						builder.AppendLine(";").AppendLine();
						builder.AppendLine(tableSql);
					}

					builder.Append(BuildAltercolumn(change.Column));
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

					builder.Append($" [{change.Column.Name}]");

					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(statement));
			}

			previousChangeType = change.Type;
		}

		return builder.Append(';').ToString();
	}

	private static bool IsComplete(IAlterTableMigrationStatement statement)
	{
		return statement.Schema is not null && statement.ColumnChanges.Count != 0;
	}

	private static string BuildAddColumn(ITableColumn column)
	{
		throw new NotImplementedException();
	}

	private static string BuildAltercolumn(ITableColumn column)
	{
		throw new NotImplementedException();
	}
}
