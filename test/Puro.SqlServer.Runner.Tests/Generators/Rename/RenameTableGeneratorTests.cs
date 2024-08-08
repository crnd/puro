using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Puro.SqlServer.Runner.Exceptions;
using Puro.SqlServer.Runner.Generators.Rename;
using Puro.Statements.Rename.Table;
using Xunit;

namespace Puro.SqlServer.Runner.Tests.Generators.Rename;

public class RenameTableGeneratorTests
{
	[Fact]
	public void NullSchemaThrows()
	{
		var statement = Substitute.For<IRenameTableMigrationStatement>();
		statement.Schema.ReturnsNull();
		statement.CurrentName.Returns("table");
		statement.NewName.Returns("new");

		Assert.Throws<IncompleteRenameTableStatementException>(() => RenameTableGenerator.Generate(statement));
	}

	[Fact]
	public void NullNewTableNameThrows()
	{
		var statement = Substitute.For<IRenameTableMigrationStatement>();
		statement.Schema.Returns("schema");
		statement.CurrentName.Returns("table");
		statement.NewName.ReturnsNull();

		Assert.Throws<IncompleteRenameTableStatementException>(() => RenameTableGenerator.Generate(statement));
	}


	[Fact]
	public void SqlGeneratedCorrectly()
	{
		var statement = Substitute.For<IRenameTableMigrationStatement>();
		statement.Schema.Returns("Transport");
		statement.CurrentName.Returns("Car");
		statement.NewName.Returns("Vehicle");

		var sql = RenameTableGenerator.Generate(statement);

		Assert.Equal("EXEC sp_rename '[Transport].[Car]', 'Vehicle';", sql);
	}
}
