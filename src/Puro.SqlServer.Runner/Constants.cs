namespace Puro.SqlServer.Runner;

internal static class Constants
{
	public const string AssemblyShortForm = "-a";

	public const string AssemblyLongForm = "--assembly";

	public const string FromMigrationShortForm = "-f";

	public const string FromMigrationLongForm = "--from-migration";

	public const string ToMigrationShortForm = "-t";

	public const string ToMigrationLongForm = "--to-migration";

	public const string ConnectionShortFrom = "-c";

	public const string ConnectionLongForm = "--connection";

	public const string DefaultSchema = "dbo";

	public const string MigrationTableCreation = """
		IF OBJECT_ID(N'[dbo].[__PuroMigrationsHistory]') IS NULL
		BEGIN

		CREATE TABLE [dbo].[__PuroMigrationsHistory] (
		[MigrationName] NVARCHAR(150) NOT NULL,
		[AppliedOn] DATETIME2 NOT NULL,
		CONSTRAINT [PK___PuroMigrationsHistory] PRIMARY KEY ([MigrationName]));

		END;

		BEGIN TRANSACTION;

		""";
}
