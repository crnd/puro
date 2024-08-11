using Puro.Statements.Alter.Table;
using Xunit;

namespace Puro.Tests.Statements.Alter;

public class AlterTableTests
{
	[Fact]
	public void NullTableNameThrows()
	{
		var migration = new NullTableNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullTableNameMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table(null!).InSchema("schema")
				.DropColumn("column");
		}
	}

	[Fact]
	public void NullSchemaNameThrows()
	{
		var migration = new NullSchemaNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullSchemaNameMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("table").InSchema(null!)
				.DropColumn("column");
		}
	}

	[Fact]
	public void EmptyTableNameThrows()
	{
		var migration = new EmptyTableNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class EmptyTableNameMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table(string.Empty).InSchema("schema")
				.DropColumn("column");
		}
	}

	[Fact]
	public void EmptySchemaNameThrows()
	{
		var migration = new EmptySchemaNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class EmptySchemaNameMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("table").InSchema(string.Empty)
				.DropColumn("column");
		}
	}

	[Fact]
	public void WhiteSpaceTableNameThrows()
	{
		var migration = new WhiteSpaceTableNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpaceTableNameMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("     ").InSchema("schema")
				.DropColumn("column");
		}
	}

	[Fact]
	public void WhiteSpaceSchemaNameThrows()
	{
		var migration = new WhiteSpaceSchemaNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpaceSchemaNameMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("table").InSchema("     ")
				.DropColumn("column");
		}
	}

	[Fact]
	public void StatementReturnsEmptyListWhenNoColumns()
	{
		var migration = new NoColumnsMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.Empty(statement.ColumnChanges);
	}

	[Fact]
	public void StatementReturnsTableName()
	{
		var migration = new NoColumnsMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("TestTable", statement.Table);
	}

	[Fact]
	public void StatementReturnsSchemaName()
	{
		var migration = new NoColumnsMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("TestSchema", statement.Schema);
	}

	private sealed class NoColumnsMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("TestTable").InSchema("TestSchema");
		}
	}

	[Fact]
	public void StatementReturnsMultipleColumns()
	{
		var migration = new MultipleColumnsMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.StrictEqual(4, statement.ColumnChanges.Count);

		var (changeType, column) = statement.ColumnChanges[0];
		Assert.StrictEqual(TableColumnChangeType.Add, changeType);
		Assert.Equal("Count", column.Name);
		Assert.StrictEqual(typeof(int), column.Type);
		Assert.True(column.Nullable);

		(changeType, column) = statement.ColumnChanges[1];
		Assert.StrictEqual(TableColumnChangeType.Drop, changeType);
		Assert.Equal("Name", column.Name);

		(changeType, column) = statement.ColumnChanges[2];
		Assert.StrictEqual(TableColumnChangeType.Add, changeType);
		Assert.Equal("Description", column.Name);
		Assert.StrictEqual(typeof(string), column.Type);
		Assert.True(column.Nullable);
		Assert.Null(column.FixedLength);
		Assert.StrictEqual(1000, column.MaximumLength);

		(changeType, column) = statement.ColumnChanges[3];
		Assert.StrictEqual(TableColumnChangeType.Drop, changeType);
		Assert.Equal("LastUpdated", column.Name);
	}

	private sealed class MultipleColumnsMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("TestTable").InSchema("TestSchema")
				.AddColumn("Count").AsInt().Null()
				.DropColumn("Name")
				.AddColumn("Description").AsString().MaximumLength(1000).Null()
				.DropColumn("LastUpdated");
		}
	}

	[Fact]
	public void NoSchemaStatementReturnsTable()
	{
		var migration = new NoSchemaMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("Account", statement.Table);
	}

	[Fact]
	public void NoSchemaStatementReturnsColumnChanges()
	{
		var migration = new NoSchemaMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.StrictEqual(2, statement.ColumnChanges.Count);

		var (changeType, column) = statement.ColumnChanges[0];
		Assert.StrictEqual(TableColumnChangeType.Add, changeType);
		Assert.Equal("Version", column.Name);
		Assert.StrictEqual(typeof(int), column.Type);
		Assert.True(column.Nullable);

		(changeType, column) = statement.ColumnChanges[1];
		Assert.StrictEqual(TableColumnChangeType.Drop, changeType);
		Assert.Equal("AccountVersion", column.Name);
	}

	private sealed class NoSchemaMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("Account")
				.AddColumn("Version").AsInt().Null()
				.DropColumn("AccountVersion");
		}
	}
}
