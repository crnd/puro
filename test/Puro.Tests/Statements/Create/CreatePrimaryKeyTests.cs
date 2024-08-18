using Puro.Exceptions;
using Puro.Statements.Create.PrimaryKey;
using Xunit;

namespace Puro.Tests.Statements.Create;

public class CreatePrimaryKeyTests
{
	[Fact]
	public void NullPrimaryKeyNameThrows()
	{
		var migration = new NullPrimaryKeyNameMigration();

		Assert.Throws<ArgumentNullException>(() => migration.Up());
	}

	private sealed class NullPrimaryKeyNameMigration : UpMigration
	{
		public override void Up()
		{
			Create.PrimaryKey(null!).OnTable("table").InSchema("schema").WithColumn("column");
		}
	}

	[Fact]
	public void NullTableNameThrows()
	{
		var migration = new NullTableNameMigration();

		Assert.Throws<ArgumentNullException>(() => migration.Up());
	}

	private sealed class NullTableNameMigration : UpMigration
	{
		public override void Up()
		{
			Create.PrimaryKey("primarykey").OnTable(null!).InSchema("schema").WithColumn("column");
		}
	}

	[Fact]
	public void NullSchemaNameThrows()
	{
		var migration = new NullSchemaNameMigration();

		Assert.Throws<ArgumentNullException>(() => migration.Up());
	}

	private sealed class NullSchemaNameMigration : UpMigration
	{
		public override void Up()
		{
			Create.PrimaryKey("primarykey").OnTable("table").InSchema(null!).WithColumn("column");
		}
	}

	[Fact]
	public void NullColumnNameThrows()
	{
		var migration = new NullColumnNameMigration();

		Assert.Throws<ArgumentNullException>(() => migration.Up());
	}

	private sealed class NullColumnNameMigration : UpMigration
	{
		public override void Up()
		{
			Create.PrimaryKey("primarykey").OnTable("table").InSchema("schema").WithColumn(null!);
		}
	}

	[Fact]
	public void EmptyPrimaryKeyNameThrows()
	{
		var migration = new EmptyPrimaryKeyNameMigration();

		Assert.Throws<ArgumentNullException>(() => migration.Up());
	}

	private sealed class EmptyPrimaryKeyNameMigration : UpMigration
	{
		public override void Up()
		{
			Create.PrimaryKey(string.Empty).OnTable("table").InSchema("schema").WithColumn("column");
		}
	}

	[Fact]
	public void EmptyTableNameThrows()
	{
		var migration = new EmptyTableNameMigration();

		Assert.Throws<ArgumentNullException>(() => migration.Up());
	}

	private sealed class EmptyTableNameMigration : UpMigration
	{
		public override void Up()
		{
			Create.PrimaryKey("primarykey").OnTable(string.Empty).InSchema("schema").WithColumn("column");
		}
	}

	[Fact]
	public void EmptySchemaNameThrows()
	{
		var migration = new EmptySchemaNameMigration();

		Assert.Throws<ArgumentNullException>(() => migration.Up());
	}

	private sealed class EmptySchemaNameMigration : UpMigration
	{
		public override void Up()
		{
			Create.PrimaryKey("primarykey").OnTable("table").InSchema(string.Empty).WithColumn("column");
		}
	}

	[Fact]
	public void EmptyColumnNameThrows()
	{
		var migration = new EmptyColumnNameMigration();

		Assert.Throws<ArgumentNullException>(() => migration.Up());
	}

	private sealed class EmptyColumnNameMigration : UpMigration
	{
		public override void Up()
		{
			Create.PrimaryKey("primarykey").OnTable("table").InSchema("schema").WithColumn(string.Empty);
		}
	}

	[Fact]
	public void WhiteSpacePrimaryKeyNameThrows()
	{
		var migration = new WhiteSpacePrimaryKeyNameMigration();

		Assert.Throws<ArgumentNullException>(() => migration.Up());
	}

	private sealed class WhiteSpacePrimaryKeyNameMigration : UpMigration
	{
		public override void Up()
		{
			Create.PrimaryKey("     ").OnTable("table").InSchema("schema").WithColumn("column");
		}
	}

	[Fact]
	public void WhiteSpaceTableNameThrows()
	{
		var migration = new WhiteSpaceTableNameMigration();

		Assert.Throws<ArgumentNullException>(() => migration.Up());
	}

