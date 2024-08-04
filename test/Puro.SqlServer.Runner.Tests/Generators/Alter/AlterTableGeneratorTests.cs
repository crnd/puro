using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Puro.SqlServer.Runner.Exceptions;
using Puro.SqlServer.Runner.Generators.Alter;
using Puro.SqlServer.Runner.Tests.Extensions;
using Puro.Statements;
using Puro.Statements.Alter.Table;
using Xunit;

namespace Puro.SqlServer.Runner.Tests.Generators.Alter;

public class AlterTableGeneratorTests
{
	[Fact]
	public void NullSchemaThrows()
	{
		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.ReturnsNull();

		Assert.Throws<IncompleteAlterTableStatementException>(() => AlterTableGenerator.Generate(statement));
	}

	[Fact]
	public void NoColumnChangesThrows()
	{
		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");

		Assert.Throws<IncompleteAlterTableStatementException>(() => AlterTableGenerator.Generate(statement));
	}

	[Fact]
	public void MixedAddAndDropColumnsGeneratedCorrectly()
	{
		var column1 = Substitute.For<ITableColumn>();
		column1.Name.Returns("column1");
		column1.Type.Returns(typeof(int));
		column1.Nullable.Returns(true);

		var column2 = Substitute.For<ITableColumn>();
		column2.Name.Returns("column2");

		var column3 = Substitute.For<ITableColumn>();
		column3.Name.Returns("column3");

		var column4 = Substitute.For<ITableColumn>();
		column4.Name.Returns("column4");
		column4.Type.Returns(typeof(decimal));
		column4.Nullable.Returns(true);
		column4.Precision.Returns(5);
		column4.Scale.Returns(2);

		var column5 = Substitute.For<ITableColumn>();
		column5.Name.Returns("column5");
		column5.Type.Returns(typeof(int));
		column5.Nullable.Returns(false);

		var changes = new List<(TableColumnChangeType, ITableColumn)>
		{
			(TableColumnChangeType.Add, column1),
			(TableColumnChangeType.Drop, column2),
			(TableColumnChangeType.Drop, column3),
			(TableColumnChangeType.Add, column4),
			(TableColumnChangeType.Add, column5)
		};

		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes.AsReadOnly());

		var sql = AlterTableGenerator.Generate(statement);

		const string expected = """
			ALTER TABLE [schema].[table]
				ADD [column1] INT NULL;

			ALTER TABLE [schema].[table]
				DROP COLUMN [column2], [column3];

			ALTER TABLE [schema].[table]
				ADD
					[column4] DECIMAL(5, 2) NULL,
					[column5] INT NOT NULL;
			""";

		expected.SqlEqual(sql);
	}
}
