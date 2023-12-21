using Puro.Statements.Drop.Table;
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

	private sealed class NullTableNameMigration : UpMigration
	{
		public override void Up()
		{
			Drop.Table(null!);
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
			Drop.Table(string.Empty);
		}
	}

	[Fact]
	public void WhiteSpaceTableNameThrows()
	{
		var migration = new WhiteSpaceNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpaceNameMigration : UpMigration
	{
		public override void Up()
		{
			Drop.Table("        ");
		}
	}

	[Fact]
	public void StatementReturnsTableName()
	{
		var migration = new TableNameMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IDropTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal(nameof(TableNameMigration), statement.Table);
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

	private sealed class TableNameMigration : UpMigration
	{
		public override void Up()
		{
			Drop.Table(nameof(TableNameMigration));
		}
	}

	[Fact]
	public void SchemaNotNullWhenSchemaDefined()
	{
		var migration = new SchemaMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IDropTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.NotNull(statement.Schema);
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

	private sealed class SchemaMigration : UpMigration
	{
		public override void Up()
		{
			Drop.Table(nameof(SchemaMigration)).InSchema("TestSchema");
		}
	}
}
