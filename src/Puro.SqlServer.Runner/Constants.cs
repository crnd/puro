namespace Puro.SqlServer.Runner;

internal static class Constants
{
	public const string DefaultSchema = "dbo";

	public const string MigrationTableCreation = """
		IF OBJECT_ID(N'[dbo].[__PuroMigrationsHistory]') IS NULL
		BEGIN
			CREATE TABLE [dbo].[__PuroMigrationsHistory] (
				[MigrationName] NVARCHAR(150) NOT NULL,
				[AppliedOn] DATETIME2 NOT NULL,
				CONSTRAINT [PK___PuroMigrationsHistory] PRIMARY KEY ([MigrationName])
			);
		END;
		""";
}
