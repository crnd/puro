using NSubstitute;
using Puro.Exceptions;
using Puro.SqlServer.Runner.Exceptions;
using Puro.SqlServer.Runner.Generators.Alter;
using Puro.Statements;
using Puro.Statements.Alter.Table;
using Xunit;

namespace Puro.SqlServer.Runner.Tests.Generators.Alter;

public class AlterTableGeneratorAlterColumnTests
{
	[Fact]
	public void MissingColumnTypeThrows()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		Assert.Throws<IncompleteAlterTableStatementException>(() => AlterTableGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void MissingNullabilityThrows()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(int));

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		Assert.Throws<IncompleteAlterTableStatementException>(() => AlterTableGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void MissingDecimalPrecisionThrows()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(decimal));
		column.Scale.Returns(5);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		Assert.Throws<IncompleteAlterTableStatementException>(() => AlterTableGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void MissingDecimalScaleThrows()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(decimal));
		column.Precision.Returns(5);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		Assert.Throws<IncompleteAlterTableStatementException>(() => AlterTableGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void MissingSecondColumnTypeThrows()
	{
		var column1 = Substitute.For<ITableColumn>();
		column1.Name.Returns("column1");
		column1.Type.Returns(typeof(int));
		column1.Nullable.Returns(true);

		var column2 = Substitute.For<ITableColumn>();
		column2.Name.Returns("column2");

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column1),
			(TableColumnChangeType.Alter, column2)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		Assert.Throws<IncompleteAlterTableStatementException>(() => AlterTableGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void MissingSecondColumnNullabilityThrows()
	{
		var column1 = Substitute.For<ITableColumn>();
		column1.Name.Returns("column1");
		column1.Type.Returns(typeof(int));
		column1.Nullable.Returns(true);

		var column2 = Substitute.For<ITableColumn>();
		column2.Name.Returns("column2");
		column2.Type.Returns(typeof(int));

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column1),
			(TableColumnChangeType.Alter, column2)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		Assert.Throws<IncompleteAlterTableStatementException>(() => AlterTableGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void MissingSecondColumnDecimalPrecisionThrows()
	{
		var column1 = Substitute.For<ITableColumn>();
		column1.Name.Returns("column1");
		column1.Type.Returns(typeof(int));
		column1.Nullable.Returns(true);

		var column2 = Substitute.For<ITableColumn>();
		column2.Name.Returns("column2");
		column2.Type.Returns(typeof(decimal));
		column2.Nullable.Returns(true);
		column2.Scale.Returns(5);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column1),
			(TableColumnChangeType.Alter, column2)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		Assert.Throws<IncompleteAlterTableStatementException>(() => AlterTableGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void MissingSecondColumnDecimalScaleThrows()
	{
		var column1 = Substitute.For<ITableColumn>();
		column1.Name.Returns("column1");
		column1.Type.Returns(typeof(int));
		column1.Nullable.Returns(true);

		var column2 = Substitute.For<ITableColumn>();
		column2.Name.Returns("column2");
		column2.Type.Returns(typeof(decimal));
		column2.Nullable.Returns(true);
		column2.Precision.Returns(5);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column1),
			(TableColumnChangeType.Alter, column2)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		Assert.Throws<IncompleteAlterTableStatementException>(() => AlterTableGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void ZeroDecimalColumnPrecisionThrows()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(decimal));
		column.Nullable.Returns(true);
		column.Precision.Returns(0);
		column.Scale.Returns(0);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		Assert.Throws<InvalidDecimalPrecisionException>(() => AlterTableGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void TooBigDecimalColumnPrecisionThrows()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(decimal));
		column.Nullable.Returns(true);
		column.Precision.Returns(40);
		column.Scale.Returns(0);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		Assert.Throws<InvalidDecimalPrecisionException>(() => AlterTableGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void NegativeDecimalColumnScaleThrows()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(decimal));
		column.Nullable.Returns(true);
		column.Precision.Returns(10);
		column.Scale.Returns(-1);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		Assert.Throws<InvalidDecimalScaleException>(() => AlterTableGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void BiggerDecimalColumnScaleThanPrecisionThrows()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(decimal));
		column.Nullable.Returns(true);
		column.Precision.Returns(5);
		column.Scale.Returns(6);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		Assert.Throws<InvalidDecimalScaleException>(() => AlterTableGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void TooBigStringColumnMaximumLengthThrows()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(string));
		column.Nullable.Returns(true);
		column.FixedLength.Returns(5000);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		Assert.Throws<InvalidStringLengthException>(() => AlterTableGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void TooBigStringColumnFixedLengthThrows()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(string));
		column.Nullable.Returns(true);
		column.FixedLength.Returns(5000);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		Assert.Throws<InvalidStringLengthException>(() => AlterTableGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void BoolColumnGeneratedCorrectly()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(bool));
		column.Nullable.Returns(false);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		var sql = AlterTableGenerator.Generate(statement, "schema");

		const string expected = """
			ALTER TABLE [schema].[table]
			ALTER COLUMN [column] BIT NOT NULL;
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void ShortColumnGeneratedCorrectly()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(short));
		column.Nullable.Returns(false);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		var sql = AlterTableGenerator.Generate(statement, "schema");

		const string expected = """
			ALTER TABLE [schema].[table]
			ALTER COLUMN [column] SMALLINT NOT NULL;
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void IntColumnGeneratedCorrectly()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(int));
		column.Nullable.Returns(false);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		var sql = AlterTableGenerator.Generate(statement, "schema");

		const string expected = """
			ALTER TABLE [schema].[table]
			ALTER COLUMN [column] INT NOT NULL;
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void LongColumnGeneratedCorrectly()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(long));
		column.Nullable.Returns(true);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		var sql = AlterTableGenerator.Generate(statement, "schema");

		const string expected = """
			ALTER TABLE [schema].[table]
			ALTER COLUMN [column] BIGINT NULL;
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void DoubleColumnGeneratedCorrectly()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(double));
		column.Nullable.Returns(false);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		var sql = AlterTableGenerator.Generate(statement, "schema");

		const string expected = """
			ALTER TABLE [schema].[table]
			ALTER COLUMN [column] FLOAT(53) NOT NULL;
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void DecimalColumnGeneratedCorrectly()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(decimal));
		column.Nullable.Returns(true);
		column.Precision.Returns(8);
		column.Scale.Returns(3);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		var sql = AlterTableGenerator.Generate(statement, "schema");

		const string expected = """
			ALTER TABLE [schema].[table]
			ALTER COLUMN [column] DECIMAL(8, 3) NULL;
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void StringColumnGeneratedCorrectly()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(string));
		column.Nullable.Returns(false);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		var sql = AlterTableGenerator.Generate(statement, "schema");

		const string expected = """
			ALTER TABLE [schema].[table]
			ALTER COLUMN [column] NVARCHAR(MAX) NOT NULL;
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void MaximumLengthStringColumnGeneratedCorrectly()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(string));
		column.Nullable.Returns(false);
		column.MaximumLength.Returns(1000);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		var sql = AlterTableGenerator.Generate(statement, "schema");

		const string expected = """
			ALTER TABLE [schema].[table]
			ALTER COLUMN [column] NVARCHAR(1000) NOT NULL;
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void FixedLengthStringColumnGeneratedCorrectly()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(string));
		column.Nullable.Returns(true);
		column.FixedLength.Returns(500);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		var sql = AlterTableGenerator.Generate(statement, "schema");

