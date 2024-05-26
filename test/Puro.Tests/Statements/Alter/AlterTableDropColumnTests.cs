using Puro.Statements.Alter.Table;
using Xunit;

namespace Puro.Tests.Statements.Alter;

public class AlterTableDropColumnTests
{
	[Fact]
	public void NullColumnNameThrows()
	{
		var migration = new NullColumnNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullColumnNameMigration : UpMigration
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

	private sealed class EmptyColumnNameMigration : UpMigration
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

	private sealed class WhiteSpaceColumnNameMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("table").InSchema("schema")
				.DropColumn("     ");
		}
	}

	private sealed class DuplicateColumnNameMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("table").InSchema("schema")
				.DropColumn("column")
				.DropColumn("column");
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
		var change = Assert.Single(statement.ColumnChanges);
		Assert.StrictEqual(TableColumnChangeType.Drop, change.Type);
		Assert.Equal("TestColumn", change.Column.Name);
	}

	private sealed class SingleColumnDropMigration : UpMigration
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
		Assert.All(statement.ColumnChanges, c => Assert.StrictEqual(TableColumnChangeType.Drop, c.Type));
		Assert.Equal("FirstColumn", statement.ColumnChanges[0].Column.Name);
		Assert.Equal("SecondColumn", statement.ColumnChanges[1].Column.Name);
		Assert.Equal("ThirdColumn", statement.ColumnChanges[2].Column.Name);
		Assert.Equal("FourthColumn", statement.ColumnChanges[3].Column.Name);
	}

	private sealed class MultipleColumnDropMigration : UpMigration
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
