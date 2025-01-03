﻿using Puro.Statements.Drop.Table;
using Xunit;

namespace Puro.Tests.Statements.Drop;

public class DropTableTests
{
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
			Drop.Table(null!);
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
			Drop.Table("table").InSchema(null!);
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
			Drop.Table(string.Empty);
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
			Drop.Table("table").InSchema(string.Empty);
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
			Drop.Table("        ");
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
			Drop.Table("table").InSchema("        ");
		}
	}

	[Fact]
	public void SchemalessStatementReturnsTableName()
	{
		var migration = new TableNameMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IDropTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("TestTable", statement.Table);
	}

	[Fact]
	public void NullSchemaReturnedWhenNoSchemaDefined()
	{
		var migration = new TableNameMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IDropTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.Null(statement.Schema);
	}

	private sealed class TableNameMigration : Migration
	{
		public override void Up()
		{
			Drop.Table("TestTable");
		}
	}

	[Fact]
	public void DefinedSchemaReturned()
	{
		var migration = new SchemaMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IDropTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("TestSchema", statement.Schema);
	}

	[Fact]
	public void StatementReturnsTableName()
	{
		var migration = new SchemaMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IDropTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("TestTable", statement.Table);
	}

	private sealed class SchemaMigration : Migration
	{
		public override void Up()
		{
			Drop.Table("TestTable").InSchema("TestSchema");
		}
	}
}
