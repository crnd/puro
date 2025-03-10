﻿using Puro.Exceptions;
using Puro.Statements.Create.Table;
using Xunit;

namespace Puro.Tests.Statements.Create;

public class CreateTableTests
{
	[Fact]
	public void NullTableNameWithoutSchemaThrows()
	{
		var migration = new NullTableNameWithoutSchemaMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullTableNameWithoutSchemaMigration : Migration
	{
		public override void Up()
		{
			Create.Table(null!)
				.WithColumn("Id").AsInt().Identity();
		}
	}

	[Fact]
	public void NullTableNameWithSchemaThrows()
	{
		var migration = new NullTableNameWithSchemaMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullTableNameWithSchemaMigration : Migration
	{
		public override void Up()
		{
			Create.Table(null!).InSchema("schema")
				.WithColumn("Id").AsInt().Identity();
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
			Create.Table("table").InSchema(null!)
				.WithColumn("Id").AsInt().Identity();
		}
	}

	[Fact]
	public void NullColumnNameThrows()
	{
		var migration = new NullColumnNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullColumnNameMigration : Migration
	{
		public override void Up()
		{
			Create.Table("table").InSchema("schema")
				.WithColumn(null!).AsInt().Identity();
		}
	}

	[Fact]
	public void EmptyTableNameWithoutSchemaThrows()
	{
		var migration = new EmptyTableNameWithoutSchemaMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class EmptyTableNameWithoutSchemaMigration : Migration
	{
		public override void Up()
		{
			Create.Table(string.Empty)
				.WithColumn("Id").AsInt().Identity();
		}
	}

	[Fact]
	public void EmptyTableNameWithSchemaThrows()
	{
		var migration = new EmptyTableNameWithSchemaMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class EmptyTableNameWithSchemaMigration : Migration
	{
		public override void Up()
		{
			Create.Table(string.Empty).InSchema("schema")
				.WithColumn("Id").AsInt().Identity();
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
			Create.Table("table").InSchema(string.Empty)
				.WithColumn("Id").AsInt().Identity();
		}
	}

	[Fact]
	public void EmptyColumnNameThrows()
	{
		var migration = new EmptyColumnNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class EmptyColumnNameMigration : Migration
	{
		public override void Up()
		{
			Create.Table("table").InSchema("schema")
				.WithColumn(string.Empty).AsInt().Identity();
		}
	}

	[Fact]
	public void WhiteSpaceTableNameWithoutSchemaThrows()
	{
		var migration = new WhiteSpaceTableNameWithoutSchemaMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpaceTableNameWithoutSchemaMigration : Migration
	{
		public override void Up()
		{
			Create.Table("     ")
				.WithColumn("Id").AsInt().Identity();
		}
	}

	[Fact]
	public void WhiteSpaceTableNameWithSchemaThrows()
	{
		var migration = new WhiteSpaceTableNameWithSchemaMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpaceTableNameWithSchemaMigration : Migration
	{
		public override void Up()
		{
			Create.Table("     ").InSchema("schema")
				.WithColumn("Id").AsInt().Identity();
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
			Create.Table("table").InSchema("     ")
				.WithColumn("Id").AsInt().Identity();
		}
	}

	[Fact]
	public void WhiteSpaceColumnNameThrows()
	{
		var migration = new WhiteSpaceColumnNameMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpaceColumnNameMigration : Migration
	{
		public override void Up()
		{
			Create.Table("table").InSchema("schema")
				.WithColumn("     ").AsInt().Identity();
		}
	}

	[Fact]
	public void DuplicateColumnNameThrows()
	{
		var migration = new DuplicateColumnNameMigration();

		Assert.Throws<TableColumnExistsException>(migration.Up);
	}

	private sealed class DuplicateColumnNameMigration : Migration
	{
		public override void Up()
		{
			Create.Table("table").InSchema("schema")
				.WithColumn("Id").AsInt().Identity()
				.WithColumn("Id").AsLong().NotNull();
		}
	}

	[Fact]
	public void NonPositiveFixedStringLengthThrows()
	{
		var migration = new NonPositiveFixedStringLengthMigration();

		Assert.Throws<InvalidStringLengthException>(migration.Up);
	}

	private sealed class NonPositiveFixedStringLengthMigration : Migration
	{
		public override void Up()
		{
			Create.Table("table").InSchema("schema")
				.WithColumn("Name").AsString().FixedLength(0);
		}
	}

	[Fact]
	public void NonPositiveMaximumStringLengthThrows()
	{
		var migration = new NonPositiveMaximumStringLengthMigration();

		Assert.Throws<InvalidStringLengthException>(migration.Up);
	}

	private sealed class NonPositiveMaximumStringLengthMigration : Migration
	{
		public override void Up()
		{
			Create.Table("table").InSchema("schema")
				.WithColumn("Name").AsString().MaximumLength(0);
		}
	}

	[Fact]
	public void StatementReturnsEmptyListWhenNoColumns()
	{
		var migration = new NoColumnsMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.Empty(statement.Columns);
	}

	private sealed class NoColumnsMigration : Migration
	{
		public override void Up()
		{
			Create.Table("TestTable").InSchema("TestSchema");
		}
	}

	[Fact]
	public void StatementReturnsTableName()
	{
		var migration = new BoolMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("TestTable", statement.Table);
	}

	[Fact]
	public void StatementReturnsSchemaName()
	{
		var migration = new BoolMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("TestSchema", statement.Schema);
	}

	[Fact]
	public void StatementReturnsColumnName()
	{
		var migration = new BoolMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		var column = Assert.Single(statement.Columns);
		Assert.Equal("Exists", column.Name);
	}

	[Fact]
	public void StatementReturnsBoolType()
	{
		var migration = new BoolMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		var column = Assert.Single(statement.Columns);
		Assert.StrictEqual(typeof(bool), column.Type);
	}

	private sealed class BoolMigration : Migration
	{
		public override void Up()
		{
			Create.Table("TestTable").InSchema("TestSchema")
				.WithColumn("Exists").AsBool().Null();
		}
	}

	[Fact]
	public void StatementReturnsShortType()
	{
		var migration = new ShortMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		var column = Assert.Single(statement.Columns);
		Assert.StrictEqual(typeof(short), column.Type);
	}

	private sealed class ShortMigration : Migration
	{
		public override void Up()
		{
			Create.Table("TestTable")
				.WithColumn("Id").AsShort().Identity();
		}
	}

	[Fact]
	public void StatementReturnsIntType()
	{
		var migration = new IntMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		var column = Assert.Single(statement.Columns);
		Assert.StrictEqual(typeof(int), column.Type);
	}

	private sealed class IntMigration : Migration
	{
		public override void Up()
		{
			Create.Table("TestTable")
				.WithColumn("Id").AsInt().Identity();
		}
	}

	[Fact]
	public void StatementReturnsLongType()
	{
		var migration = new LongMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		var column = Assert.Single(statement.Columns);
		Assert.StrictEqual(typeof(long), column.Type);
	}

	private sealed class LongMigration : Migration
	{
		public override void Up()
		{
			Create.Table("TestTable")
				.WithColumn("Id").AsLong().Identity();
		}
	}

	[Fact]
	public void StatementReturnsDoubleType()
	{
		var migration = new DoubleMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		var column = Assert.Single(statement.Columns);
		Assert.StrictEqual(typeof(double), column.Type);
	}

	private sealed class DoubleMigration : Migration
	{
		public override void Up()
		{
			Create.Table("TestTable")
				.WithColumn("Id").AsDouble().NotNull();
		}
	}

	[Fact]
	public void StatementReturnsDecimalType()
	{
		var migration = new DecimalMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		var column = Assert.Single(statement.Columns);
		Assert.StrictEqual(typeof(decimal), column.Type);
	}

	[Fact]
	public void StatementReturnsDecimalPrecision()
	{
		var migration = new DecimalMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		var column = Assert.Single(statement.Columns);
		Assert.StrictEqual(5, column.Precision);
	}

	[Fact]
	public void StatementReturnsDecimalScale()
	{
		var migration = new DecimalMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		var column = Assert.Single(statement.Columns);
		Assert.StrictEqual(2, column.Scale);
	}

	private sealed class DecimalMigration : Migration
	{
		public override void Up()
		{
			Create.Table("TestTable")
				.WithColumn("Id").AsDecimal().WithPrecision(5).WithScale(2).NotNull();
		}
	}

	[Fact]
	public void StatementReturnsGuidType()
	{
		var migration = new GuidMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		var column = Assert.Single(statement.Columns);
		Assert.StrictEqual(typeof(Guid), column.Type);
	}

	private sealed class GuidMigration : Migration
	{
		public override void Up()
		{
			Create.Table("TestTable").InSchema("TestSchema")
				.WithColumn("Id").AsGuid().NotNull();
		}
	}

	[Fact]
	public void StatementReturnsStringType()
	{
		var migration = new StringMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		var column = Assert.Single(statement.Columns);
		Assert.StrictEqual(typeof(string), column.Type);
	}

	[Fact]
	public void StatementReturnsNullFixedStringLengthWhenNotDefined()
	{
		var migration = new StringMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		var column = Assert.Single(statement.Columns);
		Assert.Null(column.FixedLength);
	}

	[Fact]
	public void StatementReturnsNullMaximumStringLengthWhenNotDefined()
	{
		var migration = new StringMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		var column = Assert.Single(statement.Columns);
		Assert.Null(column.MaximumLength);
	}

	private sealed class StringMigration : Migration
	{
		public override void Up()
		{
			Create.Table("TestTable").InSchema("TestSchema")
				.WithColumn("Name").AsString().NotNull();
		}
	}

	[Fact]
	public void StatementReturnsFixedStringLength()
	{
		var migration = new FixedStringLengthMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		var column = Assert.Single(statement.Columns);
		Assert.StrictEqual(350, column.FixedLength);
	}

	private sealed class FixedStringLengthMigration : Migration
	{
		public override void Up()
		{
			Create.Table("TestTable").InSchema("TestSchema")
				.WithColumn("Name").AsString().FixedLength(350).NotNull();
		}
	}

	[Fact]
	public void StatementReturnsMaximumStringLength()
	{
		var migration = new MaximumStringLengthMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		var column = Assert.Single(statement.Columns);
		Assert.StrictEqual(760, column.MaximumLength);
	}

	private sealed class MaximumStringLengthMigration : Migration
	{
		public override void Up()
		{
			Create.Table("TestTable").InSchema("TestSchema")
				.WithColumn("Name").AsString().MaximumLength(760).NotNull();
		}
	}

	[Fact]
	public void StatementReturnsDateType()
	{
		var migration = new DateMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		var column = Assert.Single(statement.Columns);
		Assert.StrictEqual(typeof(DateOnly), column.Type);
	}

	private sealed class DateMigration : Migration
	{
		public override void Up()
		{
			Create.Table("TestTable").InSchema("TestSchema")
				.WithColumn("TimeStamp").AsDate().NotNull();
		}
	}

	[Fact]
	public void StatementReturnsTimeType()
	{
		var migration = new TimeMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		var column = Assert.Single(statement.Columns);
		Assert.StrictEqual(typeof(TimeOnly), column.Type);
	}

	private sealed class TimeMigration : Migration
	{
		public override void Up()
		{
			Create.Table("TestTable").InSchema("TestSchema")
				.WithColumn("TimeStamp").AsTime().NotNull();
		}
	}

	[Fact]
	public void StatementReturnsDateTimeType()
	{
		var migration = new DateTimeMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		var column = Assert.Single(statement.Columns);
		Assert.StrictEqual(typeof(DateTime), column.Type);
	}

	private sealed class DateTimeMigration : Migration
	{
		public override void Up()
		{
			Create.Table("TestTable").InSchema("TestSchema")
				.WithColumn("TimeStamp").AsDateTime().NotNull();
		}
	}

	[Fact]
	public void StatementReturnsDateTimeOffsetType()
	{
		var migration = new DateTimeOffsetMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		var column = Assert.Single(statement.Columns);
		Assert.StrictEqual(typeof(DateTimeOffset), column.Type);
	}

	private sealed class DateTimeOffsetMigration : Migration
	{
		public override void Up()
		{
			Create.Table("TestTable").InSchema("TestSchema")
				.WithColumn("TimeStamp").AsDateTimeOffset().NotNull();
		}
	}

	[Fact]
	public void StatementReturnsNullable()
	{
		var migration = new NullableMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		var column = Assert.Single(statement.Columns);
		Assert.True(column.Nullable);
	}

	private sealed class NullableMigration : Migration
	{
		public override void Up()
		{
			Create.Table("TestTable").InSchema("TestSchema")
				.WithColumn("Name").AsString().Null();
		}
	}

	[Fact]
	public void StatementReturnsNotNullable()
	{
		var migration = new NotNullableMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		var column = Assert.Single(statement.Columns);
		Assert.False(column.Nullable);
	}

	private sealed class NotNullableMigration : Migration
	{
		public override void Up()
		{
			Create.Table("TestTable").InSchema("TestSchema")
				.WithColumn("Name").AsString().NotNull();
		}
	}

	[Fact]
	public void StatementReturnsMultipleColumns()
	{
		var migration = new MultipleColumnsMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.StrictEqual(4, statement.Columns.Count);

		var column = statement.Columns[0];
		Assert.Equal("Id", column.Name);
		Assert.StrictEqual(typeof(int), column.Type);
		Assert.False(column.Nullable);
		Assert.True(column.Identity);

		column = statement.Columns[1];
		Assert.Equal("Name", column.Name);
		Assert.StrictEqual(typeof(string), column.Type);
		Assert.False(column.Nullable);
		Assert.False(column.Identity);
		Assert.Null(column.FixedLength);
		Assert.StrictEqual(200, column.MaximumLength);

		column = statement.Columns[2];
		Assert.Equal("Description", column.Name);
		Assert.StrictEqual(typeof(string), column.Type);
		Assert.True(column.Nullable);
		Assert.False(column.Identity);
		Assert.Null(column.FixedLength);
		Assert.Null(column.MaximumLength);

		column = statement.Columns[3];
		Assert.Equal("LastUpdated", column.Name);
		Assert.StrictEqual(typeof(DateTimeOffset), column.Type);
		Assert.True(column.Nullable);
		Assert.False(column.Identity);
	}

	private sealed class MultipleColumnsMigration : Migration
	{
		public override void Up()
		{
			Create.Table("TestTable").InSchema("TestSchema")
				.WithColumn("Id").AsInt().Identity()
				.WithColumn("Name").AsString().MaximumLength(200).NotNull()
				.WithColumn("Description").AsString().Null()
				.WithColumn("LastUpdated").AsDateTimeOffset().Null();
		}
	}

	[Fact]
	public void SchemalessStatementReturnsTableName()
	{
		var migration = new SchemalessMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.Equal("Account", statement.Table);
	}

	[Fact]
	public void SchemalessStatementReturnsNullSchema()
	{
		var migration = new SchemalessMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.Null(statement.Schema);
	}

	[Fact]
	public void SchemalessStatementReturnsColumns()
	{
		var migration = new SchemalessMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as ICreateTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.StrictEqual(3, statement.Columns.Count);
	}

	private sealed class SchemalessMigration : Migration
	{
		public override void Up()
		{
			Create.Table("Account")
				.WithColumn("Id").AsInt().Identity()
				.WithColumn("Name").AsString().MaximumLength(100).NotNull()
				.WithColumn("Number").AsString().MaximumLength(50).NotNull();
		}
	}
}
