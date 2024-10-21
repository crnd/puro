using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Puro.SqlServer.Runner.Exceptions;
using Puro.SqlServer.Runner.Generators.Drop;
using Puro.Statements.Drop.Constraint;
using Xunit;

namespace Puro.SqlServer.Runner.Tests.Generators.Drop;

public class DropConstraintGeneratorTests
{
	[Fact]
	public void NullSchemaThrows()
	{
		var statement = Substitute.For<IDropConstraintMigrationStatement>();
		statement.Schema.Returns("TestSchema");
		statement.Table.Returns("Table");
		statement.Constraint.Returns("TestConstraint");

		Assert.Throws<ArgumentNullException>(() => DropConstraintGenerator.Generate(statement, null!));
	}

	[Fact]
	public void EmptySchemaThrows()
	{
		var statement = Substitute.For<IDropConstraintMigrationStatement>();
		statement.Schema.Returns("TestSchema");
		statement.Table.Returns("Table");
		statement.Constraint.Returns("TestConstraint");

		Assert.Throws<ArgumentNullException>(() => DropConstraintGenerator.Generate(statement, string.Empty));
	}

	[Fact]
	public void WhiteSpaceSchemaThrows()
	{
		var statement = Substitute.For<IDropConstraintMigrationStatement>();
		statement.Schema.Returns("TestSchema");
		statement.Table.Returns("Table");
		statement.Constraint.Returns("TestConstraint");

		Assert.Throws<ArgumentNullException>(() => DropConstraintGenerator.Generate(statement, "     "));
	}

	[Fact]
	public void NullTableThrows()
	{
		var statement = Substitute.For<IDropConstraintMigrationStatement>();
		statement.Schema.Returns("TestSchema");
		statement.Table.ReturnsNull();
		statement.Constraint.Returns("TestConstraint");

		Assert.Throws<IncompleteDropConstraintStatementException>(() => DropConstraintGenerator.Generate(statement, "TestSchema"));
	}

	[Fact]
	public void SqlGeneratedCorrectly()
	{
		var statement = Substitute.For<IDropConstraintMigrationStatement>();
		statement.Schema.Returns("Banking");
		statement.Table.Returns("Account");
		statement.Constraint.Returns("PK_Account_Id");

		var sql = DropConstraintGenerator.Generate(statement, "Banking");

		const string expected = """
			ALTER TABLE [Banking].[Account]
			DROP CONSTRAINT [PK_Account_Id];
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void StatementSchemaSupersedesMigrationSchema()
	{
		var statement = Substitute.For<IDropConstraintMigrationStatement>();
		statement.Schema.Returns("Correct");
		statement.Table.Returns("Account");
		statement.Constraint.Returns("PK_Account_Id");

		var sql = DropConstraintGenerator.Generate(statement, "Wrong");

		const string expected = """
			ALTER TABLE [Correct].[Account]
			DROP CONSTRAINT [PK_Account_Id];
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void MigrationSchemaUsedWhenStatementSchemaNull()
	{
		var statement = Substitute.For<IDropConstraintMigrationStatement>();
		statement.Schema.ReturnsNull();
		statement.Table.Returns("Account");
		statement.Constraint.Returns("PK_Account_Id");

		var sql = DropConstraintGenerator.Generate(statement, "Banking");

		const string expected = """
			ALTER TABLE [Banking].[Account]
			DROP CONSTRAINT [PK_Account_Id];
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
	}
}
