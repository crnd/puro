﻿using Puro.SqlServer.Runner.Exceptions;
using Puro.SqlServer.Runner.Generators.Alter;
using Puro.SqlServer.Runner.Generators.Create;
using Puro.SqlServer.Runner.Generators.Drop;
using Puro.SqlServer.Runner.Generators.Rename;
using Puro.Statements.Alter.Table;
using Puro.Statements.Create.ForeignKey;
using Puro.Statements.Create.Index;
using Puro.Statements.Create.PrimaryKey;
using Puro.Statements.Create.Table;
using Puro.Statements.Drop.Constraint;
using Puro.Statements.Drop.Index;
using Puro.Statements.Drop.Table;
using Puro.Statements.Rename.Column;
using Puro.Statements.Rename.Index;
using Puro.Statements.Rename.Table;
using Puro.Statements.Sql;
using System.Text;

namespace Puro.SqlServer.Runner.Generators;

internal static class MigrationSqlGenerator
{
	private const string DefaultSchema = "dbo";

	public static string Generate(List<Migration> migrations)
	{
		if (migrations.Count == 0)
		{
			throw new MigrationsNotFoundException();
		}

		var sqlBuilder = new StringBuilder("""
			IF OBJECT_ID(N'[dbo].[__PuroMigrationsHistory]') IS NULL
			BEGIN

			CREATE TABLE [dbo].[__PuroMigrationsHistory] (
			[MigrationName] NVARCHAR(150) NOT NULL,
			[AppliedOn] DATETIME2 NOT NULL,
			CONSTRAINT [PK___PuroMigrationsHistory] PRIMARY KEY ([MigrationName]));

			END;

			BEGIN TRANSACTION;


			""");

		foreach (var migration in migrations)
		{
			sqlBuilder.AppendLine($"""
				IF NOT EXISTS (SELECT 1 FROM [dbo].[__PuroMigrationsHistory] WHERE [MigrationName] = N'{migration.Name}')
				BEGIN

				""");

			var schema = migration.Schema ?? DefaultSchema;

			foreach (var migrationStatement in migration.Statements)
			{
				var sql = migrationStatement switch
				{
					IAlterTableMigrationStatement statement => AlterTableGenerator.Generate(statement, schema),
					ICreateForeignKeyMigrationStatement statement => CreateForeignKeyGenerator.Generate(statement, schema),
					ICreateIndexMigrationStatement statement => CreateIndexGenerator.Generate(statement, schema),
					ICreatePrimaryKeyMigrationStatement statement => CreatePrimaryKeyGenerator.Generate(statement, schema),
					ICreateTableMigrationStatement statement => CreateTableGenerator.Generate(statement, schema),
					IDropConstraintMigrationStatement statement => DropConstraintGenerator.Generate(statement, schema),
					IDropIndexMigrationStatement statement => DropIndexGenerator.Generate(statement, schema),
					IDropTableMigrationStatement statement => DropTableGenerator.Generate(statement, schema),
					IRenameColumnMigrationStatement statement => RenameColumnGenerator.Generate(statement, schema),
					IRenameIndexMigrationStatement statement => RenameIndexGenerator.Generate(statement, schema),
					IRenameTableMigrationStatement statement => RenameTableGenerator.Generate(statement, schema),
					ISqlMigrationStatement statement => statement.Sql,
					_ => throw new UnsupportedMigrationStatementException(migrationStatement.GetType())
				};

				sqlBuilder.AppendLine(sql).AppendLine();
			}

			sqlBuilder.AppendLine($"""
				INSERT INTO [dbo].[__PuroMigrationsHistory] ([MigrationName], [AppliedOn])
				VALUES (N'{migration.Name}', SYSUTCDATETIME());

				END

				""");
		}

		sqlBuilder.Append("COMMIT TRANSACTION;");

		return sqlBuilder.ToString();
	}
}
