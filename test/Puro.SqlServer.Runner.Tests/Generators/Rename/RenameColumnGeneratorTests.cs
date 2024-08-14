using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Puro.SqlServer.Runner.Exceptions;
using Puro.SqlServer.Runner.Generators.Rename;
using Puro.Statements.Rename.Column;
using Xunit;

namespace Puro.SqlServer.Runner.Tests.Generators.Rename;

public class RenameColumnGeneratorTests
{
	[Fact]
	public void NullSchemaThrows()
	{
		var statement = Substitute.For<IRenameColumnMigrationStatement>();
		statement.CurrentName.Returns("column");
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.NewName.Returns("column");

		Assert.Throws<ArgumentNullException>(() => RenameColumnGenerator.Generate(statement, null!));
	}

	[Fact]
	public void EmptySchemaThrows()
	{
		var statement = Substitute.For<IRenameColumnMigrationStatement>();
		statement.CurrentName.Returns("column");
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.NewName.Returns("column");

		Assert.Throws<ArgumentNullException>(() => RenameColumnGenerator.Generate(statement, string.Empty));
	}

	[Fact]
	public void WhiteSpaceSchemaThrows()
	{
		var statement = Substitute.For<IRenameColumnMigrationStatement>();
		statement.CurrentName.Returns("column");
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.NewName.Returns("column");

		Assert.Throws<ArgumentNullException>(() => RenameColumnGenerator.Generate(statement, "     "));
	}

	[Fact]
	public void NullTableNameThrows()
	{
		var statement = Substitute.For<IRenameColumnMigrationStatement>();
		statement.CurrentName.Returns("column");
		statement.Table.ReturnsNull();
		statement.Schema.Returns("schema");
		statement.NewName.Returns("column");

		Assert.Throws<IncompleteRenameColumnStatementException>(() => RenameColumnGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void NullNewColumnNameThrows()
	{
		var statement = Substitute.For<IRenameColumnMigrationStatement>();
		statement.CurrentName.Returns("column");
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.NewName.ReturnsNull();

		Assert.Throws<IncompleteRenameColumnStatementException>(() => RenameColumnGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void SqlGeneratedCorrectly()
	{
		var statement = Substitute.For<IRenameColumnMigrationStatement>();
		statement.CurrentName.Returns("Current");
		statement.Table.Returns("Table");
		statement.Schema.Returns("Schema");
		statement.NewName.Returns("New");

		var sql = RenameColumnGenerator.Generate(statement, "Schema");

		Assert.Equal("EXEC sp_rename N'[Schema].[Table].[Current]', N'New', N'COLUMN';", sql);
	}

	[Fact]
	public void StatementSchemaSupersedesMigrationSchema()
	{
		var statement = Substitute.For<IRenameColumnMigrationStatement>();
		statement.CurrentName.Returns("Name");
		statement.Table.Returns("Vehicle");
		statement.Schema.Returns("Correct");
		statement.NewName.Returns("Model");

		var sql = RenameColumnGenerator.Generate(statement, "Wrong");

		Assert.Equal("EXEC sp_rename N'[Correct].[Vehicle].[Name]', N'Model', N'COLUMN';", sql);
	}

	[Fact]
	public void MigrationSchemaUsedWhenStatementSchemaNull()
	{
		var statement = Substitute.For<IRenameColumnMigrationStatement>();
		statement.CurrentName.Returns("Name");
		statement.Table.Returns("Vehicle");
		statement.Schema.ReturnsNull();
		statement.NewName.Returns("Model");

		var sql = RenameColumnGenerator.Generate(statement, "Transportation");

		Assert.Equal("EXEC sp_rename N'[Transportation].[Vehicle].[Name]', N'Model', N'COLUMN';", sql);
	}
}
