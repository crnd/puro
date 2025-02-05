using Puro.SqlServer.Runner.Exceptions;
using Puro.Statements.Alter.Table;
using Puro.Statements.Create.ForeignKey;
using Puro.Statements.Create.Index;
using Puro.Statements.Create.PrimaryKey;
using Puro.Statements.Create.Table;
using Puro.Statements.Sql;
using Xunit;

namespace Puro.SqlServer.Runner.Tests.Unit;

public class MigrationsProcessorTests
{
	private readonly Type[] migrationTypes = [typeof(FirstMigration), typeof(FourthMigration), typeof(ThirdMigration), typeof(SecondMigration)];

	[Fact]
	public void NoMigrationsInstantiatedWhenNoMigrationTypes()
	{
		var migrations = MigrationsProcessor.InstantiateMigrations([]);

		Assert.Empty(migrations);
	}

	[Fact]
	public void InstantiatedMigrationsReturnedInOrder()
	{
		var migrations = MigrationsProcessor.InstantiateMigrations(migrationTypes);

		Assert.StrictEqual(4, migrations.Count);
		Assert.Equal("1_FirstMigration", migrations[0].Name);
		Assert.Equal("2_SecondMigration", migrations[1].Name);
		Assert.Equal("3_ThirdMigration", migrations[2].Name);
		Assert.Equal("4_FourthMigration", migrations[3].Name);
	}

	[Fact]
	public void MigrationIndexThrowsWhenNoMigrations()
	{
		Assert.Throws<MigrationNotFoundException>(() => MigrationsProcessor.FindMigrationIndex("12345_NotFound", []));
	}

	[Fact]
	public void MigrationIndexThrowsWhenNotFound()
	{
		var migrations = MigrationsProcessor.InstantiateMigrations(migrationTypes);

		Assert.Throws<MigrationNotFoundException>(() => MigrationsProcessor.FindMigrationIndex("12345_NotFound", migrations));
	}

	[Fact]
	public void MigrationIndexReturnsNullWhenNullMigrationName()
	{
		var migrations = MigrationsProcessor.InstantiateMigrations(migrationTypes);

		Assert.Null(MigrationsProcessor.FindMigrationIndex(null, migrations));
	}

	[Fact]
	public void MigrationIndexReturnsNullWhenNullMigrationNameAndNoMigrations()
	{
		Assert.Null(MigrationsProcessor.FindMigrationIndex(null, []));
	}

	[Fact]
	public void MigrationIndexReturnsIndex()
	{
		var migrations = MigrationsProcessor.InstantiateMigrations(migrationTypes);
		var index = MigrationsProcessor.FindMigrationIndex("3_ThirdMigration", migrations);

		Assert.StrictEqual(2, index);
	}

	[Fact]
	public void PrepareNoMigrations()
	{
		var migrations = MigrationsProcessor.Prepare([], null, null);

		Assert.Empty(migrations);
	}

