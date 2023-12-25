using Puro.Statements.Drop.Index;
using Xunit;

namespace Puro.Tests.Statements.Drop;

public class DropIndexTests
{
	[Fact]
	public void NullIndexNameThrows()
	{
		var migration = new NullIndexNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullIndexNameMigration : UpMigration
	{
		public override void Up()
		{
			Drop.Index(null!).FromTable("table").InSchema("schema");
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
			Drop.Index("index").FromTable(null!).InSchema("schema");
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
			Drop.Index("index").FromTable("table").InSchema(null!);
		}
	}

	[Fact]
	public void EmptyIndexNameThrows()
	{
		var migration = new EmptyIndexNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class EmptyIndexNameMigration : UpMigration
	{
		public override void Up()
		{
			Drop.Index(string.Empty).FromTable("table").InSchema("schema");
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
			Drop.Index("index").FromTable(string.Empty).InSchema("schema");
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
			Drop.Index("index").FromTable("table").InSchema(string.Empty);
		}
	}

	[Fact]
	public void WhiteSpaceIndexNameThrows()
	{
		var migration = new WhiteSpaceIndexNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpaceIndexNameMigration : UpMigration
	{
		public override void Up()
		{
			Drop.Index("     ").FromTable("table").InSchema("schema");
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
			Drop.Index("index").FromTable("     ").InSchema("schema");
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
			Drop.Index("index").FromTable("table").InSchema("     ");
		}
	}

	[Fact]
	public void StatementWithoutTableNameReturnsNull()
	{
		var migration = new NoTableNameMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IDropIndexMigrationStatement;
		Assert.NotNull(statement);
		Assert.Null(statement.Table);
	}

	private sealed class NoTableNameMigration : UpMigration
	{
		public override void Up()
		{
			Drop.Index("index");
		}
	}

	[Fact]
	public void StatementWithoutSchemaNameReturnsNull()
	{
		var migration = new NoSchemaNameMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IDropIndexMigrationStatement;
		Assert.NotNull(statement);
		Assert.Null(statement.Schema);
	}

	private sealed class NoSchemaNameMigration : UpMigration
	{
		public override void Up()
		{
			Drop.Index("index").FromTable("table");
		}
	}

	[Fact]
	public void StatementReturnsIndexName()
	{
		var migration = new DropIndexMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IDropIndexMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("TestIndexName", statement.Index);
	}

	[Fact]
	public void StatementReturnsTableName()
	{
		var migration = new DropIndexMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IDropIndexMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("TestTableName", statement.Table);
	}

	[Fact]
	public void StatementReturnsSchemaName()
	{
		var migration = new DropIndexMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IDropIndexMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("TestSchemaName", statement.Schema);
	}

	private sealed class DropIndexMigration : UpMigration
	{
		public override void Up()
		{
			Drop.Index("TestIndexName").FromTable("TestTableName").InSchema("TestSchemaName");
		}
	}
}
