using Puro.Exceptions;
using Puro.Statements.Create.Index;
using Xunit;

namespace Puro.Tests.Statements.Create;

public class CreateIndexTests
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
			Create.Index(null!)
				.OnTable("table").InSchema("schema")
				.OnColumn("column").Ascending();
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
			Create.Index("index")
				.OnTable(null!).InSchema("schema")
				.OnColumn("column").Ascending();
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
			Create.Index("index")
				.OnTable("table").InSchema(null!)
				.OnColumn("column").Ascending();
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
			Create.Index("index")
				.OnTable("table").InSchema("schema")
				.OnColumn(null!).Ascending();
		}
	}

	[Fact]
	public void NullFilterThrows()
	{
		var migration = new NullFilterMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullFilterMigration : UpMigration
	{
		public override void Up()
		{
			Create.Index("index")
				.OnTable("table").InSchema("schema")
				.OnColumn("column").Ascending()
				.WithFilter(null!);
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
			Create.Index(string.Empty)
				.OnTable("table").InSchema("schema")
				.OnColumn("column").Ascending();
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
			Create.Index("index")
				.OnTable(string.Empty).InSchema("schema")
				.OnColumn("column").Ascending();
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
			Create.Index("index")
				.OnTable("table").InSchema(string.Empty)
				.OnColumn("column").Ascending();
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
			Create.Index("index")
				.OnTable("table").InSchema("schema")
				.OnColumn(string.Empty).Ascending();
		}
	}

	[Fact]
	public void EmptyFilterThrows()
	{
		var migration = new EmptyFilterMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class EmptyFilterMigration : UpMigration
	{
		public override void Up()
		{
			Create.Index("index")
				.OnTable("table").InSchema("schema")
				.OnColumn("column").Ascending()
				.WithFilter(string.Empty);
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
			Create.Index("     ")
				.OnTable("table").InSchema("schema")
				.OnColumn("column").Ascending();
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
			Create.Index("index")
				.OnTable("     ").InSchema("schema")
				.OnColumn("column").Ascending();
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
			Create.Index("index")
				.OnTable("table").InSchema("     ")
				.OnColumn("column").Ascending();
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
			Create.Index("index")
				.OnTable("table").InSchema("schema")
				.OnColumn("     ").Ascending();
		}
	}

	[Fact]
	public void WhiteSpaceFilterThrows()
	{
		var migration = new WhiteSpaceFilterMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpaceFilterMigration : UpMigration
	{
		public override void Up()
		{
			Create.Index("index")
				.OnTable("table").InSchema("schema")
				.OnColumn("column").Ascending()
				.WithFilter("     ");
		}
	}

	[Fact]
	public void DuplicateColumnNameThrows()
	{
		var migration = new DuplicateColumnNameMigration();

		Assert.Throws<IndexColumnExistsException>(migration.Up);
	}

	private sealed class DuplicateColumnNameMigration : UpMigration
	{
		public override void Up()
		{
			Create.Index("index")
				.OnTable("table").InSchema("schema")
				.OnColumn("column").Ascending()
				.OnColumn("column").Descending();
		}
	}

	[Fact]
	public void StatementReturnsIndexName()
	{
		var migration = new SingleColumnMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateIndexMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("IX_Customer_AddressId", statement.Index);
	}

	[Fact]
	public void StatementReturnsTableName()
	{
		var migration = new SingleColumnMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateIndexMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("Customer", statement.Table);
	}

	[Fact]
	public void StatementReturnsSchemaName()
	{
		var migration = new SingleColumnMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateIndexMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("dbo", statement.Schema);
	}

	[Fact]
	public void StatementReturnsColumnName()
	{
		var migration = new SingleColumnMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateIndexMigrationStatement;
		Assert.NotNull(statement);
		var column = Assert.Single(statement.Columns);
		Assert.Equal("AddressId", column.Name);
	}

	[Fact]
	public void StatementReturnsColumnDirection()
	{
		var migration = new SingleColumnMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateIndexMigrationStatement;
		Assert.NotNull(statement);
		var column = Assert.Single(statement.Columns);
		Assert.False(column.Descending);
	}

	[Fact]
	public void StatementReturnsFilter()
	{
		var migration = new SingleColumnMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateIndexMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("[Enabled] = 1", statement.Filter);
	}

	[Fact]
	public void StatementReturnsNonUniqueIndex()
	{
		var migration = new SingleColumnMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateIndexMigrationStatement;
		Assert.NotNull(statement);
		Assert.False(statement.Unique);
	}

	private sealed class SingleColumnMigration : UpMigration
	{
		public override void Up()
		{
			Create.Index("IX_Customer_AddressId")
				.OnTable("Customer").InSchema("dbo")
				.OnColumn("AddressId").Ascending()
				.WithFilter("[Enabled] = 1");
		}
	}

	[Fact]
	public void NoSchemaStatementReturnsNullSchema()
	{
		var migration = new NoSchemaMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateIndexMigrationStatement;
		Assert.NotNull(statement);
		Assert.Null(statement.Schema);
	}

	[Fact]
	public void NoSchemaStatementReturnsTableName()
	{
		var migration = new NoSchemaMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateIndexMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("Customer", statement.Table);
	}

	private sealed class NoSchemaMigration : UpMigration
	{
		public override void Up()
		{
			Create.Index("IX_Customer_AddressId")
				.OnTable("Customer")
				.OnColumn("AddressId").Ascending();
		}
	}

	[Fact]
	public void StatementReturnsColumnsInCreationOrder()
	{
		var migration = new MultipleColumnMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateIndexMigrationStatement;
		Assert.NotNull(statement);
		Assert.StrictEqual(3, statement.Columns.Count);

		var column = statement.Columns[0];
		Assert.Equal("AddressId", column.Name);
		Assert.False(column.Descending);

		column = statement.Columns[1];
		Assert.Equal("AccountId", column.Name);
		Assert.True(column.Descending);

		column = statement.Columns[2];
		Assert.Equal("CompanyId", column.Name);
		Assert.False(column.Descending);
	}

	[Fact]
	public void StatementReturnsUniqueIndex()
	{
		var migration = new MultipleColumnMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateIndexMigrationStatement;
		Assert.NotNull(statement);
		Assert.True(statement.Unique);
	}

	private sealed class MultipleColumnMigration : UpMigration
	{
		public override void Up()
		{
			Create.UniqueIndex("UIX_Customer_AddressId_AccountId_CompanyId")
				.OnTable("Customer").InSchema("dbo")
				.OnColumn("AddressId").Ascending()
				.OnColumn("AccountId").Descending()
				.OnColumn("CompanyId").Ascending();
		}
	}
}
