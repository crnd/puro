﻿using Puro.Statements.Rename.Index;
using Xunit;

namespace Puro.Tests.Statements.Rename;

public class RenameIndexTests
{
	[Fact]
	public void NullCurrentIndexNameThrows()
	{
		var migration = new NullCurrentIndexNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullCurrentIndexNameMigration : Migration
	{
		public override void Up()
		{
			Rename.Index(null!).InTable("table").InSchema("schema").To("new");
		}
	}

	[Fact]
	public void NullTableNameThrows()
	{
		var migration = new NullTableNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullTableNameMigration : Migration
	{
		public override void Up()
		{
			Rename.Index("current").InTable(null!).InSchema("schema").To("new");
		}
	}

	[Fact]
	public void NullSchemaNameThrows()
	{
		var migration = new NullSchemaNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullSchemaNameMigration : Migration
	{
		public override void Up()
		{
			Rename.Index("current").InTable("table").InSchema(null!).To("new");
		}
	}

	[Fact]
	public void NullNewIndexNameThrows()
	{
		var migration = new NullNewIndexNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullNewIndexNameMigration : Migration
	{
		public override void Up()
		{
			Rename.Index("current").InTable("table").InSchema("schema").To(null!);
		}
	}

	[Fact]
	public void EmptyCurrentIndexNameThrows()
	{
		var migration = new EmptyCurrentIndexNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class EmptyCurrentIndexNameMigration : Migration
	{
		public override void Up()
		{
			Rename.Index(string.Empty).InTable("table").InSchema("schema").To("new");
		}
	}

	[Fact]
	public void EmptyTableNameThrows()
	{
		var migration = new EmptyTableNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class EmptyTableNameMigration : Migration
	{
		public override void Up()
		{
			Rename.Index("current").InTable(string.Empty).InSchema("schema").To("new");
		}
	}

	[Fact]
	public void EmptySchemaNameThrows()
	{
		var migration = new EmptySchemaNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class EmptySchemaNameMigration : Migration
	{
		public override void Up()
		{
			Rename.Index("current").InTable("table").InSchema(string.Empty).To("new");
		}
	}

	[Fact]
	public void EmptyNewIndexNameThrows()
	{
		var migration = new EmptyNewIndexNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class EmptyNewIndexNameMigration : Migration
	{
		public override void Up()
		{
			Rename.Index("current").InTable("table").InSchema("schema").To(string.Empty);
		}
	}

	[Fact]
	public void WhiteSpaceCurrentIndexNameThrows()
	{
		var migration = new WhiteSpaceCurrentIndexNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpaceCurrentIndexNameMigration : Migration
	{
		public override void Up()
		{
			Rename.Index("     ").InTable("table").InSchema("schema").To("new");
		}
	}

	[Fact]
	public void WhiteSpaceTableNameThrows()
	{
		var migration = new WhiteSpaceTableNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpaceTableNameMigration : Migration
	{
		public override void Up()
		{
			Rename.Index("current").InTable("     ").InSchema("schema").To("new");
		}
	}

	[Fact]
	public void WhiteSpaceSchemaNameThrows()
	{
		var migration = new WhiteSpaceSchemaNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpaceSchemaNameMigration : Migration
	{
		public override void Up()
		{
			Rename.Index("current").InTable("table").InSchema("     ").To("new");
		}
	}

	[Fact]
	public void WhiteSpaceNewIndexNameThrows()
	{
		var migration = new WhiteSpaceNewIndexNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpaceNewIndexNameMigration : Migration
	{
		public override void Up()
		{
			Rename.Index("current").InTable("table").InSchema("schema").To("     ");
		}
	}

	[Fact]
	public void StatementReturnsCurrentIndexName()
	{
		var migration = new MigrationWithSchema();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IRenameIndexMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("IX_AccountNumber", statement.CurrentName);
	}

	[Fact]
	public void StatementReturnsTableName()
	{
		var migration = new MigrationWithSchema();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IRenameIndexMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("Account", statement.Table);
	}

	[Fact]
	public void StatementReturnsSchemaName()
	{
		var migration = new MigrationWithSchema();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IRenameIndexMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("Banking", statement.Schema);
	}

	[Fact]
	public void StatementReturnsNewIndexName()
	{
		var migration = new MigrationWithSchema();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IRenameIndexMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("UIX_AccountNumber", statement.NewName);
	}

	private sealed class MigrationWithSchema : Migration
	{
		public override void Up()
		{
			Rename.Index("IX_AccountNumber").InTable("Account").InSchema("Banking").To("UIX_AccountNumber");
		}
	}

	[Fact]
	public void StatementWithoutSchemaReturnsCurrentIndexName()
	{
		var migration = new MigrationWithoutSchema();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IRenameIndexMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("IX_AccountNumber", statement.CurrentName);
	}

	[Fact]
	public void StatementWithoutSchemaReturnsTableName()
	{
		var migration = new MigrationWithoutSchema();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IRenameIndexMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("Account", statement.Table);
	}

	[Fact]
	public void StatementWithoutSchemaReturnsNewIndexName()
	{
		var migration = new MigrationWithoutSchema();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IRenameIndexMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("UIX_AccountNumber", statement.NewName);
	}

	[Fact]
	public void StatementWithoutSchemaReturnsNullSchema()
	{
		var migration = new MigrationWithoutSchema();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IRenameIndexMigrationStatement;
		Assert.NotNull(statement);
		Assert.Null(statement.Schema);
	}

	private sealed class MigrationWithoutSchema : Migration
	{
		public override void Up()
		{
			Rename.Index("IX_AccountNumber").InTable("Account").To("UIX_AccountNumber");
		}
	}
}
