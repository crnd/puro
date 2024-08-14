using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Puro.SqlServer.Runner.Exceptions;
using Puro.SqlServer.Runner.Generators.Rename;
using Puro.Statements.Rename.Index;
using Xunit;

namespace Puro.SqlServer.Runner.Tests.Generators.Rename;

public class RenameIndexGeneratorTests
{
	[Fact]
	public void NullSchemaThrows()
	{
		var statement = Substitute.For<IRenameIndexMigrationStatement>();
		statement.Schema.Returns("Schema");
		statement.Table.Returns("Table");
		statement.CurrentName.Returns("Index");
		statement.NewName.Returns("Index");

		Assert.Throws<ArgumentNullException>(() => RenameIndexGenerator.Generate(statement, null!));
	}

	[Fact]
	public void EmptySchemaThrows()
	{
		var statement = Substitute.For<IRenameIndexMigrationStatement>();
		statement.Schema.Returns("Schema");
		statement.Table.Returns("Table");
		statement.CurrentName.Returns("Index");
		statement.NewName.Returns("Index");

		Assert.Throws<ArgumentNullException>(() => RenameIndexGenerator.Generate(statement, string.Empty));
	}

	[Fact]
	public void WhiteSpaceSchemaThrows()
	{
		var statement = Substitute.For<IRenameIndexMigrationStatement>();
		statement.Schema.Returns("Schema");
		statement.Table.Returns("Table");
		statement.CurrentName.Returns("Index");
		statement.NewName.Returns("Index");

		Assert.Throws<ArgumentNullException>(() => RenameIndexGenerator.Generate(statement, "     "));
	}

	[Fact]
	public void NullTableNameThrows()
	{
		var statement = Substitute.For<IRenameIndexMigrationStatement>();
		statement.Schema.Returns("Schema");
		statement.Table.ReturnsNull();
		statement.CurrentName.Returns("Index");
		statement.NewName.Returns("Index");

		Assert.Throws<IncompleteRenameIndexStatementException>(() => RenameIndexGenerator.Generate(statement, "Schema"));
	}

	[Fact]
	public void NullNewIndexNameThrows()
	{
		var statement = Substitute.For<IRenameIndexMigrationStatement>();
		statement.Schema.Returns("Schema");
		statement.Table.Returns("Table");
		statement.CurrentName.Returns("Index");
		statement.NewName.ReturnsNull();

		Assert.Throws<IncompleteRenameIndexStatementException>(() => RenameIndexGenerator.Generate(statement, "Schema"));
	}

	[Fact]
	public void SqlGeneratedCorrectly()
	{
		var statement = Substitute.For<IRenameIndexMigrationStatement>();
		statement.Schema.Returns("Banking");
		statement.Table.Returns("Account");
		statement.CurrentName.Returns("IX_AccountNumber");
		statement.NewName.Returns("UIX_AccountNumber");

		var sql = RenameIndexGenerator.Generate(statement, "Banking");

		Assert.Equal("EXEC sp_rename N'[Banking].[Account].[IX_AccountNumber]', N'UIX_AccountNumber', N'INDEX';", sql);
	}

	[Fact]
	public void StatementSchemaSupersedesMigrationSchema()
	{
		var statement = Substitute.For<IRenameIndexMigrationStatement>();
		statement.Schema.Returns("Correct");
		statement.Table.Returns("Account");
		statement.CurrentName.Returns("IX_AccountNumber");
		statement.NewName.Returns("UIX_AccountNumber");

		var sql = RenameIndexGenerator.Generate(statement, "Wrong");

		Assert.Equal("EXEC sp_rename N'[Correct].[Account].[IX_AccountNumber]', N'UIX_AccountNumber', N'INDEX';", sql);
	}

	[Fact]
	public void MigrationSchemaUsedWhenStatementSchemaNull()
	{
		var statement = Substitute.For<IRenameIndexMigrationStatement>();
		statement.Schema.ReturnsNull();
		statement.Table.Returns("Account");
		statement.CurrentName.Returns("IX_AccountNumber");
		statement.NewName.Returns("UIX_AccountNumber");

		var sql = RenameIndexGenerator.Generate(statement, "Banking");

		Assert.Equal("EXEC sp_rename N'[Banking].[Account].[IX_AccountNumber]', N'UIX_AccountNumber', N'INDEX';", sql);
	}
}
