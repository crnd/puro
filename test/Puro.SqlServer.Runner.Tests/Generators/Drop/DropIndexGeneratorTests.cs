using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Puro.SqlServer.Runner.Exceptions;
using Puro.SqlServer.Runner.Generators.Drop;
using Puro.Statements.Drop.Index;
using Xunit;

namespace Puro.SqlServer.Runner.Tests.Generators.Drop;

public class DropIndexGeneratorTests
{
	[Fact]
	public void NullSchemaThrows()
	{
		var statement = Substitute.For<IDropIndexMigrationStatement>();
		statement.Schema.Returns("TestSchema");
		statement.Table.Returns("TestTable");
		statement.Index.Returns("TestIndex");

		Assert.Throws<ArgumentNullException>(() => DropIndexGenerator.Generate(statement, null!));
	}

	[Fact]
	public void EmptySchemaThrows()
	{
		var statement = Substitute.For<IDropIndexMigrationStatement>();
		statement.Schema.Returns("TestSchema");
		statement.Table.Returns("TestTable");
		statement.Index.Returns("TestIndex");

		Assert.Throws<ArgumentNullException>(() => DropIndexGenerator.Generate(statement, string.Empty));
	}

	[Fact]
	public void WhiteSpaceSchemaThrows()
	{
		var statement = Substitute.For<IDropIndexMigrationStatement>();
		statement.Schema.Returns("TestSchema");
		statement.Table.Returns("TestTable");
		statement.Index.Returns("TestIndex");

		Assert.Throws<ArgumentNullException>(() => DropIndexGenerator.Generate(statement, "     "));
	}

	[Fact]
	public void NullTableThrows()
	{
		var statement = Substitute.For<IDropIndexMigrationStatement>();
		statement.Schema.Returns("TestSchema");
		statement.Table.ReturnsNull();
		statement.Index.Returns("TestIndex");

		Assert.Throws<IncompleteDropIndexStatementException>(() => DropIndexGenerator.Generate(statement, "TestSchema"));
	}

	[Fact]
	public void SqlGeneratedCorrectly()
	{
		var statement = Substitute.For<IDropIndexMigrationStatement>();
		statement.Schema.Returns("Banking");
		statement.Table.Returns("Account");
		statement.Index.Returns("UIX_Account_AccountNumber");

		var sql = DropIndexGenerator.Generate(statement, "Banking");

		Assert.Equal("DROP INDEX [UIX_Account_AccountNumber] ON [Banking].[Account];", sql);
	}

	[Fact]
	public void StatementSchemaSupersedesMigrationSchema()
	{
		var statement = Substitute.For<IDropIndexMigrationStatement>();
		statement.Schema.Returns("Correct");
		statement.Table.Returns("Account");
		statement.Index.Returns("UIX_Account_AccountNumber");

		var sql = DropIndexGenerator.Generate(statement, "Wrong");

		Assert.Equal("DROP INDEX [UIX_Account_AccountNumber] ON [Correct].[Account];", sql);
	}

	[Fact]
	public void MigrationSchemaUsedWhenStatementSchemaNull()
	{
		var statement = Substitute.For<IDropIndexMigrationStatement>();
		statement.Schema.ReturnsNull();
		statement.Table.Returns("Account");
		statement.Index.Returns("UIX_Account_AccountNumber");

		var sql = DropIndexGenerator.Generate(statement, "Banking");

		Assert.Equal("DROP INDEX [UIX_Account_AccountNumber] ON [Banking].[Account];", sql);
	}
}
