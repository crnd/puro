using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Puro.SqlServer.Generator.Exceptions;
using Puro.SqlServer.Generator.Generators.Drop;
using Puro.Statements.Drop.Constraint;
using Xunit;

namespace Puro.SqlServer.Generator.Tests.Generators.Drop;

public class DropConstraintGeneratorTests
{
	[Fact]
	public void NullSchemaThrows()
	{
		var statement = Substitute.For<IDropConstraintMigrationStatement>();
		statement.Schema.ReturnsNull();
		statement.Table.Returns("TestTable");
		statement.Constraint.Returns("TestConstraint");

		Assert.Throws<IncompleteDropConstraintStatementException>(() => DropConstraintGenerator.Generate(statement));
	}

	[Fact]
	public void NullTableThrows()
	{
		var statement = Substitute.For<IDropConstraintMigrationStatement>();
		statement.Schema.Returns("TestSchema");
		statement.Table.ReturnsNull();
		statement.Constraint.Returns("TestConstraint");

		Assert.Throws<IncompleteDropConstraintStatementException>(() => DropConstraintGenerator.Generate(statement));
	}

	[Fact]
	public void SqlGeneratedCorrectly()
	{
		var statement = Substitute.For<IDropConstraintMigrationStatement>();
		statement.Schema.Returns("Banking");
		statement.Table.Returns("Account");
		statement.Constraint.Returns("PK_Account_Id");

		var sql = DropConstraintGenerator.Generate(statement);

		Assert.Equal("ALTER TABLE [Banking].[Account] DROP CONSTRAINT [PK_Account_Id];", sql);
	}
}
