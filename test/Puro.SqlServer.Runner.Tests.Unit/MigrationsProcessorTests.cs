using Puro.SqlServer.Runner.Exceptions;
using Puro.Statements.Alter.Table;
using Puro.Statements.Create.ForeignKey;
using Puro.Statements.Create.Index;
using Puro.Statements.Create.PrimaryKey;
using Puro.Statements.Create.Table;
using Puro.Statements.Drop.Constraint;
using Puro.Statements.Drop.Index;
using Puro.Statements.Drop.Table;
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

	[Theory]
	[InlineData(null, null, true)]
	[InlineData(null, 10, true)]
	[InlineData(2, null, true)]
	[InlineData(2, 10, true)]
	[InlineData(5, 5, true)]
	[InlineData(5, 3, false)]
	internal void MigrationDirectionTests(int? fromIndex, int? toIndex, bool expectedDirectionIsUp)
	{
		var isUpDirection = MigrationsProcessor.IsUpDirection(fromIndex, toIndex);

		Assert.StrictEqual(expectedDirectionIsUp, isUpDirection);
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
	public void FromSecondToFirstMigrationReturnsCorrectMigration()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "2_SecondMigration", "1_FirstMigration");

		var migration = Assert.Single(migrations);
		Assert.Equal("2_SecondMigration", migration.Name);
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
	public void FromThirdToFirstMigrationReturnsCorrectMigrations()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "3_ThirdMigration", "1_FirstMigration");

		Assert.StrictEqual(2, migrations.Count);
		Assert.Equal("3_ThirdMigration", migrations[0].Name);
		Assert.Equal("2_SecondMigration", migrations[1].Name);
	}

	[Fact]
	public void FromThirdToSecondMigrationReturnsCorrectMigration()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "3_ThirdMigration", "2_SecondMigration");

		var migration = Assert.Single(migrations);
		Assert.Equal("3_ThirdMigration", migration.Name);
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
	public void FromFourthToFirstMigrationReturnsCorrectMigrations()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "4_FourthMigration", "1_FirstMigration");

		Assert.StrictEqual(3, migrations.Count);
		Assert.Equal("4_FourthMigration", migrations[0].Name);
		Assert.Equal("3_ThirdMigration", migrations[1].Name);
		Assert.Equal("2_SecondMigration", migrations[2].Name);
	}

	[Fact]
	public void FromFourthToSecondMigrationReturnsCorrectMigrations()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "4_FourthMigration", "2_SecondMigration");

		Assert.StrictEqual(2, migrations.Count);
		Assert.Equal("4_FourthMigration", migrations[0].Name);
		Assert.Equal("3_ThirdMigration", migrations[1].Name);
	}

	[Fact]
	public void FromFourthToThirdMigrationReturnsCorrectMigration()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "4_FourthMigration", "3_ThirdMigration");

		var migration = Assert.Single(migrations);
		Assert.Equal("4_FourthMigration", migration.Name);
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
		Assert.IsAssignableFrom<ISqlMigrationStatement>(statement);
		Assert.StrictEqual(6, migrations[1].Statements.Count);
		Assert.IsAssignableFrom<ICreateTableMigrationStatement>(migrations[1].Statements[0]);
		Assert.IsAssignableFrom<IAlterTableMigrationStatement>(migrations[1].Statements[1]);
		Assert.IsAssignableFrom<ISqlMigrationStatement>(migrations[1].Statements[2]);
		Assert.IsAssignableFrom<IAlterTableMigrationStatement>(migrations[1].Statements[3]);
		Assert.IsAssignableFrom<ICreateForeignKeyMigrationStatement>(migrations[1].Statements[4]);
		Assert.IsAssignableFrom<ICreateIndexMigrationStatement>(migrations[1].Statements[5]);
		statement = Assert.Single(migrations[2].Statements);
		Assert.IsAssignableFrom<ICreateIndexMigrationStatement>(statement);
	}

	[Fact]
	public void FromSecondMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "2_SecondMigration", null);

		Assert.StrictEqual(2, migrations.Count);
		Assert.StrictEqual(6, migrations[0].Statements.Count);
		Assert.IsAssignableFrom<ICreateTableMigrationStatement>(migrations[0].Statements[0]);
		Assert.IsAssignableFrom<IAlterTableMigrationStatement>(migrations[0].Statements[1]);
		Assert.IsAssignableFrom<ISqlMigrationStatement>(migrations[0].Statements[2]);
		Assert.IsAssignableFrom<IAlterTableMigrationStatement>(migrations[0].Statements[3]);
		Assert.IsAssignableFrom<ICreateForeignKeyMigrationStatement>(migrations[0].Statements[4]);
		Assert.IsAssignableFrom<ICreateIndexMigrationStatement>(migrations[0].Statements[5]);
		var statement = Assert.Single(migrations[1].Statements);
		Assert.IsAssignableFrom<ICreateIndexMigrationStatement>(statement);
	}

	[Fact]
	public void FromThirdMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "3_ThirdMigration", null);

		var migration = Assert.Single(migrations);
		var statement = Assert.Single(migration.Statements);
		Assert.IsAssignableFrom<ICreateIndexMigrationStatement>(statement);
	}

	[Fact]
	public void ToFirstMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, null, "1_FirstMigration");

		var migration = Assert.Single(migrations);
		Assert.StrictEqual(2, migration.Statements.Count);
		Assert.IsAssignableFrom<ICreateTableMigrationStatement>(migration.Statements[0]);
		Assert.IsAssignableFrom<ICreatePrimaryKeyMigrationStatement>(migration.Statements[1]);
	}

	[Fact]
	public void ToSecondMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, null, "2_SecondMigration");

		Assert.StrictEqual(2, migrations.Count);
		Assert.StrictEqual(2, migrations[0].Statements.Count);
		Assert.IsAssignableFrom<ICreateTableMigrationStatement>(migrations[0].Statements[0]);
		Assert.IsAssignableFrom<ICreatePrimaryKeyMigrationStatement>(migrations[0].Statements[1]);
		var statement = Assert.Single(migrations[1].Statements);
		Assert.IsAssignableFrom<ISqlMigrationStatement>(statement);
	}

	[Fact]
	public void ToThirdMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, null, "3_ThirdMigration");

		Assert.StrictEqual(3, migrations.Count);
		Assert.StrictEqual(2, migrations[0].Statements.Count);
		Assert.IsAssignableFrom<ICreateTableMigrationStatement>(migrations[0].Statements[0]);
		Assert.IsAssignableFrom<ICreatePrimaryKeyMigrationStatement>(migrations[0].Statements[1]);
		var statement = Assert.Single(migrations[1].Statements);
		Assert.IsAssignableFrom<ISqlMigrationStatement>(statement);
		Assert.StrictEqual(6, migrations[2].Statements.Count);
		Assert.IsAssignableFrom<ICreateTableMigrationStatement>(migrations[2].Statements[0]);
		Assert.IsAssignableFrom<IAlterTableMigrationStatement>(migrations[2].Statements[1]);
		Assert.IsAssignableFrom<ISqlMigrationStatement>(migrations[2].Statements[2]);
		Assert.IsAssignableFrom<IAlterTableMigrationStatement>(migrations[2].Statements[3]);
		Assert.IsAssignableFrom<ICreateForeignKeyMigrationStatement>(migrations[2].Statements[4]);
		Assert.IsAssignableFrom<ICreateIndexMigrationStatement>(migrations[2].Statements[5]);
	}

	[Fact]
	public void ToFourthMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, null, "4_FourthMigration");

		Assert.StrictEqual(4, migrations.Count);
		Assert.StrictEqual(2, migrations[0].Statements.Count);
		Assert.IsAssignableFrom<ICreateTableMigrationStatement>(migrations[0].Statements[0]);
		Assert.IsAssignableFrom<ICreatePrimaryKeyMigrationStatement>(migrations[0].Statements[1]);
		var statement = Assert.Single(migrations[1].Statements);
		Assert.IsAssignableFrom<ISqlMigrationStatement>(statement);
		Assert.StrictEqual(6, migrations[2].Statements.Count);
		Assert.IsAssignableFrom<ICreateTableMigrationStatement>(migrations[2].Statements[0]);
		Assert.IsAssignableFrom<IAlterTableMigrationStatement>(migrations[2].Statements[1]);
		Assert.IsAssignableFrom<ISqlMigrationStatement>(migrations[2].Statements[2]);
		Assert.IsAssignableFrom<IAlterTableMigrationStatement>(migrations[2].Statements[3]);
		Assert.IsAssignableFrom<ICreateForeignKeyMigrationStatement>(migrations[2].Statements[4]);
		Assert.IsAssignableFrom<ICreateIndexMigrationStatement>(migrations[2].Statements[5]);
		statement = Assert.Single(migrations[3].Statements);
		Assert.IsAssignableFrom<ICreateIndexMigrationStatement>(statement);
	}

	[Fact]
	public void FromFirstToSecondMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "1_FirstMigration", "2_SecondMigration");

		var migration = Assert.Single(migrations);
		var statement = Assert.Single(migration.Statements);
		Assert.IsAssignableFrom<ISqlMigrationStatement>(statement);
	}

	[Fact]
	public void FromFirstToThirdMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "1_FirstMigration", "3_ThirdMigration");

		Assert.StrictEqual(2, migrations.Count);
		var statement = Assert.Single(migrations[0].Statements);
		Assert.IsAssignableFrom<ISqlMigrationStatement>(statement);
		Assert.StrictEqual(6, migrations[1].Statements.Count);
		Assert.IsAssignableFrom<ICreateTableMigrationStatement>(migrations[1].Statements[0]);
		Assert.IsAssignableFrom<IAlterTableMigrationStatement>(migrations[1].Statements[1]);
		Assert.IsAssignableFrom<ISqlMigrationStatement>(migrations[1].Statements[2]);
		Assert.IsAssignableFrom<IAlterTableMigrationStatement>(migrations[1].Statements[3]);
		Assert.IsAssignableFrom<ICreateForeignKeyMigrationStatement>(migrations[1].Statements[4]);
		Assert.IsAssignableFrom<ICreateIndexMigrationStatement>(migrations[1].Statements[5]);
	}

	[Fact]
	public void FromFirstToFourthMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "1_FirstMigration", "4_FourthMigration");

		Assert.StrictEqual(3, migrations.Count);
		var statement = Assert.Single(migrations[0].Statements);
		Assert.IsAssignableFrom<ISqlMigrationStatement>(statement);
		Assert.StrictEqual(6, migrations[1].Statements.Count);
		Assert.IsAssignableFrom<ICreateTableMigrationStatement>(migrations[1].Statements[0]);
		Assert.IsAssignableFrom<IAlterTableMigrationStatement>(migrations[1].Statements[1]);
		Assert.IsAssignableFrom<ISqlMigrationStatement>(migrations[1].Statements[2]);
		Assert.IsAssignableFrom<IAlterTableMigrationStatement>(migrations[1].Statements[3]);
		Assert.IsAssignableFrom<ICreateForeignKeyMigrationStatement>(migrations[1].Statements[4]);
		Assert.IsAssignableFrom<ICreateIndexMigrationStatement>(migrations[1].Statements[5]);
		statement = Assert.Single(migrations[2].Statements);
		Assert.IsAssignableFrom<ICreateIndexMigrationStatement>(statement);
	}

	[Fact]
	public void FromSecondToFirstMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "2_SecondMigration", "1_FirstMigration");

		var migration = Assert.Single(migrations);
		Assert.Empty(migration.Statements);
	}

	[Fact]
	public void FromSecondToThirdMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "2_SecondMigration", "3_ThirdMigration");

		var migration = Assert.Single(migrations);
		Assert.StrictEqual(6, migration.Statements.Count);
		Assert.IsAssignableFrom<ICreateTableMigrationStatement>(migration.Statements[0]);
		Assert.IsAssignableFrom<IAlterTableMigrationStatement>(migration.Statements[1]);
		Assert.IsAssignableFrom<ISqlMigrationStatement>(migration.Statements[2]);
		Assert.IsAssignableFrom<IAlterTableMigrationStatement>(migration.Statements[3]);
		Assert.IsAssignableFrom<ICreateForeignKeyMigrationStatement>(migration.Statements[4]);
		Assert.IsAssignableFrom<ICreateIndexMigrationStatement>(migration.Statements[5]);
	}

	[Fact]
	public void FromSecondToFourthMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "2_SecondMigration", "4_FourthMigration");

		Assert.StrictEqual(2, migrations.Count);
		Assert.StrictEqual(6, migrations[0].Statements.Count);
		Assert.IsAssignableFrom<ICreateTableMigrationStatement>(migrations[0].Statements[0]);
		Assert.IsAssignableFrom<IAlterTableMigrationStatement>(migrations[0].Statements[1]);
		Assert.IsAssignableFrom<ISqlMigrationStatement>(migrations[0].Statements[2]);
		Assert.IsAssignableFrom<IAlterTableMigrationStatement>(migrations[0].Statements[3]);
		Assert.IsAssignableFrom<ICreateForeignKeyMigrationStatement>(migrations[0].Statements[4]);
		Assert.IsAssignableFrom<ICreateIndexMigrationStatement>(migrations[0].Statements[5]);
		var statement = Assert.Single(migrations[1].Statements);
		Assert.IsAssignableFrom<ICreateIndexMigrationStatement>(statement);
	}

	[Fact]
	public void FromThirdToFirstMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "3_ThirdMigration", "1_FirstMigration");

		Assert.StrictEqual(2, migrations.Count);
		Assert.StrictEqual(3, migrations[0].Statements.Count);
		Assert.IsAssignableFrom<IDropIndexMigrationStatement>(migrations[0].Statements[0]);
		Assert.IsAssignableFrom<IDropConstraintMigrationStatement>(migrations[0].Statements[1]);
		Assert.IsAssignableFrom<IDropTableMigrationStatement>(migrations[0].Statements[2]);
		Assert.Empty(migrations[1].Statements);
	}

	[Fact]
	public void FromThirdToSecondMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "3_ThirdMigration", "2_SecondMigration");

		var migration = Assert.Single(migrations);
		Assert.StrictEqual(3, migration.Statements.Count);
		Assert.IsAssignableFrom<IDropIndexMigrationStatement>(migration.Statements[0]);
		Assert.IsAssignableFrom<IDropConstraintMigrationStatement>(migration.Statements[1]);
		Assert.IsAssignableFrom<IDropTableMigrationStatement>(migration.Statements[2]);
	}

	[Fact]
	public void FromThirdToFourthMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "3_ThirdMigration", "4_FourthMigration");

		var migration = Assert.Single(migrations);
		var statement = Assert.Single(migration.Statements);
		Assert.IsAssignableFrom<ICreateIndexMigrationStatement>(statement);
	}

	[Fact]
	public void FromFourthToFirstMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "4_FourthMigration", "1_FirstMigration");

		Assert.StrictEqual(3, migrations.Count);
		var statement = Assert.Single(migrations[0].Statements);
		Assert.IsAssignableFrom<IDropIndexMigrationStatement>(statement);
		Assert.StrictEqual(3, migrations[1].Statements.Count);
		Assert.IsAssignableFrom<IDropIndexMigrationStatement>(migrations[1].Statements[0]);
		Assert.IsAssignableFrom<IDropConstraintMigrationStatement>(migrations[1].Statements[1]);
		Assert.IsAssignableFrom<IDropTableMigrationStatement>(migrations[1].Statements[2]);
		Assert.Empty(migrations[2].Statements);
	}

	[Fact]
	public void FromFourthToSecondMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "4_FourthMigration", "2_SecondMigration");

		Assert.StrictEqual(2, migrations.Count);
		var statement = Assert.Single(migrations[0].Statements);
		Assert.IsAssignableFrom<IDropIndexMigrationStatement>(statement);
		Assert.StrictEqual(3, migrations[1].Statements.Count);
		Assert.IsAssignableFrom<IDropIndexMigrationStatement>(migrations[1].Statements[0]);
		Assert.IsAssignableFrom<IDropConstraintMigrationStatement>(migrations[1].Statements[1]);
		Assert.IsAssignableFrom<IDropTableMigrationStatement>(migrations[1].Statements[2]);
	}

	[Fact]
	public void FromFourthToThirdMigrationReturnsCorrectMigrationStatements()
	{
		var migrations = MigrationsProcessor.Prepare(migrationTypes, "4_FourthMigration", "3_ThirdMigration");

		var migration = Assert.Single(migrations);
		var statement = Assert.Single(migration.Statements);
		Assert.IsAssignableFrom<IDropIndexMigrationStatement>(statement);
	}

	[MigrationName("1_FirstMigration")]
	private sealed class FirstMigration : Migration
	{
		public override void Down()
		{
			Drop.Table("Account");
		}

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
	private sealed class SecondMigration : UpMigration
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
		public override void Down()
		{
			Drop.Index("IX_Account_CustomerId")
				.FromTable("Account");

			Drop.Constraint("FK_Account_Customer")
				.FromTable("Account");

			Drop.Table("Customer");
		}

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
		public override void Down()
		{
			Drop.Index("UIX_Account_Number");
		}

		public override void Up()
		{
			Create.UniqueIndex("UIX_Account_Number")
				.OnTable("Account")
				.OnColumn("Number").Ascending();
		}
	}
}
