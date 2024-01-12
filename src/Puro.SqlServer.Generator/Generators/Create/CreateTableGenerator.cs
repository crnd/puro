using Puro.Exceptions;
using Puro.SqlServer.Generator.Exceptions;
using Puro.Statements.Create.Table;
using System.Text;

namespace Puro.SqlServer.Generator.Generators.Create;

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
	public static string Generate(ICreateTableMigrationStatement statement)
	{
		if (!IsComplete(statement))
		{
			throw new IncompleteCreateTableStatementException(statement.Table);
		}

		var builder = new StringBuilder($"CREATE TABLE [{statement.Schema}].[{statement.Table}] (");

		var columns = statement.Columns
			.Select(BuildColumnRowSql)
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

		if (statement.Columns.Any(c => !ColumnIsComplete(c)))
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

	private static bool ColumnIsComplete(ITableColumn column)
	{
		if (column.Type is null || column.Nullable is null)
		{
			return false;
		}

		if (column.Type == typeof(decimal) && (column.Precision is null || column.Scale is null))
		{
			return false;
		}

		return true;
	}

	private static string BuildColumnRowSql(ITableColumn column)
	{
		var sql = $"[{column.Name}] {ColumnToSqlType(column)} ";

		if (!column.Nullable!.Value)
		{
			sql += "NOT ";
		}

		sql += "NULL";

		if (column.Identity)
		{
			sql += " IDENTITY(1, 1)";
		}

		return sql;
	}

	private static string ColumnToSqlType(ITableColumn tableColumn)
	{
		return tableColumn switch
		{
			var column when column.Type == typeof(bool) => "BIT",
			var column when column.Type == typeof(short) => "SMALLINT",
			var column when column.Type == typeof(int) => "INT",
			var column when column.Type == typeof(long) => "BIGINT",
			var column when column.Type == typeof(double) => "FLOAT(53)",
			var column when column.Type == typeof(decimal) => BuildDecimalType(column.Precision!.Value, column.Scale!.Value),
			var column when column.Type == typeof(string) => BuildStringType(column.ExactLength, column.MaximumLength),
			var column when column.Type == typeof(Guid) => "UNIQUEIDENTIFIER",
			var column when column.Type == typeof(DateOnly) => "DATE",
			var column when column.Type == typeof(TimeOnly) => "TIME",
			var column when column.Type == typeof(DateTime) => "DATETIME",
			var column when column.Type == typeof(DateTimeOffset) => "DATETIMEOFFSET",
			_ => throw new NotImplementedException()
		};
	}

	private static string BuildDecimalType(int precision, int scale)
	{
		if (precision < 1 || precision > 38)
		{
			throw new InvalidDecimalPrecisionException(precision);
		}

		if (scale < 0)
		{
			throw new InvalidDecimalScaleException(scale);
		}

		if (scale > precision)
		{
			throw new InvalidDecimalScaleException(scale, precision);
		}

		return $"DECIMAL({precision}, {scale})";
	}

	private static string BuildStringType(int? exactLength, int? maximumLength)
	{
		if (exactLength is not null)
		{
			if (exactLength.Value > 4000)
			{
				throw new InvalidStringLengthException(exactLength.Value);
			}

			return $"NCHAR({exactLength.Value})";
		}

		if (maximumLength is not null)
		{
			if (maximumLength.Value > 4000)
			{
				throw new InvalidStringLengthException(maximumLength.Value);
			}

			return $"NVARCHAR({maximumLength})";
		}

		return "NVARCHAR(MAX)";
	}
}
