using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Puro.SqlServer.Generator.Exceptions;
using Puro.SqlServer.Generator.Generators.Alter;
using Puro.SqlServer.Generator.Tests.Extensions;
using Puro.Statements.Alter.Table;
using Xunit;

namespace Puro.SqlServer.Generator.Tests.Generators.Alter;

public class AlterTableGeneratorDropColumnTests
{
	[Fact]
	public void NullSchemaThrows()
	{
		var columns = new List<string> { "column1", "column2" };
		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.ReturnsNull();
		statement.DropColumns.Returns(columns.AsReadOnly());

		Assert.Throws<IncompleteAlterTableStatementException>(() => AlterTableGenerator.Generate(statement));
	}

	[Fact]
	public void SingleDropColumnGeneratedCorrectly()
	{
		var columns = new List<string> { "column1" };
		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.DropColumns.Returns(columns.AsReadOnly());

		var sql = AlterTableGenerator.Generate(statement);

		var expected = """
			ALTER TABLE [schema].[table]
				DROP COLUMN [column1];
			""";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void MultipleDropColumnsGeneratedCorrectly()
	{
		var columns = new List<string> { "column1", "column2", "column3" };
		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.DropColumns.Returns(columns.AsReadOnly());

		var sql = AlterTableGenerator.Generate(statement);

		var expected = """
			ALTER TABLE [schema].[table]
				DROP COLUMN [column1], [column2], [column3];
			""";

		expected.SqlEqual(sql);
	}
}
