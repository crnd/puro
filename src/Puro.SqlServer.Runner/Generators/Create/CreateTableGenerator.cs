using Puro.Exceptions;
using Puro.SqlServer.Runner.Exceptions;
using Puro.Statements.Create.Table;
using System.Text;

namespace Puro.SqlServer.Runner.Generators.Create;

/// <summary>
/// T-SQL generator for create table statements.
/// </summary>
internal static class CreateTableGenerator
{
	/// <summary>
	/// Generates T-SQL from <paramref name="statement"/> to create a table.
	/// </summary>
	/// <param name="statement">Migration statement definition.</param>
	/// <returns>T-SQL for creating a table.</returns>
	/// <exception cref="IncompleteCreateTableStatementException">Thrown if <paramref name="statement"/> is not correctly defined.</exception>
	/// <exception cref="MultipleIdentityColumnsException">Thrown if <paramref name="statement"/> contains more than one identity column.</exception>
	/// <exception cref="InvalidDecimalPrecisionException">Thrown if <paramref name="statement"/> contains a decimal column with invalid precision.</exception>
	/// <exception cref="InvalidDecimalScaleException">Thrown if <paramref name="statement"/> contains a decimal column with invalid scale.</exception>
	/// <exception cref="InvalidStringLengthException">Thrown if <paramref name="statement"/> contains a string column with invalid length.</exception>
	public static string Generate(ICreateTableMigrationStatement statement)
	{
		if (!IsComplete(statement))
		{
			throw new IncompleteCreateTableStatementException(statement.Table);
		}

		var builder = new StringBuilder($"CREATE TABLE [{statement.Schema}].[{statement.Table}] (").AppendLine();

		var columns = statement.Columns
			.Select(ColumnGenerator.BuildColumnRowSql)
			.ToList();

		builder.Append(string.Join("," + Environment.NewLine, columns));

		return builder.Append(");").ToString();
	}

	private static bool IsComplete(ICreateTableMigrationStatement statement)
	{
		if (statement.Schema is null || statement.Columns.Count == 0)
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