	[Fact]
	public void AllMigrationsReturnedInOrder()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, null, null);

		Assert.StrictEqual(4, migrations.Count);
		Assert.Equal("1_FirstMigration", migrations[0].Name);
		Assert.Equal("2_SecondMigration", migrations[1].Name);
		Assert.Equal("3_ThirdMigration", migrations[2].Name);
		Assert.Equal("4_FourthMigration", migrations[3].Name);
	}

	[Fact]
	public void FromFirstMigrationReturnsCorrectMigrations()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "1_FirstMigration", null);

		Assert.StrictEqual(3, migrations.Count);
		Assert.Equal("2_SecondMigration", migrations[0].Name);
		Assert.Equal("3_ThirdMigration", migrations[1].Name);
		Assert.Equal("4_FourthMigration", migrations[2].Name);
	}

	[Fact]
	public void FromSecondMigrationReturnsCorrectMigrations()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "2_SecondMigration", null);

		Assert.StrictEqual(2, migrations.Count);
		Assert.Equal("3_ThirdMigration", migrations[0].Name);
		Assert.Equal("4_FourthMigration", migrations[1].Name);
	}

	[Fact]
	public void FromThirdMigrationReturnsCorrectMigration()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "3_ThirdMigration", null);

		var migration = Assert.Single(migrations);
		Assert.Equal("4_FourthMigration", migration.Name);
	}

	[Fact]
	public void FromFourthMigrationReturnsNoMigrations()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "4_FourthMigration", null);

		Assert.Empty(migrations);
	}

	[Fact]
	public void ToFirstMigrationReturnsCorrectMigration()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, null, "1_FirstMigration");

		var migration = Assert.Single(migrations);
		Assert.Equal("1_FirstMigration", migration.Name);
	}

	[Fact]
	public void ToSecondMigrationReturnsCorrectMigrations()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, null, "2_SecondMigration");

		Assert.StrictEqual(2, migrations.Count);
		Assert.Equal("1_FirstMigration", migrations[0].Name);
		Assert.Equal("2_SecondMigration", migrations[1].Name);
	}

	[Fact]
	public void ToThirdMigrationReturnsCorrectMigrations()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, null, "3_ThirdMigration");

		Assert.StrictEqual(3, migrations.Count);
		Assert.Equal("1_FirstMigration", migrations[0].Name);
		Assert.Equal("2_SecondMigration", migrations[1].Name);
		Assert.Equal("3_ThirdMigration", migrations[2].Name);
	}

	[Fact]
	public void ToFourthMigrationReturnsCorrectMigrations()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, null, "4_FourthMigration");

		Assert.StrictEqual(4, migrations.Count);
		Assert.Equal("1_FirstMigration", migrations[0].Name);
		Assert.Equal("2_SecondMigration", migrations[1].Name);
		Assert.Equal("3_ThirdMigration", migrations[2].Name);
		Assert.Equal("4_FourthMigration", migrations[3].Name);
	}

	[Fact]
	public void FromFirstToFirstMigrationReturnsNoMigrations()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "1_FirstMigration", "1_FirstMigration");

		Assert.Empty(migrations);
	}

	[Fact]
	public void FromFirstToSecondMigrationReturnsCorrectMigration()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "1_FirstMigration", "2_SecondMigration");

		var migration = Assert.Single(migrations);
		Assert.Equal("2_SecondMigration", migration.Name);
	}

	[Fact]
	public void FromFirstToThirdMigrationReturnsCorrectMigrations()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "1_FirstMigration", "3_ThirdMigration");

		Assert.StrictEqual(2, migrations.Count);
		Assert.Equal("2_SecondMigration", migrations[0].Name);
		Assert.Equal("3_ThirdMigration", migrations[1].Name);
	}

	[Fact]
	public void FromFirstToFourthMigrationReturnsCorrectMigrations()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "1_FirstMigration", "4_FourthMigration");

		Assert.StrictEqual(3, migrations.Count);
		Assert.Equal("2_SecondMigration", migrations[0].Name);
		Assert.Equal("3_ThirdMigration", migrations[1].Name);
		Assert.Equal("4_FourthMigration", migrations[2].Name);
	}

	[Fact]
	public void FromSecondToFirstMigrationReturnsNoMigrations()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "2_SecondMigration", "1_FirstMigration");

		Assert.Empty(migrations);
	}

	[Fact]
	public void FromSecondToSecondMigrationReturnsNoMigrations()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "2_SecondMigration", "2_SecondMigration");

		Assert.Empty(migrations);
	}

	[Fact]
	public void FromSecondToThirdMigrationReturnsCorrectMigration()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "2_SecondMigration", "3_ThirdMigration");

		var migration = Assert.Single(migrations);
		Assert.Equal("3_ThirdMigration", migration.Name);
	}

	[Fact]
	public void FromSecondToFourthMigrationReturnsCorrectMigrations()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "2_SecondMigration", "4_FourthMigration");

		Assert.StrictEqual(2, migrations.Count);
		Assert.Equal("3_ThirdMigration", migrations[0].Name);
		Assert.Equal("4_FourthMigration", migrations[1].Name);
	}

	[Fact]
	public void FromThirdToFirstMigrationReturnsNoMigrations()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "3_ThirdMigration", "1_FirstMigration");

		Assert.Empty(migrations);
	}

	[Fact]
	public void FromThirdToSecondMigrationReturnsNoMigrations()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "3_ThirdMigration", "2_SecondMigration");

		Assert.Empty(migrations);
	}

	[Fact]
	public void FromThirdToThirdMigrationReturnsNoMigrations()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "3_ThirdMigration", "3_ThirdMigration");

		Assert.Empty(migrations);
	}

	[Fact]
	public void FromThirdToFourthMigrationReturnsCorrectMigration()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "3_ThirdMigration", "4_FourthMigration");

		var migration = Assert.Single(migrations);
		Assert.Equal("4_FourthMigration", migration.Name);
	}

	[Fact]
	public void FromFourthToFirstMigrationReturnsNoMigrations()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "4_FourthMigration", "1_FirstMigration");

		Assert.Empty(migrations);
	}

	[Fact]
	public void FromFourthToSecondMigrationReturnsNoMigrations()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "4_FourthMigration", "2_SecondMigration");

		Assert.Empty(migrations);
	}

	[Fact]
	public void FromFourthToThirdMigrationReturnsNoMigrations()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "4_FourthMigration", "3_ThirdMigration");

		Assert.Empty(migrations);
	}

	[Fact]
	public void FromFourthToFourthMigrationReturnsNoMigrations()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "4_FourthMigration", "4_FourthMigration");

		Assert.Empty(migrations);
	}

	[Fact]
	public void FromFirstMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "1_FirstMigration", null);

		Assert.StrictEqual(3, migrations.Count);
		var statement = Assert.Single(migrations[0].Statements);
		Assert.IsType<ISqlMigrationStatement>(statement, false);
		Assert.StrictEqual(6, migrations[1].Statements.Count);
		Assert.IsType<ICreateTableMigrationStatement>(migrations[1].Statements[0], false);
		Assert.IsType<IAlterTableMigrationStatement>(migrations[1].Statements[1], false);
		Assert.IsType<ISqlMigrationStatement>(migrations[1].Statements[2], false);
		Assert.IsType<IAlterTableMigrationStatement>(migrations[1].Statements[3], false);
		Assert.IsType<ICreateForeignKeyMigrationStatement>(migrations[1].Statements[4], false);
		Assert.IsType<ICreateIndexMigrationStatement>(migrations[1].Statements[5], false);
		statement = Assert.Single(migrations[2].Statements);
		Assert.IsType<ICreateIndexMigrationStatement>(statement, false);
	}

	[Fact]
	public void FromSecondMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "2_SecondMigration", null);

		Assert.StrictEqual(2, migrations.Count);
		Assert.StrictEqual(6, migrations[0].Statements.Count);
		Assert.IsType<ICreateTableMigrationStatement>(migrations[0].Statements[0], false);
		Assert.IsType<IAlterTableMigrationStatement>(migrations[0].Statements[1], false);
		Assert.IsType<ISqlMigrationStatement>(migrations[0].Statements[2], false);
		Assert.IsType<IAlterTableMigrationStatement>(migrations[0].Statements[3], false);
		Assert.IsType<ICreateForeignKeyMigrationStatement>(migrations[0].Statements[4], false);
		Assert.IsType<ICreateIndexMigrationStatement>(migrations[0].Statements[5], false);
		var statement = Assert.Single(migrations[1].Statements);
		Assert.IsType<ICreateIndexMigrationStatement>(statement, false);
	}

	[Fact]
	public void FromThirdMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "3_ThirdMigration", null);

		var migration = Assert.Single(migrations);
		var statement = Assert.Single(migration.Statements);
		Assert.IsType<ICreateIndexMigrationStatement>(statement, false);
	}

	[Fact]
	public void ToFirstMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, null, "1_FirstMigration");

		var migration = Assert.Single(migrations);
		Assert.StrictEqual(2, migration.Statements.Count);
		Assert.IsType<ICreateTableMigrationStatement>(migration.Statements[0], false);
		Assert.IsType<ICreatePrimaryKeyMigrationStatement>(migration.Statements[1], false);
	}

	[Fact]
	public void ToSecondMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, null, "2_SecondMigration");

		Assert.StrictEqual(2, migrations.Count);
		Assert.StrictEqual(2, migrations[0].Statements.Count);
		Assert.IsType<ICreateTableMigrationStatement>(migrations[0].Statements[0], false);
		Assert.IsType<ICreatePrimaryKeyMigrationStatement>(migrations[0].Statements[1], false);
		var statement = Assert.Single(migrations[1].Statements);
		Assert.IsType<ISqlMigrationStatement>(statement, false);
	}

	[Fact]
	public void ToThirdMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, null, "3_ThirdMigration");

		Assert.StrictEqual(3, migrations.Count);
		Assert.StrictEqual(2, migrations[0].Statements.Count);
		Assert.IsType<ICreateTableMigrationStatement>(migrations[0].Statements[0], false);
		Assert.IsType<ICreatePrimaryKeyMigrationStatement>(migrations[0].Statements[1], false);
		var statement = Assert.Single(migrations[1].Statements);
		Assert.IsType<ISqlMigrationStatement>(statement, false);
		Assert.StrictEqual(6, migrations[2].Statements.Count);
		Assert.IsType<ICreateTableMigrationStatement>(migrations[2].Statements[0], false);
		Assert.IsType<IAlterTableMigrationStatement>(migrations[2].Statements[1], false);
		Assert.IsType<ISqlMigrationStatement>(migrations[2].Statements[2], false);
		Assert.IsType<IAlterTableMigrationStatement>(migrations[2].Statements[3], false);
		Assert.IsType<ICreateForeignKeyMigrationStatement>(migrations[2].Statements[4], false);
		Assert.IsType<ICreateIndexMigrationStatement>(migrations[2].Statements[5], false);
	}

	[Fact]
	public void ToFourthMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, null, "4_FourthMigration");

		Assert.StrictEqual(4, migrations.Count);
		Assert.StrictEqual(2, migrations[0].Statements.Count);
		Assert.IsType<ICreateTableMigrationStatement>(migrations[0].Statements[0], false);
		Assert.IsType<ICreatePrimaryKeyMigrationStatement>(migrations[0].Statements[1], false);
		var statement = Assert.Single(migrations[1].Statements);
		Assert.IsType<ISqlMigrationStatement>(statement, false);
		Assert.StrictEqual(6, migrations[2].Statements.Count);
		Assert.IsType<ICreateTableMigrationStatement>(migrations[2].Statements[0], false);
		Assert.IsType<IAlterTableMigrationStatement>(migrations[2].Statements[1], false);
		Assert.IsType<ISqlMigrationStatement>(migrations[2].Statements[2], false);
		Assert.IsType<IAlterTableMigrationStatement>(migrations[2].Statements[3], false);
		Assert.IsType<ICreateForeignKeyMigrationStatement>(migrations[2].Statements[4], false);
		Assert.IsType<ICreateIndexMigrationStatement>(migrations[2].Statements[5], false);
		statement = Assert.Single(migrations[3].Statements);
		Assert.IsType<ICreateIndexMigrationStatement>(statement, false);
	}

	[Fact]
	public void FromFirstToSecondMigrationReturnsCorrectMigrationStatement()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "1_FirstMigration", "2_SecondMigration");

		var migration = Assert.Single(migrations);
		var statement = Assert.Single(migration.Statements);
		Assert.IsType<ISqlMigrationStatement>(statement, false);
	}

	[Fact]
	public void FromFirstToThirdMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "1_FirstMigration", "3_ThirdMigration");

		Assert.StrictEqual(2, migrations.Count);
		var statement = Assert.Single(migrations[0].Statements);
		Assert.IsType<ISqlMigrationStatement>(statement, false);
		Assert.StrictEqual(6, migrations[1].Statements.Count);
		Assert.IsType<ICreateTableMigrationStatement>(migrations[1].Statements[0], false);
		Assert.IsType<IAlterTableMigrationStatement>(migrations[1].Statements[1], false);
		Assert.IsType<ISqlMigrationStatement>(migrations[1].Statements[2], false);
		Assert.IsType<IAlterTableMigrationStatement>(migrations[1].Statements[3], false);
		Assert.IsType<ICreateForeignKeyMigrationStatement>(migrations[1].Statements[4], false);
		Assert.IsType<ICreateIndexMigrationStatement>(migrations[1].Statements[5], false);
	}

	[Fact]
	public void FromFirstToFourthMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "1_FirstMigration", "4_FourthMigration");

		Assert.StrictEqual(3, migrations.Count);
		var statement = Assert.Single(migrations[0].Statements);
		Assert.IsType<ISqlMigrationStatement>(statement, false);
		Assert.StrictEqual(6, migrations[1].Statements.Count);
		Assert.IsType<ICreateTableMigrationStatement>(migrations[1].Statements[0], false);
		Assert.IsType<IAlterTableMigrationStatement>(migrations[1].Statements[1], false);
		Assert.IsType<ISqlMigrationStatement>(migrations[1].Statements[2], false);
		Assert.IsType<IAlterTableMigrationStatement>(migrations[1].Statements[3], false);
		Assert.IsType<ICreateForeignKeyMigrationStatement>(migrations[1].Statements[4], false);
		Assert.IsType<ICreateIndexMigrationStatement>(migrations[1].Statements[5], false);
		statement = Assert.Single(migrations[2].Statements);
		Assert.IsType<ICreateIndexMigrationStatement>(statement, false);
	}

	[Fact]
	public void FromSecondToThirdMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "2_SecondMigration", "3_ThirdMigration");

		var migration = Assert.Single(migrations);
		Assert.StrictEqual(6, migration.Statements.Count);
		Assert.IsType<ICreateTableMigrationStatement>(migration.Statements[0], false);
		Assert.IsType<IAlterTableMigrationStatement>(migration.Statements[1], false);
		Assert.IsType<ISqlMigrationStatement>(migration.Statements[2], false);
		Assert.IsType<IAlterTableMigrationStatement>(migration.Statements[3], false);
		Assert.IsType<ICreateForeignKeyMigrationStatement>(migration.Statements[4], false);
		Assert.IsType<ICreateIndexMigrationStatement>(migration.Statements[5], false);
	}

	[Fact]
	public void FromSecondToFourthMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "2_SecondMigration", "4_FourthMigration");

		Assert.StrictEqual(2, migrations.Count);
		Assert.StrictEqual(6, migrations[0].Statements.Count);
		Assert.IsType<ICreateTableMigrationStatement>(migrations[0].Statements[0], false);
		Assert.IsType<IAlterTableMigrationStatement>(migrations[0].Statements[1], false);
		Assert.IsType<ISqlMigrationStatement>(migrations[0].Statements[2], false);
		Assert.IsType<IAlterTableMigrationStatement>(migrations[0].Statements[3], false);
		Assert.IsType<ICreateForeignKeyMigrationStatement>(migrations[0].Statements[4], false);
		Assert.IsType<ICreateIndexMigrationStatement>(migrations[0].Statements[5], false);
		var statement = Assert.Single(migrations[1].Statements);
		Assert.IsType<ICreateIndexMigrationStatement>(statement, false);
	}

	[Fact]
	public void FromThirdToFourthMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "3_ThirdMigration", "4_FourthMigration");

		var migration = Assert.Single(migrations);
		var statement = Assert.Single(migration.Statements);
		Assert.IsType<ICreateIndexMigrationStatement>(statement, false);
	}

	[MigrationName("1_FirstMigration")]
	private sealed class FirstMigration : Migration
	{
		public override void Up()
		{
			Create.Table("Account")
				.WithColumn("Id").AsInt().Identity()
				.WithColumn("Number").AsString().MaximumLength(50).NotNull();

			Create.PrimaryKey("PK_Account")
				.OnTable("Account")
				.WithColumn("Id");
		}
	}

	[MigrationName("2_SecondMigration")]
	private sealed class SecondMigration : Migration
	{
		public override void Up()
		{
			Sql("""
				INSERT INTO [dbo].[Account] ([Number])
				VALUES (N'TST1234567890');
				""");
		}
	}

	[MigrationName("3_ThirdMigration")]
	private sealed class ThirdMigration : Migration
	{
		public override void Up()
		{
			Create.Table("Customer")
				.WithColumn("Id").AsInt().Identity()
				.WithColumn("Name").AsString().MaximumLength(200).NotNull();

			Alter.Table("Account")
				.AddColumn("CustomerId").AsInt().Null();

			Sql("""
				INSERT INTO [dbo].[Customer] ([Name])
				VALUES (N'Tom Tester');

				UPDATE [dbo].[Account]
				SET [CustomerId] = SCOPE_IDENTITY();
				""");

			Alter.Table("Account")
				.AlterColumn("CustomerId").AsInt().NotNull();

			Create.ForeignKey("FK_Account_Customer")
				.FromTable("Account").FromColumn("CustomerId")
				.ToTable("Customer").ToColumn("Id")
				.OnDeleteRestrict();

			Create.Index("IX_Account_CustomerId")
				.OnTable("Account")
				.OnColumn("CustomerId")
				.Ascending();
		}
	}

	[MigrationName("4_FourthMigration")]
	private sealed class FourthMigration : Migration
	{
		public override void Up()
		{
			Create.UniqueIndex("UIX_Account_Number")
				.OnTable("Account")
				.OnColumn("Number").Ascending();
		}
	}
}
