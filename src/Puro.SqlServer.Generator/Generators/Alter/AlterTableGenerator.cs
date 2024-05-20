using Puro.SqlServer.Generator.Exceptions;
using Puro.Statements.Alter.Table;
using System.Text;

namespace Puro.SqlServer.Generator.Generators.Alter;

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

		var builder = new StringBuilder($"ALTER TABLE [{statement.Schema}].[{statement.Table}]").AppendLine();

		if (statement.DropColumns.Count != 0)
		{
			builder.Append("DROP COLUMN [");
			builder.Append(string.Join("], [", statement.DropColumns));
			builder.Append("];");
		}

		return builder.ToString();

	}

	private static bool IsComplete(IAlterTableMigrationStatement statement)
	{
		if (statement.Schema is null)
		{
			return false;
		}

		if (statement.DropColumns.Count == 0)
		{
			return false;
		}

		return true;
	}
}
