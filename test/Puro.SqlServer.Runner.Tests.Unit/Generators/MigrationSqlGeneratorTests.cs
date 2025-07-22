using Puro.SqlServer.Runner.Exceptions;
using Puro.SqlServer.Runner.Generators;
using Xunit;

namespace Puro.SqlServer.Runner.Tests.Unit.Generators;

public class MigrationSqlGeneratorTests
{
	[Fact]
	public void NoMigrations()
	{
		Assert.Throws<MigrationsNotFoundException>(() => MigrationSqlGenerator.Generate([]));
	}

	[Fact]
	public void SingleMigration()
	{
		var migration = new CreateTableMigration();
		migration.Up();

		var sql = MigrationSqlGenerator.Generate([migration]);

		const string expected = """
			IF OBJECT_ID(N'[dbo].[__PuroMigrationsHistory]') IS NULL
			BEGIN

			CREATE TABLE [dbo].[__PuroMigrationsHistory] (
			[MigrationName] NVARCHAR(150) NOT NULL,
			[AppliedOn] DATETIME2 NOT NULL,
			CONSTRAINT [PK___PuroMigrationsHistory] PRIMARY KEY ([MigrationName]));

			END;

			BEGIN TRANSACTION;

			IF NOT EXISTS (SELECT 1 FROM [dbo].[__PuroMigrationsHistory] WHERE [MigrationName] = N'1_CreateTable')
			BEGIN

			CREATE TABLE [dbo].[Book] (
			[Id] INT NOT NULL IDENTITY(1, 1),
			[Name] NVARCHAR(MAX) NOT NULL);

			ALTER TABLE [dbo].[Book]
			ADD CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED ([Id]);

			INSERT INTO [dbo].[__PuroMigrationsHistory] ([MigrationName], [AppliedOn])
			VALUES (N'1_CreateTable', SYSUTCDATETIME());

			END

			COMMIT TRANSACTION;
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void MultipleMigrations()
	{
		var migration1 = new CreateTableMigration();
		migration1.Up();
		var migration2 = new DeleteBooksEmptyNamesMigration();
		migration2.Up();
		var migration3 = new CreateIndexMigration();
		migration3.Up();

		var sql = MigrationSqlGenerator.Generate([migration1, migration2, migration3]);

		const string expected = """
			IF OBJECT_ID(N'[dbo].[__PuroMigrationsHistory]') IS NULL
			BEGIN

			CREATE TABLE [dbo].[__PuroMigrationsHistory] (
			[MigrationName] NVARCHAR(150) NOT NULL,
			[AppliedOn] DATETIME2 NOT NULL,
			CONSTRAINT [PK___PuroMigrationsHistory] PRIMARY KEY ([MigrationName]));

			END;

			BEGIN TRANSACTION;

			IF NOT EXISTS (SELECT 1 FROM [dbo].[__PuroMigrationsHistory] WHERE [MigrationName] = N'1_CreateTable')
			BEGIN

			CREATE TABLE [dbo].[Book] (
			[Id] INT NOT NULL IDENTITY(1, 1),
			[Name] NVARCHAR(MAX) NOT NULL);

			ALTER TABLE [dbo].[Book]
			ADD CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED ([Id]);

			INSERT INTO [dbo].[__PuroMigrationsHistory] ([MigrationName], [AppliedOn])
			VALUES (N'1_CreateTable', SYSUTCDATETIME());

			END

			IF NOT EXISTS (SELECT 1 FROM [dbo].[__PuroMigrationsHistory] WHERE [MigrationName] = N'2_DeleteBooksWithEmptyNames')
			BEGIN
			
			DELETE FROM [dbo].[Book]
			WHERE [Name] = N'';
			
			INSERT INTO [dbo].[__PuroMigrationsHistory] ([MigrationName], [AppliedOn])
			VALUES (N'2_DeleteBooksWithEmptyNames', SYSUTCDATETIME());
			
			END

			IF NOT EXISTS (SELECT 1 FROM [dbo].[__PuroMigrationsHistory] WHERE [MigrationName] = N'3_CreateIndex')
			BEGIN

			CREATE UNIQUE INDEX [UIX_Book_Name]
			ON [dbo].[Book] ([Name] ASC);

			INSERT INTO [dbo].[__PuroMigrationsHistory] ([MigrationName], [AppliedOn])
			VALUES (N'3_CreateIndex', SYSUTCDATETIME());

			END

			COMMIT TRANSACTION;
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
	}

	[MigrationName("1_CreateTable")]
	private sealed class CreateTableMigration : Migration
	{
		public override void Up()
		{
			Create.Table("Book")
				.WithColumn("Id").AsInt().Identity()
				.WithColumn("Name").AsString().NotNull();

			Create.PrimaryKey("PK_Book")
				.OnTable("Book")
				.WithColumn("Id");
		}
	}

	[MigrationName("2_DeleteBooksWithEmptyNames")]
	private sealed class DeleteBooksEmptyNamesMigration : Migration
	{
		public override void Up()
		{
			Sql("""
				DELETE FROM [dbo].[Book]
				WHERE [Name] = N'';
				""");
		}
	}

	[MigrationName("3_CreateIndex")]
	private sealed class CreateIndexMigration : Migration
	{
		public override void Up()
		{
			Create.UniqueIndex("UIX_Book_Name")
				.OnTable("Book")
				.OnColumn("Name")
				.Ascending();
		}
	}
}
