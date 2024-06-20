using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Puro.SqlServer.Runner.Exceptions;
using Puro.SqlServer.Runner.Generators.Drop;
using Puro.Statements.Drop.Table;
using Xunit;

namespace Puro.SqlServer.Runner.Tests.Generators.Drop;

public class DropTableGeneratorTests
{
	[Fact]
	public void NullSchemaThrows()
	{
		var statement = Substitute.For<IDropTableMigrationStatement>();
		statement.Schema.ReturnsNull();
		statement.Table.Returns("TestTable");

		Assert.Throws<IncompleteDropTableStatementException>(() => DropTableGenerator.Generate(statement));
	}

	[Fact]
	public void SqlGeneratedCorrectly()
	{
		var statement = Substitute.For<IDropTableMigrationStatement>();
		statement.Schema.Returns("Banking");
		statement.Table.Returns("Account");

		var sql = DropTableGenerator.Generate(statement);

		Assert.Equal("DROP TABLE [Banking].[Account];", sql);
	}
}
