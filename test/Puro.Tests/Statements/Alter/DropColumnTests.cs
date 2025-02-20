﻿using Puro.Statements.Alter.Table;
using Xunit;

namespace Puro.Tests.Statements.Alter;

public class DropColumnTests
{
	[Fact]
	public void NullColumnNameThrows()
	{
		var migration = new NullColumnNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullColumnNameMigration : Migration
	{
		public override void Up()
		{
			Alter.Table("table").InSchema("schema")
				.DropColumn(null!);
		}
	}

	[Fact]
	public void EmptyColumnNameThrows()
	{
		var migration = new EmptyColumnNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class EmptyColumnNameMigration : Migration
	{
		public override void Up()
		{
			Alter.Table("table").InSchema("schema")
				.DropColumn(string.Empty);
		}
	}

	[Fact]
	public void WhiteSpaceColumnNameThrows()
	{
		var migration = new WhiteSpaceColumnNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpaceColumnNameMigration : Migration
	{
		public override void Up()
		{
			Alter.Table("table").InSchema("schema")
				.DropColumn("     ");
		}
	}

	[Fact]
	public void StatementReturnsTableName()
	{
		var migration = new SingleColumnDropMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("TestTable", statement.Table);
	}

	[Fact]
	public void StatementReturnsSchemaName()
	{
		var migration = new SingleColumnDropMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("TestSchema", statement.Schema);
	}

	[Fact]
	public void StatementReturnsColumnName()
	{
		var migration = new SingleColumnDropMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		var (changeType, column) = Assert.Single(statement.ColumnChanges);
		Assert.StrictEqual(TableColumnChangeType.Drop, changeType);
		Assert.Equal("TestColumn", column.Name);
	}

	private sealed class SingleColumnDropMigration : Migration
	{
		public override void Up()
		{
			Alter.Table("TestTable").InSchema("TestSchema")
				.DropColumn("TestColumn");
		}
	}

	[Fact]
	public void StatementReturnsMultipleColumnNames()
	{
		var migration = new MultipleColumnDropMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.StrictEqual(4, statement.ColumnChanges.Count);
		Assert.All(statement.ColumnChanges, c => Assert.StrictEqual(TableColumnChangeType.Drop, c.ChangeType));
		Assert.Equal("FirstColumn", statement.ColumnChanges[0].Column.Name);
		Assert.Equal("SecondColumn", statement.ColumnChanges[1].Column.Name);
		Assert.Equal("ThirdColumn", statement.ColumnChanges[2].Column.Name);
		Assert.Equal("FourthColumn", statement.ColumnChanges[3].Column.Name);
	}

	private sealed class MultipleColumnDropMigration : Migration
	{
		public override void Up()
		{
			Alter.Table("TestTable").InSchema("TestSchema")
				.DropColumn("FirstColumn")
				.DropColumn("SecondColumn")
				.DropColumn("ThirdColumn")
				.DropColumn("FourthColumn");
		}
	}
}
