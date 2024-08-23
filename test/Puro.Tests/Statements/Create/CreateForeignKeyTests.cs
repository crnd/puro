using Puro.Exceptions;
using Puro.Statements;
using Puro.Statements.Create.ForeignKey;
using Xunit;

namespace Puro.Tests.Statements.Create;

public class CreateForeignKeyTests
{
	[Fact]
	public void NullForeignKeyNameThrows()
	{
		var migration = new NullForeignKeyMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullForeignKeyMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey(null!)
				.FromTable("fromTable").FromSchema("fromSchema").FromColumn("fromColumn")
				.ToTable("toTable").ToSchema("toSchema").ToColumn("toColumn")
				.OnDeleteRestrict();
		}
	}

	[Fact]
	public void NullPrincipalTableNameThrows()
	{
		var migration = new NullPrincipalTableMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullPrincipalTableMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("foreignkey")
				.FromTable(null!).FromSchema("fromSchema").FromColumn("fromColumn")
				.ToTable("toTable").ToSchema("toSchema").ToColumn("toColumn")
				.OnDeleteRestrict();
		}
	}

	[Fact]
	public void NullPrincipalTableSchemaNameThrows()
	{
		var migration = new NullPrincipalTableSchemaMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullPrincipalTableSchemaMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("foreignkey")
				.FromTable("fromTable").FromSchema(null!).FromColumn("fromColumn")
				.ToTable("toTable").ToSchema("toSchema").ToColumn("toColumn")
				.OnDeleteRestrict();
		}
	}

	[Fact]
	public void NullPrincipalTableColumnNameThrows()
	{
		var migration = new NullPrincipalTableColumnMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullPrincipalTableColumnMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("foreignkey")
				.FromTable("fromTable").FromSchema("fromSchema").FromColumn(null!)
				.ToTable("toTable").ToSchema("toSchema").ToColumn("toColumn")
				.OnDeleteRestrict();
		}
	}

	[Fact]
	public void NullTargetTableNameThrows()
	{
		var migration = new NullTargetTableMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullTargetTableMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("foreignkey")
				.FromTable("fromTable").FromSchema("fromSchema").FromColumn("fromColumn")
				.ToTable(null!).ToSchema("toSchema").ToColumn("toColumn")
				.OnDeleteRestrict();
		}
	}

	[Fact]
	public void NullTargetTableSchemaNameThrows()
	{
		var migration = new NullTargetTableSchemaMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullTargetTableSchemaMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("foreignkey")
				.FromTable("fromTable").FromSchema("fromSchema").FromColumn("fromColumn")
				.ToTable("toTable").ToSchema(null!).ToColumn("toColumn")
				.OnDeleteRestrict();
		}
	}

	[Fact]
	public void NullTargetTableColumnNameThrows()
	{
		var migration = new NullTargetTableColumnMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullTargetTableColumnMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("foreignkey")
				.FromTable("fromTable").FromSchema("fromSchema").FromColumn("fromColumn")
				.ToTable("toTable").ToSchema("toSchema").ToColumn(null!)
				.OnDeleteRestrict();
		}
	}

	[Fact]
	public void EmptyForeignKeyNameThrows()
	{
		var migration = new EmptyForeignKeyMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class EmptyForeignKeyMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey(string.Empty)
				.FromTable("fromTable").FromSchema("fromSchema").FromColumn("fromColumn")
				.ToTable("toTable").ToSchema("toSchema").ToColumn("toColumn")
				.OnDeleteRestrict();
		}
	}

	[Fact]
	public void EmptyPrincipalTableNameThrows()
	{
		var migration = new EmptyPrincipalTableMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class EmptyPrincipalTableMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("foreignkey")
				.FromTable(string.Empty).FromSchema("fromSchema").FromColumn("fromColumn")
				.ToTable("toTable").ToSchema("toSchema").ToColumn("toColumn")
				.OnDeleteRestrict();
		}
	}

	[Fact]
	public void EmptyPrincipalTableSchemaNameThrows()
	{
		var migration = new EmptyPrincipalTableSchemaMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class EmptyPrincipalTableSchemaMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("foreignkey")
				.FromTable("fromTable").FromSchema(string.Empty).FromColumn("fromColumn")
				.ToTable("toTable").ToSchema("toSchema").ToColumn("toColumn")
				.OnDeleteRestrict();
		}
	}

	[Fact]
	public void EmptyPrincipalTableColumnNameThrows()
	{
		var migration = new EmptyPrincipalTableColumnMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class EmptyPrincipalTableColumnMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("foreignkey")
				.FromTable("fromTable").FromSchema("fromSchema").FromColumn(string.Empty)
				.ToTable("toTable").ToSchema("toSchema").ToColumn("toColumn")
				.OnDeleteRestrict();
		}
	}

	[Fact]
	public void EmptyTargetTableNameThrows()
	{
		var migration = new EmptyTargetTableMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class EmptyTargetTableMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("foreignkey")
				.FromTable("fromTable").FromSchema("fromSchema").FromColumn("fromColumn")
				.ToTable(string.Empty).ToSchema("toSchema").ToColumn("toColumn")
				.OnDeleteRestrict();
		}
	}

	[Fact]
	public void EmptyTargetTableSchemaNameThrows()
	{
		var migration = new EmptyTargetTableSchemaMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class EmptyTargetTableSchemaMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("foreignkey")
				.FromTable("fromTable").FromSchema("fromSchema").FromColumn("fromColumn")
				.ToTable("toTable").ToSchema(string.Empty).ToColumn("toColumn")
				.OnDeleteRestrict();
		}
	}

	[Fact]
	public void EmptyTargetTableColumnNameThrows()
	{
		var migration = new EmptyTargetTableColumnMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class EmptyTargetTableColumnMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("foreignkey")
				.FromTable("fromTable").FromSchema("fromSchema").FromColumn("fromColumn")
				.ToTable("toTable").ToSchema("toSchema").ToColumn(string.Empty)
				.OnDeleteRestrict();
		}
	}

	[Fact]
	public void WhiteSpaceForeignKeyNameThrows()
	{
		var migration = new WhiteSpaceForeignKeyMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpaceForeignKeyMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("     ")
				.FromTable("fromTable").FromSchema("fromSchema").FromColumn("fromColumn")
				.ToTable("toTable").ToSchema("toSchema").ToColumn("toColumn")
				.OnDeleteRestrict();
		}
	}

	[Fact]
	public void WhiteSpacePrincipalTableNameThrows()
	{
		var migration = new WhiteSpacePrincipalTableMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpacePrincipalTableMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("foreignkey")
				.FromTable("     ").FromSchema("fromSchema").FromColumn("fromColumn")
				.ToTable("toTable").ToSchema("toSchema").ToColumn("toColumn")
				.OnDeleteRestrict();
		}
	}

	[Fact]
	public void WhiteSpacePrincipalTableSchemaNameThrows()
	{
		var migration = new WhiteSpacePrincipalTableSchemaMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpacePrincipalTableSchemaMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("foreignkey")
				.FromTable("fromTable").FromSchema("     ").FromColumn("fromColumn")
				.ToTable("toTable").ToSchema("toSchema").ToColumn("toColumn")
				.OnDeleteRestrict();
		}
	}

	[Fact]
	public void WhiteSpacePrincipalTableColumnNameThrows()
	{
		var migration = new WhiteSpacePrincipalTableColumnMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpacePrincipalTableColumnMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("foreignkey")
				.FromTable("fromTable").FromSchema("fromSchema").FromColumn("     ")
				.ToTable("toTable").ToSchema("toSchema").ToColumn("toColumn")
				.OnDeleteRestrict();
		}
	}

	[Fact]
	public void WhiteSpaceTargetTableNameThrows()
	{
		var migration = new WhiteSpaceTargetTableMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpaceTargetTableMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("foreignkey")
				.FromTable("fromTable").FromSchema("fromSchema").FromColumn("fromColumn")
				.ToTable("     ").ToSchema("toSchema").ToColumn("toColumn")
				.OnDeleteRestrict();
		}
	}

	[Fact]
	public void WhiteSpaceTargetTableSchemaNameThrows()
	{
		var migration = new WhiteSpaceTargetTableSchemaMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpaceTargetTableSchemaMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("foreignkey")
				.FromTable("fromTable").FromSchema("fromSchema").FromColumn("fromColumn")
				.ToTable("toTable").ToSchema("     ").ToColumn("toColumn")
				.OnDeleteRestrict();
		}
	}

	[Fact]
	public void WhiteSpaceTargetTableColumnNameThrows()
	{
		var migration = new WhiteSpaceTargetTableColumnMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpaceTargetTableColumnMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("foreignkey")
				.FromTable("fromTable").FromSchema("fromSchema").FromColumn("fromColumn")
				.ToTable("toTable").ToSchema("toSchema").ToColumn("     ")
				.OnDeleteRestrict();
		}
	}

	[Fact]
	public void DuplicateFromColumnsThrows()
	{
		var migration = new DuplicateFromColumnsMigration();

		Assert.Throws<ConstraintColumnExistsException>(() => migration.Up());
	}

	private sealed class DuplicateFromColumnsMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("foreignkey")
				.FromTable("fromTable").FromSchema("fromSchema").FromColumn("fromColumn").FromColumn("fromColumn")
				.ToTable("toTable").ToSchema("toSchema").ToColumn("toColumn")
				.OnDeleteRestrict();
		}
	}

	[Fact]
	public void DuplicateToColumnsThrows()
	{
		var migration = new DuplicateToColumnsMigration();

		Assert.Throws<ConstraintColumnExistsException>(() => migration.Up());
	}

	private sealed class DuplicateToColumnsMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("foreignkey")
				.FromTable("fromTable").FromSchema("fromSchema").FromColumn("fromColumn")
				.ToTable("toTable").ToSchema("toSchema").ToColumn("toColumn").ToColumn("toColumn")
				.OnDeleteRestrict();
		}
	}

	[Fact]
	public void StatementReturnsForeignKeyName()
	{
		var migration = new SingleColumnMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateForeignKeyMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("FK_Account_Customer", statement.ForeignKey);
	}

	[Fact]
	public void StatementReturnsPrincipalTableName()
	{
		var migration = new SingleColumnMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateForeignKeyMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("Account", statement.ReferencingTable);
	}

	[Fact]
	public void StatementReturnsPrincipalTableSchema()
	{
		var migration = new SingleColumnMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateForeignKeyMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("Banking", statement.ReferencingTableSchema);
	}

	[Fact]
	public void StatementReturnsPrincipalTableColumn()
	{
		var migration = new SingleColumnMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateForeignKeyMigrationStatement;
		Assert.NotNull(statement);
		var column = Assert.Single(statement.ReferencingColumns);
		Assert.Equal("CustomerId", column);
	}

	[Fact]
	public void StatementReturnsTargetTableName()
	{
		var migration = new SingleColumnMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateForeignKeyMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("Customer", statement.ReferencedTable);
	}

	[Fact]
	public void StatementReturnsTargetTableSchema()
	{
		var migration = new SingleColumnMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateForeignKeyMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("People", statement.ReferencedTableSchema);
	}

	[Fact]
	public void StatementReturnsTargetTableColumn()
	{
		var migration = new SingleColumnMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateForeignKeyMigrationStatement;
		Assert.NotNull(statement);
		var column = Assert.Single(statement.ReferencedColumns);
		Assert.Equal("Id", column);
	}

	[Fact]
	public void StatementReturnsCascade()
	{
		var migration = new SingleColumnMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateForeignKeyMigrationStatement;
		Assert.NotNull(statement);
		Assert.StrictEqual(OnDeleteBehavior.Cascade, statement.OnDelete);
	}

	private sealed class SingleColumnMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("FK_Account_Customer")
				.FromTable("Account").FromSchema("Banking").FromColumn("CustomerId")
				.ToTable("Customer").ToSchema("People").ToColumn("Id")
				.OnDeleteCascade();
		}
	}

	[Fact]
	public void StatementReturnsFromColumns()
	{
		var migration = new MultiColumnMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateForeignKeyMigrationStatement;
		Assert.NotNull(statement);
		Assert.StrictEqual(3, statement.ReferencingColumns.Count);
		Assert.Equal("from1", statement.ReferencingColumns[0]);
		Assert.Equal("from2", statement.ReferencingColumns[1]);
		Assert.Equal("from3", statement.ReferencingColumns[2]);
	}

	[Fact]
	public void StatementReturnsToColumns()
	{
		var migration = new MultiColumnMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateForeignKeyMigrationStatement;
		Assert.NotNull(statement);
		Assert.StrictEqual(3, statement.ReferencedColumns.Count);
		Assert.Equal("to1", statement.ReferencedColumns[0]);
		Assert.Equal("to2", statement.ReferencedColumns[1]);
		Assert.Equal("to3", statement.ReferencedColumns[2]);
	}

	[Fact]
	public void StatementReturnsRestrict()
	{
		var migration = new MultiColumnMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateForeignKeyMigrationStatement;
		Assert.NotNull(statement);
		Assert.StrictEqual(OnDeleteBehavior.Restrict, statement.OnDelete);
	}

	private sealed class MultiColumnMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("foreignkey")
				.FromTable("fromTable").FromSchema("fromSchema").FromColumn("from1").FromColumn("from2").FromColumn("from3")
				.ToTable("toTable").ToSchema("toSchema").ToColumn("to1").ToColumn("to2").ToColumn("to3")
				.OnDeleteRestrict();
		}
	}

	[Fact]
	public void StatementReturnsSetNull()
	{
		var migration = new SetNullMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateForeignKeyMigrationStatement;
		Assert.NotNull(statement);
		Assert.StrictEqual(OnDeleteBehavior.SetNull, statement.OnDelete);
	}

	private sealed class SetNullMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("foreignkey")
				.FromTable("fromTable").FromSchema("fromSchema").FromColumn("fromColumn")
				.ToTable("toTable").ToSchema("toSchema").ToColumn("toColumn")
				.OnDeleteSetNull();
		}
	}

	[Fact]
	public void StatementReturnsTableNameWhenReferencingSchemaNotDefined()
	{
		var migration = new ReferencingSchemaNotDefinedMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateForeignKeyMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("fromTable", statement.ReferencingTable);
	}

	[Fact]
	public void StatementReturnsNullWhenReferencingSchemaNotDefined()
	{
		var migration = new ReferencingSchemaNotDefinedMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateForeignKeyMigrationStatement;
		Assert.NotNull(statement);
		Assert.Null(statement.ReferencingTableSchema);
	}

	private sealed class ReferencingSchemaNotDefinedMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("foreignkey")
				.FromTable("fromTable").FromColumn("fromColumn")
				.ToTable("toTable").ToSchema("toSchema").ToColumn("toColumn")
				.OnDeleteSetNull();
		}
	}

	[Fact]
	public void StatementReturnsTableNameWhenReferencedSchemaNotDefined()
	{
		var migration = new ReferencedSchemaNotDefinedMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateForeignKeyMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("toTable", statement.ReferencedTable);
	}

	[Fact]
	public void StatementReturnsNullWhenReferencedSchemaNotDefined()
	{
		var migration = new ReferencedSchemaNotDefinedMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateForeignKeyMigrationStatement;
		Assert.NotNull(statement);
		Assert.Null(statement.ReferencedTableSchema);
	}

	private sealed class ReferencedSchemaNotDefinedMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("foreignkey")
				.FromTable("fromTable").FromSchema("fromSchema").FromColumn("fromColumn")
				.ToTable("toTable").ToColumn("toColumn")
				.OnDeleteSetNull();
		}
	}

	[Fact]
	public void SchemalessStatementReturnsForeignKeyName()
	{
		var migration = new NoSchemasMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateForeignKeyMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("foreignkey", statement.ForeignKey);
	}

	[Fact]
	public void SchemalessStatementReturnsReferencingTableName()
	{
		var migration = new NoSchemasMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateForeignKeyMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("fromTable", statement.ReferencingTable);
	}

	[Fact]
	public void SchemalessStatementReturnsNullReferencingTableSchema()
	{
		var migration = new NoSchemasMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateForeignKeyMigrationStatement;
		Assert.NotNull(statement);
		Assert.Null(statement.ReferencingTableSchema);
	}

	[Fact]
	public void SchemalessStatementReturnsReferencingColumnName()
	{
		var migration = new NoSchemasMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateForeignKeyMigrationStatement;
		Assert.NotNull(statement);
		var columnName = Assert.Single(statement.ReferencingColumns);
		Assert.Equal("fromColumn", columnName);
	}

	[Fact]
	public void SchemalessStatementReturnsReferencedTableName()
	{
		var migration = new NoSchemasMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateForeignKeyMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("toTable", statement.ReferencedTable);
	}

	[Fact]
	public void SchemalessStatementReturnsNullReferencedTableSchema()
	{
		var migration = new NoSchemasMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateForeignKeyMigrationStatement;
		Assert.NotNull(statement);
		Assert.Null(statement.ReferencedTableSchema);
	}

	[Fact]
	public void SchemalessStatementReturnsReferencedColumnName()
	{
		var migration = new NoSchemasMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateForeignKeyMigrationStatement;
		Assert.NotNull(statement);
		var columnName = Assert.Single(statement.ReferencedColumns);
		Assert.Equal("toColumn", columnName);
	}

	[Fact]
	public void SchemalessStatementReturnsOnDeleteBehavior()
	{
		var migration = new NoSchemasMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateForeignKeyMigrationStatement;
		Assert.NotNull(statement);
		Assert.StrictEqual(OnDeleteBehavior.SetNull, statement.OnDelete);
	}

	private sealed class NoSchemasMigration : UpMigration
	{
		public override void Up()
		{
			Create.ForeignKey("foreignkey")
				.FromTable("fromTable").FromColumn("fromColumn")
				.ToTable("toTable").ToColumn("toColumn")
				.OnDeleteSetNull();
		}
	}
}
