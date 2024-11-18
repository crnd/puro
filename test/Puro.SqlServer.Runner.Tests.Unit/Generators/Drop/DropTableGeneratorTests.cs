using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Puro.SqlServer.Runner.Generators.Drop;
using Puro.Statements.Drop.Table;
using Xunit;

namespace Puro.SqlServer.Runner.Tests.Unit.Generators.Drop;

public class DropTableGeneratorTests
{
	[Fact]
	public void NullSchemaThrows()
	{
		var statement = Substitute.For<IDropTableMigrationStatement>();
		statement.Schema.ReturnsNull();
		statement.Table.Returns("TestTable");

		Assert.Throws<ArgumentNullException>(() => DropTableGenerator.Generate(statement, null!));
	}

	[Fact]
	public void EmptySchemaThrows()
	{
		var statement = Substitute.For<IDropTableMigrationStatement>();
		statement.Schema.ReturnsNull();
		statement.Table.Returns("TestTable");

		Assert.Throws<ArgumentNullException>(() => DropTableGenerator.Generate(statement, string.Empty));
	}

	[Fact]
	public void WhiteSpaceSchemaThrows()
	{
		var statement = Substitute.For<IDropTableMigrationStatement>();
		statement.Schema.ReturnsNull();
		statement.Table.Returns("TestTable");

		Assert.Throws<ArgumentNullException>(() => DropTableGenerator.Generate(statement, "     "));
	}

	[Fact]
	public void SqlGeneratedCorrectly()
	{
		var statement = Substitute.For<IDropTableMigrationStatement>();
		statement.Schema.Returns("Banking");
		statement.Table.Returns("Account");

		var sql = DropTableGenerator.Generate(statement, "Banking");

		Assert.Equal("DROP TABLE [Banking].[Account];", sql);
	}

	[Fact]
	public void StatementSchemaSupersedesMigrationSchema()
	{
		var statement = Substitute.For<IDropTableMigrationStatement>();
		statement.Schema.Returns("Correct");
		statement.Table.Returns("Account");

		var sql = DropTableGenerator.Generate(statement, "Wrong");

		Assert.Equal("DROP TABLE [Correct].[Account];", sql);
	}

	[Fact]
	public void MigrationSchemaUsedWhenStatementSchemaNull()
	{
		var statement = Substitute.For<IDropTableMigrationStatement>();
		statement.Schema.ReturnsNull();
		statement.Table.Returns("Account");

		var sql = DropTableGenerator.Generate(statement, "Banking");

		Assert.Equal("DROP TABLE [Banking].[Account];", sql);
	}
}
