using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Puro.SqlServer.Generator.Exceptions;
using Puro.SqlServer.Generator.Statements.Drop;
using Puro.Statements.Drop.Index;
using Xunit;

namespace Puro.SqlServer.Generator.Tests.Statements.Drop;

public class DropIndexGeneratorTests
{
	[Fact]
	public void NullSchemaThrows()
	{
		var statement = Substitute.For<IDropIndexMigrationStatement>();
		statement.Schema.ReturnsNull();
		statement.Table.Returns("TestTable");
		statement.Index.Returns("TestIndex");

		Assert.Throws<IncompleteDropIndexStatementException>(() => DropIndexGenerator.Generate(statement));
	}

	[Fact]
	public void NullTableThrows()
	{
		var statement = Substitute.For<IDropIndexMigrationStatement>();
		statement.Schema.Returns("TestSchema");
		statement.Table.ReturnsNull();
		statement.Index.Returns("TestIndex");

		Assert.Throws<IncompleteDropIndexStatementException>(() => DropIndexGenerator.Generate(statement));
	}

	[Fact]
	public void SqlGeneratedCorrectly()
	{
		var statement = Substitute.For<IDropIndexMigrationStatement>();
		statement.Schema.Returns("Banking");
		statement.Table.Returns("Account");
		statement.Index.Returns("UIX_Account_AccountNumber");

		var sql = DropIndexGenerator.Generate(statement);

		Assert.Equal("DROP INDEX [UIX_Account_AccountNumber] ON [Banking].[Account];", sql);
	}
}
