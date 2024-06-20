using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Puro.SqlServer.Runner.Exceptions;
using Puro.SqlServer.Runner.Generators.Create;
using Puro.SqlServer.Runner.Tests.Extensions;
using Puro.Statements.Create.PrimaryKey;
using Xunit;

namespace Puro.SqlServer.Runner.Tests.Generators.Create;

public class CreatePrimaryKeyGeneratorTests
{
	[Fact]
	public void NullSchemaThrows()
	{
		var statement = Substitute.For<ICreatePrimaryKeyMigrationStatement>();
		statement.Schema.ReturnsNull();
		statement.Table.Returns("TestTable");
		statement.PrimaryKey.Returns("PK_Test");
		statement.Columns.Returns(["Id"]);

		Assert.Throws<IncompleteCreatePrimaryKeyStatementException>(() => CreatePrimaryKeyGenerator.Generate(statement));
	}

	[Fact]
	public void NullTableThrows()
	{
		var statement = Substitute.For<ICreatePrimaryKeyMigrationStatement>();
		statement.Schema.Returns("TestSchema");
		statement.Table.ReturnsNull();
		statement.PrimaryKey.Returns("PK_Test");
		statement.Columns.Returns(["Id"]);

		Assert.Throws<IncompleteCreatePrimaryKeyStatementException>(() => CreatePrimaryKeyGenerator.Generate(statement));
	}

	[Fact]
	public void EmptyColumnsThrows()
	{
		var statement = Substitute.For<ICreatePrimaryKeyMigrationStatement>();
		statement.Schema.Returns("TestSchema");
		statement.Table.Returns("TestTable");
		statement.PrimaryKey.Returns("PK_Test");
		statement.Columns.Returns([]);

		Assert.Throws<IncompleteCreatePrimaryKeyStatementException>(() => CreatePrimaryKeyGenerator.Generate(statement));
	}

	[Fact]
	public void SingleColumnSqlGeneratedCorrectly()
	{
		var statement = Substitute.For<ICreatePrimaryKeyMigrationStatement>();
		statement.Schema.Returns("Banking");
		statement.Table.Returns("Account");
		statement.PrimaryKey.Returns("PK_Account_Id");
		statement.Columns.Returns(["Id"]);

		var sql = CreatePrimaryKeyGenerator.Generate(statement);

		var expected = """
			ALTER TABLE [Banking].[Account]
				ADD CONSTRAINT [PK_Account_Id] PRIMARY KEY CLUSTERED ([Id]);
			""";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void MultipleColumnSqlGeneratedCorrectly()
	{
		var statement = Substitute.For<ICreatePrimaryKeyMigrationStatement>();
		statement.Schema.Returns("Banking");
		statement.Table.Returns("Account");
		statement.PrimaryKey.Returns("PK_Account_AccountId_CustomerId_BankId");
		statement.Columns.Returns(["AccountId", "CustomerId", "BankId"]);

		var sql = CreatePrimaryKeyGenerator.Generate(statement);

		var expected = """
			ALTER TABLE [Banking].[Account]
				ADD CONSTRAINT [PK_Account_AccountId_CustomerId_BankId]
					PRIMARY KEY CLUSTERED ([AccountId], [CustomerId], [BankId]);
			""";

		expected.SqlEqual(sql);
	}
}
