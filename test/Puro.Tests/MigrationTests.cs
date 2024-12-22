using Puro.Exceptions;
using Puro.Statements.Create.PrimaryKey;
using Puro.Statements.Drop.Constraint;
using Puro.Statements.Drop.Index;
using Puro.Statements.Drop.Table;
using Puro.Statements.Sql;
using Xunit;

namespace Puro.Tests;

public class MigrationTests
{
	[Fact]
	public void GettingMigrationNameWithoutNameAttributeThrows()
	{
		var migration = new NamelessMigration();

		var exception = Assert.Throws<NameAttributeNotFoundException>(() => migration.Name);
		Assert.NotNull(exception);
		Assert.StartsWith("Migration NamelessMigration ", exception.Message);
	}

	[Fact]
	public void EmptyCollectionReturnedFromMigrationWithNoStatements()
	{
		var migration = new NamelessMigration();
		migration.Up();

		Assert.Empty(migration.Statements);
	}

	private sealed class NamelessMigration : Migration
	{
		public override void Up() { }
	}

	[Fact]
	public void MigrationNameReturned()
	{
		var migration = new NameMigration();

		Assert.Equal("TestMigration", migration.Name);
	}

	[MigrationName("TestMigration")]
	private sealed class NameMigration : Migration
	{
		public override void Up() { }
	}

	[Fact]
	public void StatementsReturnedInCreationOrder()
	{
		var migration = new StatementsMigration();
		migration.Up();

		Assert.StrictEqual(5, migration.Statements.Count);
		Assert.IsAssignableFrom<IDropConstraintMigrationStatement>(migration.Statements[0]);
		Assert.IsAssignableFrom<ICreatePrimaryKeyMigrationStatement>(migration.Statements[1]);
		Assert.IsAssignableFrom<IDropIndexMigrationStatement>(migration.Statements[2]);
		Assert.IsAssignableFrom<ISqlMigrationStatement>(migration.Statements[3]);
		Assert.IsAssignableFrom<IDropTableMigrationStatement>(migration.Statements[4]);
	}

	[Fact]
	public void MultipleUpCallsAddStatementsMultipleTimes()
	{
		var migration = new StatementsMigration();
		migration.Up();
		migration.Up();
		migration.Up();

		Assert.StrictEqual(15, migration.Statements.Count);
	}

	private sealed class StatementsMigration : Migration
	{
		public override void Up()
		{
			Drop.Constraint("PK_Account_CustomerId")
				.FromTable("Account").InSchema("Banking");

			Create.PrimaryKey("PK_Account_Id")
				.OnTable("Account")
				.InSchema("Banking")
				.WithColumn("Id");

			Drop.Index("UIX_Account_CustomerId")
				.FromTable("Accout")
				.InSchema("Banking");

			Sql("SELECT [AccountNumber] FROM [Banking].[Account];");

			Drop.Table("Account").InSchema("Banking");
		}
	}

	[Fact]
	public void OverwrittenSchemaReturnedFromMigration()
	{
		var migration = new SchemaOverwriteMigration();

		Assert.Equal("overwritten", migration.Schema);
	}

	private sealed class SchemaOverwriteMigration : SchemaOverwriteMigrationBase
	{
		public override void Up()
		{
			Create.Table("Vehicle")
				.WithColumn("Id").AsInt().Identity()
				.WithColumn("Model").AsString().MaximumLength(200).NotNull();
		}
	}

	private abstract class SchemaOverwriteMigrationBase : Migration
	{
		public new string Schema => "overwritten";
	}
}
