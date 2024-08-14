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
		statement.Schema.Returns("schema");
		statement.CurrentName.Returns("table");
		statement.NewName.Returns("newname");

		Assert.Throws<ArgumentNullException>(() => RenameTableGenerator.Generate(statement, null!));
	}

	[Fact]
	public void EmptySchemaThrows()
	{
		var statement = Substitute.For<IRenameTableMigrationStatement>();
		statement.Schema.Returns("schema");
		statement.CurrentName.Returns("table");
		statement.NewName.Returns("newname");

		Assert.Throws<ArgumentNullException>(() => RenameTableGenerator.Generate(statement, string.Empty));
	}

	[Fact]
	public void WhiteSpaceSchemaThrows()
	{
		var statement = Substitute.For<IRenameTableMigrationStatement>();
		statement.Schema.Returns("schema");
		statement.CurrentName.Returns("table");
		statement.NewName.Returns("newname");

		Assert.Throws<ArgumentNullException>(() => RenameTableGenerator.Generate(statement, "     "));
	}

	[Fact]
	public void NullNewTableNameThrows()
	{
		var statement = Substitute.For<IRenameTableMigrationStatement>();
		statement.Schema.Returns("schema");
		statement.CurrentName.Returns("table");
		statement.NewName.ReturnsNull();

		Assert.Throws<IncompleteRenameTableStatementException>(() => RenameTableGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void SqlGeneratedCorrectly()
	{
		var statement = Substitute.For<IRenameTableMigrationStatement>();
		statement.Schema.Returns("Transport");
		statement.CurrentName.Returns("Car");
		statement.NewName.Returns("Vehicle");

		var sql = RenameTableGenerator.Generate(statement, "Transport");

		Assert.Equal("EXEC sp_rename N'[Transport].[Car]', N'Vehicle';", sql);
	}

	[Fact]
	public void StatementSchemaSupersedesMigrationSchema()
	{
		var statement = Substitute.For<IRenameTableMigrationStatement>();
		statement.Schema.Returns("Correct");
		statement.CurrentName.Returns("Car");
		statement.NewName.Returns("Vehicle");

		var sql = RenameTableGenerator.Generate(statement, "Wrong");

		Assert.Equal("EXEC sp_rename N'[Correct].[Car]', N'Vehicle';", sql);
	}

	[Fact]
	public void MigrationSchemaUsedWhenStatementSchemaNull()
	{
		var statement = Substitute.For<IRenameTableMigrationStatement>();
		statement.Schema.ReturnsNull();
		statement.CurrentName.Returns("Car");
		statement.NewName.Returns("Vehicle");

		var sql = RenameTableGenerator.Generate(statement, "Transport");

		Assert.Equal("EXEC sp_rename N'[Transport].[Car]', N'Vehicle';", sql);
	}
}
