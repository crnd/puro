using Puro.Exceptions;
using Puro.SqlServer.Generator.Exceptions;
using Puro.Statements;

namespace Puro.SqlServer.Generator.Generators;

internal static class ColumnGenerator
{
	public static bool ColumnIsComplete(ITableColumn column)
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

	public static string BuildColumnRowSql(ITableColumn column)
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
			var column when column.Type == typeof(string) => BuildStringType(column.FixedLength, column.MaximumLength),
			var column when column.Type == typeof(Guid) => "UNIQUEIDENTIFIER",
			var column when column.Type == typeof(DateOnly) => "DATE",
			var column when column.Type == typeof(TimeOnly) => "TIME",
			var column when column.Type == typeof(DateTime) => "DATETIME2",
			var column when column.Type == typeof(DateTimeOffset) => "DATETIMEOFFSET",
			_ => throw new ArgumentOutOfRangeException(nameof(tableColumn))
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

	private static string BuildStringType(int? fixedLength, int? maximumLength)
	{
		if (fixedLength is not null)
		{
			if (fixedLength.Value > 4000)
			{
				throw new InvalidStringLengthException(fixedLength.Value);
			}

			return $"NCHAR({fixedLength.Value})";
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
