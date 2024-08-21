using Puro.Statements.Rename.Column;
using Xunit;

namespace Puro.Tests.Statements.Rename;

public class RenameColumnTests
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
			Rename.Column(null!).InTable("table").InSchema("schema").To("newName");
		}
	}

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
			Rename.Column("column").InTable(null!).InSchema("schema").To("newName");
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
			Rename.Column("column").InTable("table").InSchema(null!).To("newName");
		}
	}

	[Fact]
	public void NullNewColumnNameThrows()
	{
		var migration = new NullNewColumnNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullNewColumnNameMigration : UpMigration
	{
		public override void Up()
		{
			Rename.Column("column").InTable("table").InSchema("schema").To(null!);
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
			Rename.Column(string.Empty).InTable("table").InSchema("schema").To("newName");
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
			Rename.Column("column").InTable(string.Empty).InSchema("schema").To("newName");
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
			Rename.Column("column").InTable("table").InSchema(string.Empty).To("newName");
		}
	}

	[Fact]
	public void EmptyNewColumnNameThrows()
	{
		var migration = new EmptyNewColumnNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class EmptyNewColumnNameMigration : UpMigration
	{
		public override void Up()
		{
			Rename.Column("column").InTable("table").InSchema("schema").To(string.Empty);
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
			Rename.Column("     ").InTable("table").InSchema("schema").To("newName");
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
			Rename.Column("column").InTable("     ").InSchema("schema").To("newName");
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
			Rename.Column("column").InTable("table").InSchema("     ").To("newName");
		}
	}

	[Fact]
	public void WhiteSpaceNewColumnNameThrows()
	{
		var migration = new WhiteSpaceNewColumnNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpaceNewColumnNameMigration : UpMigration
	{
		public override void Up()
		{
			Rename.Column("column").InTable("table").InSchema("schema").To("     ");
		}
	}

	[Fact]
	public void StatementReturnsCurrentColumnName()
	{
		var migration = new MigrationWithSchema();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IRenameColumnMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("Name", statement.CurrentName);
	}

	[Fact]
	public void StatementReturnsTableName()
	{
		var migration = new MigrationWithSchema();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IRenameColumnMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("Vehicle", statement.Table);
	}

	[Fact]
	public void StatementReturnsSchemaName()
	{
		var migration = new MigrationWithSchema();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IRenameColumnMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("Transportation", statement.Schema);
	}

	[Fact]
	public void StatementReturnsNewColumnName()
	{
		var migration = new MigrationWithSchema();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IRenameColumnMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("Model", statement.NewName);
	}

	private sealed class MigrationWithSchema : UpMigration
	{
		public override void Up()
		{
			Rename.Column("Name").InTable("Vehicle").InSchema("Transportation").To("Model");
		}
	}

	[Fact]
	public void StatementWithoutSchemaReturnsCurrentColumnName()
	{
		var migration = new MigrationWithoutSchema();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IRenameColumnMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("Name", statement.CurrentName);
	}

	[Fact]
	public void StatementWithoutSchemaReturnsNewColumnName()
	{
		var migration = new MigrationWithoutSchema();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IRenameColumnMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("Model", statement.NewName);
	}

	[Fact]
	public void StatementWithoutSchemaReturnsTableName()
	{
		var migration = new MigrationWithoutSchema();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IRenameColumnMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("Vehicle", statement.Table);
	}

	[Fact]
	public void StatementWithoutSchemaReturnsNullSchema()
	{
		var migration = new MigrationWithoutSchema();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IRenameColumnMigrationStatement;
		Assert.NotNull(statement);
		Assert.Null(statement.Schema);
	}

	private sealed class MigrationWithoutSchema : UpMigration
	{
		public override void Up()
		{
			Rename.Column("Name").InTable("Vehicle").To("Model");
		}
	}
}
