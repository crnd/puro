using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Puro.SqlServer.Runner.Exceptions;
using Puro.SqlServer.Runner.Generators.Alter;
using Puro.SqlServer.Runner.Generators.Rename;
using Puro.SqlServer.Runner.Tests.Extensions;
using Puro.Statements;
using Puro.Statements.Alter.Table;
using Puro.Statements.Rename.Table;
using Xunit;

namespace Puro.SqlServer.Runner.Tests.Generators.Alter;

public class AlterTableGeneratorTests
{
	[Fact]
	public void NullSchemaThrows()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(int));
		column.Nullable.Returns(true);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Add, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		Assert.Throws<ArgumentNullException>(() => AlterTableGenerator.Generate(statement, null!));
	}

	[Fact]
	public void EmptySchemaThrows()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(int));
		column.Nullable.Returns(true);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Add, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		Assert.Throws<ArgumentNullException>(() => AlterTableGenerator.Generate(statement, string.Empty));
	}

	[Fact]
	public void WhiteSpaceSchemaThrows()
	{
		var column = Substitute.For<ITableColumn>();
		column.Name.Returns("column");
		column.Type.Returns(typeof(int));
		column.Nullable.Returns(true);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Add, column)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		Assert.Throws<ArgumentNullException>(() => AlterTableGenerator.Generate(statement, "     "));
	}

	[Fact]
	public void NoColumnChangesThrows()
	{
		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");

		Assert.Throws<IncompleteAlterTableStatementException>(() => AlterTableGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void MixedColumnChangesGeneratedCorrectly()
	{
		var column1 = Substitute.For<ITableColumn>();
		column1.Name.Returns("column1");
		column1.Type.Returns(typeof(int));
		column1.Nullable.Returns(true);

		var column2 = Substitute.For<ITableColumn>();
		column2.Name.Returns("column2");
		column2.Type.Returns(typeof(DateTimeOffset));
		column2.Nullable.Returns(true);

		var column3 = Substitute.For<ITableColumn>();
		column3.Name.Returns("column3");

		var column4 = Substitute.For<ITableColumn>();
		column4.Name.Returns("column4");

		var column5 = Substitute.For<ITableColumn>();
		column5.Name.Returns("column5");
		column5.Type.Returns(typeof(decimal));
		column5.Nullable.Returns(true);
		column5.Precision.Returns(5);
		column5.Scale.Returns(2);

		var column6 = Substitute.For<ITableColumn>();
		column6.Name.Returns("column6");
		column6.Type.Returns(typeof(int));
		column6.Nullable.Returns(false);

		var column7 = Substitute.For<ITableColumn>();
		column7.Name.Returns("column7");
		column7.Type.Returns(typeof(TimeOnly));
		column7.Nullable.Returns(true);

		var column8 = Substitute.For<ITableColumn>();
		column8.Name.Returns("column8");
		column8.Type.Returns(typeof(string));
		column8.Nullable.Returns(false);
		column8.FixedLength.Returns(250);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Add, column1),
			(TableColumnChangeType.Alter, column2),
			(TableColumnChangeType.Drop, column3),
			(TableColumnChangeType.Drop, column4),
			(TableColumnChangeType.Add, column5),
			(TableColumnChangeType.Add, column6),
			(TableColumnChangeType.Alter, column7),
			(TableColumnChangeType.Alter, column8)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		var sql = AlterTableGenerator.Generate(statement, "schema");

		const string expected = """
			ALTER TABLE [schema].[table]
				ADD [column1] INT NULL;

			ALTER TABLE [schema].[table]
				ALTER COLUMN [column2] DATETIMEOFFSET NULL;

			ALTER TABLE [schema].[table]
				DROP COLUMN [column3], [column4];

			ALTER TABLE [schema].[table]
				ADD
					[column5] DECIMAL(5, 2) NULL,
					[column6] INT NOT NULL;

			ALTER TABLE [schema].[table]
				ALTER COLUMN [column7] TIME NULL;

			ALTER TABLE [schema].[table]
				ALTER COLUMN [column8] NCHAR(250) NOT NULL;
			""";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void StatementSchemaSupersedesMigrationSchema()
	{
		var column1 = Substitute.For<ITableColumn>();
		column1.Name.Returns("column1");
		column1.Type.Returns(typeof(int));
		column1.Nullable.Returns(true);

		var column2 = Substitute.For<ITableColumn>();
		column2.Name.Returns("column2");
		column2.Type.Returns(typeof(DateTimeOffset));
		column2.Nullable.Returns(true);

		var column3 = Substitute.For<ITableColumn>();
		column3.Name.Returns("column3");

		var column4 = Substitute.For<ITableColumn>();
		column4.Name.Returns("column4");

		var column5 = Substitute.For<ITableColumn>();
		column5.Name.Returns("column5");
		column5.Type.Returns(typeof(decimal));
		column5.Nullable.Returns(true);
		column5.Precision.Returns(5);
		column5.Scale.Returns(2);

		var column6 = Substitute.For<ITableColumn>();
		column6.Name.Returns("column6");
		column6.Type.Returns(typeof(int));
		column6.Nullable.Returns(false);

		var column7 = Substitute.For<ITableColumn>();
		column7.Name.Returns("column7");
		column7.Type.Returns(typeof(TimeOnly));
		column7.Nullable.Returns(true);

		var column8 = Substitute.For<ITableColumn>();
		column8.Name.Returns("column8");
		column8.Type.Returns(typeof(string));
		column8.Nullable.Returns(false);
		column8.FixedLength.Returns(250);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Add, column1),
			(TableColumnChangeType.Alter, column2),
			(TableColumnChangeType.Drop, column3),
			(TableColumnChangeType.Drop, column4),
			(TableColumnChangeType.Add, column5),
			(TableColumnChangeType.Add, column6),
			(TableColumnChangeType.Alter, column7),
			(TableColumnChangeType.Alter, column8)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("correct");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		var sql = AlterTableGenerator.Generate(statement, "wrong");

		const string expected = """
			ALTER TABLE [correct].[table]
				ADD [column1] INT NULL;

			ALTER TABLE [correct].[table]
				ALTER COLUMN [column2] DATETIMEOFFSET NULL;

			ALTER TABLE [correct].[table]
				DROP COLUMN [column3], [column4];

			ALTER TABLE [correct].[table]
				ADD
					[column5] DECIMAL(5, 2) NULL,
					[column6] INT NOT NULL;

			ALTER TABLE [correct].[table]
				ALTER COLUMN [column7] TIME NULL;

			ALTER TABLE [correct].[table]
				ALTER COLUMN [column8] NCHAR(250) NOT NULL;
			""";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void MigrationSchemaUsedWhenStatementSchemaNull()
	{
		var column1 = Substitute.For<ITableColumn>();
		column1.Name.Returns("column1");
		column1.Type.Returns(typeof(int));
		column1.Nullable.Returns(true);

		var column2 = Substitute.For<ITableColumn>();
		column2.Name.Returns("column2");
		column2.Type.Returns(typeof(DateTimeOffset));
		column2.Nullable.Returns(true);

		var column3 = Substitute.For<ITableColumn>();
		column3.Name.Returns("column3");

		var column4 = Substitute.For<ITableColumn>();
		column4.Name.Returns("column4");

		var column5 = Substitute.For<ITableColumn>();
		column5.Name.Returns("column5");
		column5.Type.Returns(typeof(decimal));
		column5.Nullable.Returns(true);
		column5.Precision.Returns(5);
		column5.Scale.Returns(2);

		var column6 = Substitute.For<ITableColumn>();
		column6.Name.Returns("column6");
		column6.Type.Returns(typeof(int));
		column6.Nullable.Returns(false);

		var column7 = Substitute.For<ITableColumn>();
		column7.Name.Returns("column7");
		column7.Type.Returns(typeof(TimeOnly));
		column7.Nullable.Returns(true);

		var column8 = Substitute.For<ITableColumn>();
		column8.Name.Returns("column8");
		column8.Type.Returns(typeof(string));
		column8.Nullable.Returns(false);
		column8.FixedLength.Returns(250);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Add, column1),
			(TableColumnChangeType.Alter, column2),
			(TableColumnChangeType.Drop, column3),
			(TableColumnChangeType.Drop, column4),
			(TableColumnChangeType.Add, column5),
			(TableColumnChangeType.Add, column6),
			(TableColumnChangeType.Alter, column7),
			(TableColumnChangeType.Alter, column8)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.ReturnsNull();
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		var sql = AlterTableGenerator.Generate(statement, "schema");

		const string expected = """
			ALTER TABLE [schema].[table]
				ADD [column1] INT NULL;

			ALTER TABLE [schema].[table]
				ALTER COLUMN [column2] DATETIMEOFFSET NULL;

			ALTER TABLE [schema].[table]
				DROP COLUMN [column3], [column4];

			ALTER TABLE [schema].[table]
				ADD
					[column5] DECIMAL(5, 2) NULL,
					[column6] INT NOT NULL;

			ALTER TABLE [schema].[table]
				ALTER COLUMN [column7] TIME NULL;

			ALTER TABLE [schema].[table]
				ALTER COLUMN [column8] NCHAR(250) NOT NULL;
			""";

		expected.SqlEqual(sql);
	}
}
