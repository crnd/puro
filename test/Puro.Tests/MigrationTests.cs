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

		Assert.Empty(migration.Statements);
	}

	private sealed class NamelessMigration : UpMigration
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
	private sealed class NameMigration : UpMigration
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
	public void UpOnlyMigrationAddsNoStatementsToDown()
	{
		var migration = new StatementsMigration();
		migration.Down();

		Assert.Empty(migration.Statements);
	}

	private sealed class StatementsMigration : UpMigration
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
	public void DownStatementsNotMixedWithUpStatements()
	{
		var migration = new TwoWayMigration();
		migration.Up();

		Assert.StrictEqual(2, migration.Statements.Count);
		Assert.IsAssignableFrom<ISqlMigrationStatement>(migration.Statements[0]);
		Assert.IsAssignableFrom<IDropTableMigrationStatement>(migration.Statements[1]);
	}

	[Fact]
	public void UpStatementsNotMixedWithDownStatements()
	{
		var migration = new TwoWayMigration();
		migration.Down();

		Assert.StrictEqual(3, migration.Statements.Count);
		Assert.IsAssignableFrom<IDropConstraintMigrationStatement>(migration.Statements[0]);
		Assert.IsAssignableFrom<ICreatePrimaryKeyMigrationStatement>(migration.Statements[1]);
		Assert.IsAssignableFrom<IDropIndexMigrationStatement>(migration.Statements[2]);
	}

	[Fact]
	public void MultipleUpCallsAddStatementsMultipleTimes()
	{
		var migration = new TwoWayMigration();
		migration.Up();
		migration.Up();
		migration.Up();

		Assert.StrictEqual(6, migration.Statements.Count);
	}

	[Fact]
	public void MultipleDownCallsAddStatementsMultipleTimes()
	{
		var migration = new TwoWayMigration();
		migration.Down();
		migration.Down();
		migration.Down();

		Assert.StrictEqual(9, migration.Statements.Count);
	}

	[Fact]
	public void UpAndDownAddsAllStatements()
	{
		var migration = new TwoWayMigration();
		migration.Up();
		migration.Down();

		Assert.StrictEqual(5, migration.Statements.Count);
	}

	[Fact]
	public void DownAndUpAddsAllStatements()
	{
		var migration = new TwoWayMigration();
		migration.Down();
		migration.Up();

		Assert.StrictEqual(5, migration.Statements.Count);
	}

	private sealed class TwoWayMigration : Migration
	{
		public override void Up()
		{
			Sql("SELECT * FROM [dbo].[Test];");

			Drop.Table("Test").InSchema("dbo");
		}

		public override void Down()
		{
			Drop.Constraint("PK_Test_TestId")
				.FromTable("Test")
				.InSchema("dbo");

			Create.PrimaryKey("PK_Test_Id")
				.OnTable("Test")
				.InSchema("dbo")
				.WithColumn("Id");

			Drop.Index("UIX_Test_Id")
				.FromTable("Test")
				.InSchema("dbo");
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
		public override void Down()
		{
			Drop.Table("Vehicle");
		}

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

	[Fact]
	public void OverwrittenSchemaReturnedFromUpMigration()
	{
		var migration = new SchemaOverwriteUpMigration();

		Assert.Equal("overwritten", migration.Schema);
	}

	private sealed class SchemaOverwriteUpMigration : SchemaOverwriteUpMigrationBase
	{
		public override void Up()
		{
			Create.Table("Vehicle")
				.WithColumn("Id").AsInt().Identity()
				.WithColumn("Model").AsString().MaximumLength(200).NotNull();
		}
	}

	private abstract class SchemaOverwriteUpMigrationBase : UpMigration
	{
		public new string Schema => "overwritten";
	}
}