		const string expected = """
			ALTER TABLE [schema].[table]
			ALTER COLUMN [column] NCHAR(500) NULL;
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void GuidColumnGeneratedCorrectly()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(Guid));
		column.Nullable.Returns(false);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		var sql = AlterTableGenerator.Generate(statement, "schema");

		const string expected = """
			ALTER TABLE [schema].[table]
			ALTER COLUMN [column] UNIQUEIDENTIFIER NOT NULL;
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void DateColumnGeneratedCorrectly()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(DateOnly));
		column.Nullable.Returns(true);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		var sql = AlterTableGenerator.Generate(statement, "schema");

		const string expected = """
			ALTER TABLE [schema].[table]
			ALTER COLUMN [column] DATE NULL;
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void TimeColumnGeneratedCorrectly()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(TimeOnly));
		column.Nullable.Returns(false);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		var sql = AlterTableGenerator.Generate(statement, "schema");

		const string expected = """
			ALTER TABLE [schema].[table]
			ALTER COLUMN [column] TIME NOT NULL;
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void DateTimeColumnGeneratedCorrectly()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(DateTime));
		column.Nullable.Returns(true);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		var sql = AlterTableGenerator.Generate(statement, "schema");

		const string expected = """
			ALTER TABLE [schema].[table]
			ALTER COLUMN [column] DATETIME2 NULL;
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void DateTimeOffsetColumnGeneratedCorrectly()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(DateTimeOffset));
		column.Nullable.Returns(false);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		var sql = AlterTableGenerator.Generate(statement, "schema");

		const string expected = """
			ALTER TABLE [schema].[table]
			ALTER COLUMN [column] DATETIMEOFFSET NOT NULL;
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void MultipleColumnsGeneratedCorrectly()
	{
		var column1 = Substitute.For<ITableColumn>();
		column1.Name.Returns("LastUpdated");
		column1.Type.Returns(typeof(DateTime));
		column1.Nullable.Returns(false);

		var column2 = Substitute.For<ITableColumn>();
		column2.Name.Returns("Code");
		column2.Type.Returns(typeof(string));
		column2.Nullable.Returns(false);
		column2.FixedLength.Returns(10);

		var column3 = Substitute.For<ITableColumn>();
		column3.Name.Returns("Description");
		column3.Type.Returns(typeof(string));
		column3.Nullable.Returns(true);
		column3.MaximumLength.Returns(1000);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Alter, column1),
			(TableColumnChangeType.Alter, column2),
			(TableColumnChangeType.Alter, column3)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		var sql = AlterTableGenerator.Generate(statement, "schema");

		const string expected = """
			ALTER TABLE [schema].[table]
			ALTER COLUMN [LastUpdated] DATETIME2 NOT NULL;

			ALTER TABLE [schema].[table]
			ALTER COLUMN [Code] NCHAR(10) NOT NULL;

			ALTER TABLE [schema].[table]
			ALTER COLUMN [Description] NVARCHAR(1000) NULL;
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
	}
}
