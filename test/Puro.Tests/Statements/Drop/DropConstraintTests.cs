using Puro.Statements.Drop.Constraint;
using Xunit;

namespace Puro.Tests.Statements.Drop;

public class DropConstraintTests
{
	[Fact]
	public void NullConstraintNameThrows()
	{
		var migration = new NullConstraintNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullConstraintNameMigration : UpMigration
	{
		public override void Up()
		{
			Drop.Constraint(null!).FromTable("table").InSchema("schema");
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
			Drop.Constraint("constraint").FromTable(null!).InSchema("schema");
		}
	}

	[Fact]
	public void NullTableNameWithoutSchemaThrows()
	{
		var migration = new NullTableNameWithoutSchemaMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullTableNameWithoutSchemaMigration : UpMigration
	{
		public override void Up()
		{
			Drop.Constraint("constraint").FromTable(null!);
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
			Drop.Constraint("constraint").FromTable("table").InSchema(null!);
		}
	}

	[Fact]
	public void EmptyConstraintNameThrows()
	{
		var migration = new EmptyConstraintNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class EmptyConstraintNameMigration : UpMigration
	{
		public override void Up()
		{
			Drop.Constraint(string.Empty).FromTable("table").InSchema("schema");
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
			Drop.Constraint("constraint").FromTable(string.Empty).InSchema("schema");
		}
	}

	[Fact]
	public void EmptyTableNameWithoutSchemaThrows()
	{
		var migration = new EmptyTableNameWithoutSchemaMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class EmptyTableNameWithoutSchemaMigration : UpMigration
	{
		public override void Up()
		{
			Drop.Constraint("constraint").FromTable(string.Empty);
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
			Drop.Constraint("constraint").FromTable("table").InSchema(string.Empty);
		}
	}

	[Fact]
	public void WhiteSpaceConstraintNameThrows()
	{
		var migration = new WhiteSpaceConstraintNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpaceConstraintNameMigration : UpMigration
	{
		public override void Up()
		{
			Drop.Constraint("     ").FromTable("table").InSchema("schema");
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
			Drop.Constraint("constraint").FromTable("     ").InSchema("schema");
		}
	}

	[Fact]
	public void WhiteSpaceTableNameWithoutSchemaThrows()
	{
		var migration = new WhiteSpaceTableNameWithoutSchemaMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpaceTableNameWithoutSchemaMigration : UpMigration
	{
		public override void Up()
		{
			Drop.Constraint("constraint").FromTable("     ");
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
			Drop.Constraint("constraint").FromTable("table").InSchema("     ");
		}
	}

	[Fact]
	public void StatementWithoutTableNameReturnsNullTableName()
	{
		var migration = new NoTableNameMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IDropConstraintMigrationStatement;
		Assert.NotNull(statement);
		Assert.Null(statement.Table);
	}

	private sealed class NoTableNameMigration : UpMigration
	{
		public override void Up()
		{
			Drop.Constraint("constraint");
		}
	}

	[Fact]
	public void SchemalessStatementReturnsNullSchemaName()
	{
		var migration = new NoSchemaNameMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IDropConstraintMigrationStatement;
		Assert.NotNull(statement);
		Assert.Null(statement.Schema);
	}

	[Fact]
	public void SchemalessStatementReturnsConstraintName()
	{
		var migration = new NoSchemaNameMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IDropConstraintMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("constraint", statement.Constraint);
	}

	[Fact]
	public void SchemalessStatementReturnsTableName()
	{
		var migration = new NoSchemaNameMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IDropConstraintMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("table", statement.Table);
	}

	private sealed class NoSchemaNameMigration : UpMigration
	{
		public override void Up()
		{
			Drop.Constraint("constraint").FromTable("table");
		}
	}

	[Fact]
	public void StatementReturnsConstraintName()
	{
		var migration = new DropConstraintMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IDropConstraintMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("TestConstraintName", statement.Constraint);
	}

	[Fact]
	public void StatementReturnsTableName()
	{
		var migration = new DropConstraintMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IDropConstraintMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("TestTableName", statement.Table);
	}

	[Fact]
	public void StatementReturnsSchemaName()
	{
		var migration = new DropConstraintMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IDropConstraintMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("TestSchemaName", statement.Schema);
	}

	private sealed class DropConstraintMigration : UpMigration
	{
		public override void Up()
		{
			Drop.Constraint("TestConstraintName").FromTable("TestTableName").InSchema("TestSchemaName");
		}
	}

	[Fact]
	public void StatementWithoutSchemaReturnsTableName()
	{
		var migration = new DropConstraintWithoutSchemaMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IDropConstraintMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("TestTableName", statement.Table);
	}

	private sealed class DropConstraintWithoutSchemaMigration : UpMigration
	{
		public override void Up()
		{
			Drop.Constraint("TestConstraintName").FromTable("TestTableName");
		}
	}
}