	private sealed class WhiteSpaceTableNameMigration : UpMigration
	{
		public override void Up()
		{
			Create.PrimaryKey("primarykey").OnTable("     ").InSchema("schema").WithColumn("column");
		}
	}

	[Fact]
	public void WhiteSpaceSchemaNameThrows()
	{
		var migration = new WhiteSpaceSchemaNameMigration();

		Assert.Throws<ArgumentNullException>(() => migration.Up());
	}

	private sealed class WhiteSpaceSchemaNameMigration : UpMigration
	{
		public override void Up()
		{
			Create.PrimaryKey("primarykey").OnTable("table").InSchema("     ").WithColumn("column");
		}
	}

	[Fact]
	public void WhiteSpaceColumnNameThrows()
	{
		var migration = new WhiteSpaceColumnNameMigration();

		Assert.Throws<ArgumentNullException>(() => migration.Up());
	}

	private sealed class WhiteSpaceColumnNameMigration : UpMigration
	{
		public override void Up()
		{
			Create.PrimaryKey("primarykey").OnTable("table").InSchema("schema").WithColumn("     ");
		}
	}

	[Fact]
	public void SameColumnMultipleTimesThrows()
	{
		var migration = new RepeatingColumnMigration();

		Assert.Throws<ConstraintColumnExistsException>(() => migration.Up());
	}

	private sealed class RepeatingColumnMigration : UpMigration
	{
		public override void Up()
		{
			Create.PrimaryKey("primarykey").OnTable("table").InSchema("schema").WithColumn("column").WithColumn("column");
		}
	}

	[Fact]
	public void EmptyListReturnedWhenNoColumns()
	{
		var migration = new NoColumnsMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreatePrimaryKeyMigrationStatement;
		Assert.NotNull(statement);
		Assert.Empty(statement.Columns);
	}

	private sealed class NoColumnsMigration : UpMigration
	{
		public override void Up()
		{
			Create.PrimaryKey("primarykey").OnTable("table").InSchema("schema");
		}
	}

	[Fact]
	public void StatementReturnsPrimaryKeyName()
	{
		var migration = new SingleColumnMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreatePrimaryKeyMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("TestPrimaryKey", statement.PrimaryKey);
	}

	[Fact]
	public void StatementReturnsTableName()
	{
		var migration = new SingleColumnMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreatePrimaryKeyMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("TestTable", statement.Table);
	}

	[Fact]
	public void StatementReturnsSchemaName()
	{
		var migration = new SingleColumnMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreatePrimaryKeyMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("TestSchema", statement.Schema);
	}

	[Fact]
	public void StatementReturnsColumnName()
	{
		var migration = new SingleColumnMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreatePrimaryKeyMigrationStatement;
		Assert.NotNull(statement);
		var column = Assert.Single(statement.Columns);
		Assert.Equal("TestColumn", column);
	}

	private sealed class SingleColumnMigration : UpMigration
	{
		public override void Up()
		{
			Create.PrimaryKey("TestPrimaryKey").OnTable("TestTable").InSchema("TestSchema").WithColumn("TestColumn");
		}
	}

	[Fact]
	public void StatementReturnsMultipleColumnsInCorrectOrder()
	{
		var migration = new MultipleColumnMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreatePrimaryKeyMigrationStatement;
		Assert.NotNull(statement);
		Assert.StrictEqual(3, statement.Columns.Count);
		Assert.Equal("Column3", statement.Columns[0]);
		Assert.Equal("Column2", statement.Columns[1]);
		Assert.Equal("Column1", statement.Columns[2]);
	}

	private sealed class MultipleColumnMigration : UpMigration
	{
		public override void Up()
		{
			Create.PrimaryKey("TestPrimaryKey")
				.OnTable("TestTable")
				.InSchema("TestSchema")
				.WithColumn("Column3")
				.WithColumn("Column2")
				.WithColumn("Column1");
		}
	}

	[Fact]
	public void NoSchemaStatementReturnsNullSchema()
	{
		var migration = new MigrationWithoutSchema();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreatePrimaryKeyMigrationStatement;
		Assert.NotNull(statement);
		Assert.Null(statement.Schema);
	}

	private sealed class MigrationWithoutSchema : UpMigration
	{
		public override void Up()
		{
			Create.PrimaryKey("TestPrimaryKey")
				.OnTable("TestTable")
				.WithColumn("Id");
		}
	}
}
