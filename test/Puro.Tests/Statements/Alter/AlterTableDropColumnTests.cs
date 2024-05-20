using Puro.Exceptions;
using Puro.Statements.Alter.Table;
using Xunit;

namespace Puro.Tests.Statements.Alter;

public class AlterTableDropColumnTests
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

	[Fact]
	public void DuplicateColumnNameThrows()
	{
		var migration = new DuplicateColumnNameMigration();

		Assert.Throws<DuplicateDropColumnException>(migration.Up);
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
		var column = Assert.Single(statement.DropColumns);
		Assert.Equal("TestColumn", column);
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
		Assert.StrictEqual(4, statement.DropColumns.Count);
		Assert.Equal("FirstColumn", statement.DropColumns[0]);
		Assert.Equal("SecondColumn", statement.DropColumns[1]);
		Assert.Equal("ThirdColumn", statement.DropColumns[2]);
		Assert.Equal("FourthColumn", statement.DropColumns[3]);
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
