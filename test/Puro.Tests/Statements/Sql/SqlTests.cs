using Puro.Statements.Sql;
using Xunit;

namespace Puro.Tests.Statements.Sql;

public class SqlTests
{
	[Fact]
	public void NullSqlThrows()
	{
		var migration = new NullMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullMigration : UpMigration
	{
		public override void Up()
		{
			Sql(null!);
		}
	}

	[Fact]
	public void EmptySqlThrows()
	{
		var migration = new EmptyMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class EmptyMigration : UpMigration
	{
		public override void Up()
		{
			Sql(string.Empty);
		}
	}

	[Fact]
	public void WhiteSpaceSqlThrows()
	{
		var migration = new WhiteSpaceMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpaceMigration : UpMigration
	{
		public override void Up()
		{
			Sql("     ");
		}
	}

	[Fact]
	public void DefinedSqlReturned()
	{
		var migration = new SqlMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ISqlMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("SELECT [CustomerId] FROM [Banking].[Account];", statement.Sql);
	}

	private sealed class SqlMigration : UpMigration
	{
		public override void Up()
		{
			Sql("SELECT [CustomerId] FROM [Banking].[Account];");
		}
	}
}
