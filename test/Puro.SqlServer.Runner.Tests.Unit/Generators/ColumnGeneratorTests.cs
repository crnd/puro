using NSubstitute;
using Puro.Exceptions;
using Puro.SqlServer.Runner.Exceptions;
using Puro.SqlServer.Runner.Generators;
using Puro.Statements;
using Xunit;

namespace Puro.SqlServer.Runner.Tests.Unit.Generators;

public class ColumnGeneratorTests
{
	[Fact]
	public void IncompleteColumnWhenTypeIsNotDefined()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(true);

		Assert.False(ColumnGenerator.ColumnIsComplete(column));
	}

	[Fact]
	public void IncompleteColumnWhenNullabilityIsNotDefined()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(string));

		Assert.False(ColumnGenerator.ColumnIsComplete(column));
	}

	[Fact]
	public void IncompleteDecimalColumnWhenPrecisionAndScaleNotDefined()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(true);
		column.Type.Returns(typeof(decimal));

		Assert.False(ColumnGenerator.ColumnIsComplete(column));
	}

	[Fact]
	public void IncompleteDecimalColumnWhenPrecisionNotDefined()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(true);
		column.Type.Returns(typeof(decimal));
		column.Scale.Returns(3);

		Assert.False(ColumnGenerator.ColumnIsComplete(column));
	}

	[Fact]
	public void IncompleteDecimalColumnWhenScaleNotDefined()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(true);
		column.Type.Returns(typeof(decimal));
		column.Precision.Returns(3);

		Assert.False(ColumnGenerator.ColumnIsComplete(column));
	}

	[Theory]
	[InlineData(typeof(bool), null, null)]
	[InlineData(typeof(short), null, null)]
	[InlineData(typeof(int), null, null)]
	[InlineData(typeof(long), null, null)]
	[InlineData(typeof(double), null, null)]
	[InlineData(typeof(decimal), 5, 2)]
	[InlineData(typeof(string), null, null)]
	[InlineData(typeof(Guid), null, null)]
	[InlineData(typeof(DateOnly), null, null)]
	[InlineData(typeof(TimeOnly), null, null)]
	[InlineData(typeof(DateTime), null, null)]
	[InlineData(typeof(DateTimeOffset), null, null)]
	public void CompleteColumn(Type columnType, int? precision, int? scale)
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(true);
		column.Type.Returns(columnType);
		column.Precision.Returns(precision);
		column.Scale.Returns(scale);

		Assert.True(ColumnGenerator.ColumnIsComplete(column));
	}

	[Theory]
	[InlineData(typeof(object))]
	[InlineData(typeof(sbyte))]
	[InlineData(typeof(byte))]
	[InlineData(typeof(ushort))]
	[InlineData(typeof(uint))]
	[InlineData(typeof(ulong))]
	[InlineData(typeof(char))]
	[InlineData(typeof(float))]
	[InlineData(typeof(nint))]
	[InlineData(typeof(nuint))]
	public void ColumnBuildUnsupportedTypeThrows(Type columnType)
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(true);
		column.Type.Returns(columnType);

		Assert.Throws<ArgumentOutOfRangeException>(() => ColumnGenerator.BuildColumnRowSql(column));
	}

	[Fact]
	public void ColumnBuildNonPositiveDecimalPrecisionThrows()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(true);
		column.Type.Returns(typeof(decimal));
		column.Precision.Returns(0);
		column.Scale.Returns(2);

		Assert.Throws<InvalidDecimalPrecisionException>(() => ColumnGenerator.BuildColumnRowSql(column));
	}

	[Fact]
	public void ColumnBuildTooBigDecimalPrecisionThrows()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(true);
		column.Type.Returns(typeof(decimal));
		column.Precision.Returns(39);
		column.Scale.Returns(2);

		Assert.Throws<InvalidDecimalPrecisionException>(() => ColumnGenerator.BuildColumnRowSql(column));
	}

	[Fact]
	public void ColumnBuildNegativeDecimalScaleThrows()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(true);
		column.Type.Returns(typeof(decimal));
		column.Precision.Returns(5);
		column.Scale.Returns(-1);

		Assert.Throws<InvalidDecimalScaleException>(() => ColumnGenerator.BuildColumnRowSql(column));
	}

	[Fact]
	public void ColumnBuildDecimalScaleBiggerThanPrecisionThrows()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(true);
		column.Type.Returns(typeof(decimal));
		column.Precision.Returns(5);
		column.Scale.Returns(6);

		Assert.Throws<InvalidDecimalScaleException>(() => ColumnGenerator.BuildColumnRowSql(column));
	}

	[Fact]
	public void ColumnBuildStringFixedLengthTooLongThrows()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(true);
		column.Type.Returns(typeof(string));
		column.FixedLength.Returns(5000);

		Assert.Throws<InvalidStringLengthException>(() => ColumnGenerator.BuildColumnRowSql(column));
	}

	[Fact]
	public void ColumnBuildStringMaximumLengthTooLongThrows()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(true);
		column.Type.Returns(typeof(string));
		column.MaximumLength.Returns(5000);

		Assert.Throws<InvalidStringLengthException>(() => ColumnGenerator.BuildColumnRowSql(column));
	}

	[Fact]
	public void GeneratedNonNullableBoolColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(false);
		column.Type.Returns(typeof(bool));

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] BIT NOT NULL", sql);
	}

	[Fact]
	public void GeneratedNullableBoolColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(true);
		column.Type.Returns(typeof(bool));

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] BIT NULL", sql);
	}

	[Fact]
	public void GeneratedNonNullableShortColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(false);
		column.Type.Returns(typeof(short));

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] SMALLINT NOT NULL", sql);
	}

	[Fact]
	public void GeneratedNonNullableShortIdentityColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(false);
		column.Type.Returns(typeof(short));
		column.Identity.Returns(true);

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] SMALLINT NOT NULL IDENTITY(1, 1)", sql);
	}

	[Fact]
	public void GeneratedNullableShortColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(true);
		column.Type.Returns(typeof(short));

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] SMALLINT NULL", sql);
	}

	[Fact]
	public void GeneratedNonNullableIntColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(false);
		column.Type.Returns(typeof(int));

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] INT NOT NULL", sql);
	}

	[Fact]
	public void GeneratedNonNullableIntIdentityColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(false);
		column.Type.Returns(typeof(int));
		column.Identity.Returns(true);

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] INT NOT NULL IDENTITY(1, 1)", sql);
	}

	[Fact]
	public void GeneratedNullableIntColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(true);
		column.Type.Returns(typeof(int));

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] INT NULL", sql);
	}

	[Fact]
	public void GeneratedNonNullableLongColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(false);
		column.Type.Returns(typeof(long));

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] BIGINT NOT NULL", sql);
	}

	[Fact]
	public void GeneratedNonNullableLongIdentityColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(false);
		column.Type.Returns(typeof(long));
		column.Identity.Returns(true);

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] BIGINT NOT NULL IDENTITY(1, 1)", sql);
	}

	[Fact]
	public void GeneratedNullableLongColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(true);
		column.Type.Returns(typeof(long));

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] BIGINT NULL", sql);
	}

	[Fact]
	public void GeneratedNonNullableDoubleColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(false);
		column.Type.Returns(typeof(double));

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] FLOAT(53) NOT NULL", sql);
	}

	[Fact]
	public void GeneratedNullableDoubleColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(true);
		column.Type.Returns(typeof(double));

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] FLOAT(53) NULL", sql);
	}

	[Fact]
	public void GeneratedNonNullableDecimalColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(false);
		column.Type.Returns(typeof(decimal));
		column.Precision.Returns(5);
		column.Scale.Returns(2);

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] DECIMAL(5, 2) NOT NULL", sql);
	}

	[Fact]
	public void GeneratedNonNullableDecimalIdentityColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(false);
		column.Type.Returns(typeof(decimal));
		column.Precision.Returns(5);
		column.Scale.Returns(2);
		column.Identity.Returns(true);

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] DECIMAL(5, 2) NOT NULL IDENTITY(1, 1)", sql);
	}

	[Fact]
	public void GeneratedNullableDecimalColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(true);
		column.Type.Returns(typeof(decimal));
		column.Precision.Returns(5);
		column.Scale.Returns(2);

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] DECIMAL(5, 2) NULL", sql);
	}

	[Fact]
	public void GeneratedNonNullableFixedLengthStringColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(false);
		column.Type.Returns(typeof(string));
		column.FixedLength.Returns(500);

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] NCHAR(500) NOT NULL", sql);
	}

	[Fact]
	public void GeneratedNonNullableMaximumLengthStringColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(false);
		column.Type.Returns(typeof(string));
		column.MaximumLength.Returns(200);

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] NVARCHAR(200) NOT NULL", sql);
	}

	[Fact]
	public void GeneratedNullableFixedLengthStringColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(true);
		column.Type.Returns(typeof(string));
		column.FixedLength.Returns(500);

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] NCHAR(500) NULL", sql);
	}

	[Fact]
	public void GeneratedNullableMaximumLengthStringColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(true);
		column.Type.Returns(typeof(string));
		column.MaximumLength.Returns(200);

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] NVARCHAR(200) NULL", sql);
	}

	[Fact]
	public void GeneratedNonNullableUnlimitedLengthStringColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(false);
		column.Type.Returns(typeof(string));

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] NVARCHAR(MAX) NOT NULL", sql);
	}

	[Fact]
	public void GeneratedNullableUnlimitedLengthStringColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(true);
		column.Type.Returns(typeof(string));

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] NVARCHAR(MAX) NULL", sql);
	}

	[Fact]
	public void GeneratedNonNullableGuidColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(false);
		column.Type.Returns(typeof(Guid));

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] UNIQUEIDENTIFIER NOT NULL", sql);
	}

	[Fact]
	public void GeneratedNullableGuidColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(true);
		column.Type.Returns(typeof(Guid));

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] UNIQUEIDENTIFIER NULL", sql);
	}

	[Fact]
	public void GeneratedNonNullableDateOnlyColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(false);
		column.Type.Returns(typeof(DateOnly));

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] DATE NOT NULL", sql);
	}

	[Fact]
	public void GeneratedNullableDateOnlyColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(true);
		column.Type.Returns(typeof(DateOnly));

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] DATE NULL", sql);
	}

	[Fact]
	public void GeneratedNonNullableTimeOnlyColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(false);
		column.Type.Returns(typeof(TimeOnly));

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] TIME NOT NULL", sql);
	}

	[Fact]
	public void GeneratedNullableTimeOnlyColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(true);
		column.Type.Returns(typeof(TimeOnly));

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] TIME NULL", sql);
	}

	[Fact]
	public void GeneratedNonNullableDateTimeColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(false);
		column.Type.Returns(typeof(DateTime));

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] DATETIME2 NOT NULL", sql);
	}

	[Fact]
	public void GeneratedNullableDateTimeColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(true);
		column.Type.Returns(typeof(DateTime));

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] DATETIME2 NULL", sql);
	}

	[Fact]
	public void GeneratedNonNullableDateTimeOffsetColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(false);
		column.Type.Returns(typeof(DateTimeOffset));

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] DATETIMEOFFSET NOT NULL", sql);
	}

	[Fact]
	public void GeneratedNullableDateTimeOffsetColumn()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Nullable.Returns(true);
		column.Type.Returns(typeof(DateTimeOffset));

		var sql = ColumnGenerator.BuildColumnRowSql(column);

		Assert.Equal("[column] DATETIMEOFFSET NULL", sql);
	}
}
