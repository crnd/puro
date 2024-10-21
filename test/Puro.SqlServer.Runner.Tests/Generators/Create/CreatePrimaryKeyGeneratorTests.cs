using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Puro.SqlServer.Runner.Exceptions;
using Puro.SqlServer.Runner.Generators.Create;
using Puro.Statements.Create.PrimaryKey;
using Xunit;

namespace Puro.SqlServer.Runner.Tests.Generators.Create;

public class CreatePrimaryKeyGeneratorTests
{
	[Fact]
	public void NullSchemaThrows()
	{
		var statement = Substitute.For<ICreatePrimaryKeyMigrationStatement>();
		statement.Schema.Returns("TestSchema");
		statement.Table.Returns("TestTable");
		statement.PrimaryKey.Returns("PK_Test");
		statement.Columns.Returns(["Id"]);

		Assert.Throws<ArgumentNullException>(() => CreatePrimaryKeyGenerator.Generate(statement, null!));
	}

	[Fact]
	public void EmptySchemaThrows()
	{
		var statement = Substitute.For<ICreatePrimaryKeyMigrationStatement>();
		statement.Schema.Returns("TestSchema");
		statement.Table.Returns("TestTable");
		statement.PrimaryKey.Returns("PK_Test");
		statement.Columns.Returns(["Id"]);

		Assert.Throws<ArgumentNullException>(() => CreatePrimaryKeyGenerator.Generate(statement, string.Empty));
	}

	[Fact]
	public void WhiteSpaceSchemaThrows()
	{
		var statement = Substitute.For<ICreatePrimaryKeyMigrationStatement>();
		statement.Schema.Returns("TestSchema");
		statement.Table.Returns("TestTable");
		statement.PrimaryKey.Returns("PK_Test");
		statement.Columns.Returns(["Id"]);

		Assert.Throws<ArgumentNullException>(() => CreatePrimaryKeyGenerator.Generate(statement, "     "));
	}

	[Fact]
	public void NullTableThrows()
	{
		var statement = Substitute.For<ICreatePrimaryKeyMigrationStatement>();
		statement.Schema.Returns("TestSchema");
		statement.Table.ReturnsNull();
		statement.PrimaryKey.Returns("PK_Test");
		statement.Columns.Returns(["Id"]);

		Assert.Throws<IncompleteCreatePrimaryKeyStatementException>(() => CreatePrimaryKeyGenerator.Generate(statement, "TestSchema"));
	}

	[Fact]
	public void EmptyColumnsThrows()
	{
		var statement = Substitute.For<ICreatePrimaryKeyMigrationStatement>();
		statement.Schema.Returns("TestSchema");
		statement.Table.Returns("TestTable");
		statement.PrimaryKey.Returns("PK_Test");
		statement.Columns.Returns([]);

		Assert.Throws<IncompleteCreatePrimaryKeyStatementException>(() => CreatePrimaryKeyGenerator.Generate(statement, "TestSchema"));
	}

	[Fact]
	public void SingleColumnSqlGeneratedCorrectly()
	{
		var statement = Substitute.For<ICreatePrimaryKeyMigrationStatement>();
		statement.Schema.Returns("Banking");
		statement.Table.Returns("Account");
		statement.PrimaryKey.Returns("PK_Account_Id");
		statement.Columns.Returns(["Id"]);

		var sql = CreatePrimaryKeyGenerator.Generate(statement, "Banking");

		const string expected = "ALTER TABLE [Banking].[Account] ADD CONSTRAINT [PK_Account_Id] PRIMARY KEY CLUSTERED ([Id]);";

		Assert.Equal(expected, sql);
	}

	[Fact]
	public void MultipleColumnSqlGeneratedCorrectly()
	{
		var statement = Substitute.For<ICreatePrimaryKeyMigrationStatement>();
		statement.Schema.Returns("Banking");
		statement.Table.Returns("Account");
		statement.PrimaryKey.Returns("PK_Account_AccountId_CustomerId_BankId");
		statement.Columns.Returns(["AccountId", "CustomerId", "BankId"]);

		var sql = CreatePrimaryKeyGenerator.Generate(statement, "Banking");

		const string expected = "ALTER TABLE [Banking].[Account] ADD CONSTRAINT [PK_Account_AccountId_CustomerId_BankId] PRIMARY KEY CLUSTERED ([AccountId], [CustomerId], [BankId]);";

		Assert.Equal(expected, sql);
	}

	[Fact]
	public void StatementSchemaSupersedesMigrationSchema()
	{
		var statement = Substitute.For<ICreatePrimaryKeyMigrationStatement>();
		statement.Schema.Returns("Correct");
		statement.Table.Returns("Account");
		statement.PrimaryKey.Returns("PK_Account_AccountId_CustomerId_BankId");
		statement.Columns.Returns(["AccountId", "CustomerId", "BankId"]);

		var sql = CreatePrimaryKeyGenerator.Generate(statement, "Wrong");

		const string expected = "ALTER TABLE [Correct].[Account] ADD CONSTRAINT [PK_Account_AccountId_CustomerId_BankId] PRIMARY KEY CLUSTERED ([AccountId], [CustomerId], [BankId]);";

		Assert.Equal(expected, sql);
	}

	[Fact]
	public void MigrationSchemaUsedWhenStatementSchemaNull()
	{
		var statement = Substitute.For<ICreatePrimaryKeyMigrationStatement>();
		statement.Schema.ReturnsNull();
		statement.Table.Returns("Account");
		statement.PrimaryKey.Returns("PK_Account_AccountId_CustomerId_BankId");
		statement.Columns.Returns(["AccountId", "CustomerId", "BankId"]);

		var sql = CreatePrimaryKeyGenerator.Generate(statement, "Banking");

		const string expected = "ALTER TABLE [Banking].[Account] ADD CONSTRAINT [PK_Account_AccountId_CustomerId_BankId] PRIMARY KEY CLUSTERED ([AccountId], [CustomerId], [BankId]);";

		Assert.Equal(expected, sql);
	}
}
