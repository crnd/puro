using Puro.Statements.Rename.Table;
using Xunit;

namespace Puro.Tests.Statements.Rename;

public class RenameTableTests
{
	[Fact]
	public void NullCurrentTableNameThrows()
	{
		var migration = new NullCurrentTableNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullCurrentTableNameMigration : Migration
	{
		public override void Up()
		{
			Rename.Table(null!).InSchema("schema").To("new");
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
			Rename.Table("table").InSchema(null!).To("new");
		}
	}

	[Fact]
	public void NullNewNameThrows()
	{
		var migration = new NullNewNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullNewNameMigration : Migration
	{
		public override void Up()
		{
			Rename.Table("table").InSchema("schema").To(null!);
		}
	}

	[Fact]
	public void EmptyCurrentTableNameThrows()
	{
		var migration = new EmptyCurrentTableNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class EmptyCurrentTableNameMigration : Migration
	{
		public override void Up()
		{
			Rename.Table(string.Empty).InSchema("schema").To("new");
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
			Rename.Table("table").InSchema(string.Empty).To("new");
		}
	}

	[Fact]
	public void EmptyNewNameThrows()
	{
		var migration = new EmptyNewNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class EmptyNewNameMigration : Migration
	{
		public override void Up()
		{
			Rename.Table("table").InSchema("schema").To(string.Empty);
		}
	}

	[Fact]
	public void WhiteSpaceCurrentTableNameThrows()
	{
		var migration = new WhiteSpaceCurrentTableNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpaceCurrentTableNameMigration : Migration
	{
		public override void Up()
		{
			Rename.Table("        ").InSchema("schema").To("new");
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
			Rename.Table("table").InSchema("     ").To("new");
		}
	}

	[Fact]
	public void WhiteSpaceNewNameThrows()
	{
		var migration = new WhiteSpaceNewNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpaceNewNameMigration : Migration
	{
		public override void Up()
		{
			Rename.Table("table").InSchema("schema").To("     ");
		}
	}

	[Fact]
	public void StatementReturnsCurrentTableName()
	{
		var migration = new RenameTableMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IRenameTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("Car", statement.CurrentName);
	}

	[Fact]
	public void StatementReturnsSchemaName()
	{
		var migration = new RenameTableMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IRenameTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("Transportation", statement.Schema);
	}

	[Fact]
	public void StatementReturnsNewTableName()
	{
		var migration = new RenameTableMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IRenameTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("Vehicle", statement.NewName);
	}

	private sealed class RenameTableMigration : Migration
	{
		public override void Up()
		{
			Rename.Table("Car").InSchema("Transportation").To("Vehicle");
		}
	}

	[Fact]
	public void SchemalessStatementReturnsCurrentTableName()
	{
		var migration = new SchemalessRenameTableMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IRenameTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("Car", statement.CurrentName);
	}

	[Fact]
	public void SchemalessStatementReturnsNewTableName()
	{
		var migration = new SchemalessRenameTableMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IRenameTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("Vehicle", statement.NewName);
	}

	[Fact]
	public void SchemalessStatementReturnsNullSchemaName()
	{
		var migration = new SchemalessRenameTableMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IRenameTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.Null(statement.Schema);
	}

	private sealed class SchemalessRenameTableMigration : Migration
	{
		public override void Up()
		{
			Rename.Table("Car").To("Vehicle");
		}
	}
}
