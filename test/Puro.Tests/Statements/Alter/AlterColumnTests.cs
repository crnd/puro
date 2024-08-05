using Puro.Exceptions;
using Puro.Statements.Alter.Table;
using Xunit;

namespace Puro.Tests.Statements.Alter;

public class AlterColumnTests
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
			Alter.Table("table").InSchema("schema")
				.AlterColumn(null!).AsInt().Null();
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
				.AlterColumn(string.Empty).AsInt().Null();
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
				.AlterColumn("     ").AsInt().Null();
		}
	}

	[Fact]
	public void NonPositiveFixedStringLengthThrows()
	{
		var migration = new NonPositiveFixedStringLengthMigration();

		Assert.Throws<InvalidStringLengthException>(() => migration.Up());
	}

	private sealed class NonPositiveFixedStringLengthMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("table").InSchema("schema")
				.AlterColumn("Name").AsString().FixedLength(0);
		}
	}

	[Fact]
	public void NonPositiveMaximumStringLengthThrows()
	{
		var migration = new NonPositiveMaximumStringLengthMigration();

		Assert.Throws<InvalidStringLengthException>(() => migration.Up());
	}

	private sealed class NonPositiveMaximumStringLengthMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("table").InSchema("schema")
				.AlterColumn("Name").AsString().MaximumLength(0);
		}
	}

	[Fact]
	public void StatementReturnsColumnName()
	{
		var migration = new BoolMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		var (changeType, column) = Assert.Single(statement.ColumnChanges);
		Assert.StrictEqual(TableColumnChangeType.Alter, changeType);
		Assert.Equal("Exists", column.Name);
	}

	[Fact]
	public void StatementReturnsBoolType()
	{
		var migration = new BoolMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		var (changeType, column) = Assert.Single(statement.ColumnChanges);
		Assert.StrictEqual(TableColumnChangeType.Alter, changeType);
		Assert.StrictEqual(typeof(bool), column.Type);
	}

	private sealed class BoolMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("TestTable").InSchema("TestSchema")
				.AlterColumn("Exists").AsBool().Null();
		}
	}

	[Fact]
	public void StatementReturnsShortType()
	{
		var migration = new ShortMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		var (changeType, column) = Assert.Single(statement.ColumnChanges);
		Assert.StrictEqual(TableColumnChangeType.Alter, changeType);
		Assert.StrictEqual(typeof(short), column.Type);
	}

	private sealed class ShortMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("TestTable").InSchema("TestSchema")
				.AlterColumn("Count").AsShort().Null();
		}
	}

	[Fact]
	public void StatementReturnsIntType()
	{
		var migration = new IntMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		var (changeType, column) = Assert.Single(statement.ColumnChanges);
		Assert.StrictEqual(TableColumnChangeType.Alter, changeType);
		Assert.StrictEqual(typeof(int), column.Type);
	}

	private sealed class IntMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("TestTable").InSchema("TestSchema")
				.AlterColumn("Count").AsInt().Null();
		}
	}

	[Fact]
	public void StatementReturnsLongType()
	{
		var migration = new LongMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		var (changeType, column) = Assert.Single(statement.ColumnChanges);
		Assert.StrictEqual(TableColumnChangeType.Alter, changeType);
		Assert.StrictEqual(typeof(long), column.Type);
	}

	private sealed class LongMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("TestTable").InSchema("TestSchema")
				.AlterColumn("Count").AsLong().Null();
		}
	}

	[Fact]
	public void StatementReturnsDoubleType()
	{
		var migration = new DoubleMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		var (changeType, column) = Assert.Single(statement.ColumnChanges);
		Assert.StrictEqual(TableColumnChangeType.Alter, changeType);
		Assert.StrictEqual(typeof(double), column.Type);
	}

	private sealed class DoubleMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("TestTable").InSchema("TestSchema")
				.AlterColumn("Count").AsDouble().Null();
		}
	}

	[Fact]
	public void StatementReturnsDecimalType()
	{
		var migration = new DecimalMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		var (changeType, column) = Assert.Single(statement.ColumnChanges);
		Assert.StrictEqual(TableColumnChangeType.Alter, changeType);
		Assert.StrictEqual(typeof(decimal), column.Type);
	}

	[Fact]
	public void StatementReturnsDecimalPrecision()
	{
		var migration = new DecimalMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		var (changeType, column) = Assert.Single(statement.ColumnChanges);
		Assert.StrictEqual(TableColumnChangeType.Alter, changeType);
		Assert.StrictEqual(5, column.Precision);
	}

	[Fact]
	public void StatementReturnsDecimalScale()
	{
		var migration = new DecimalMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		var (changeType, column) = Assert.Single(statement.ColumnChanges);
		Assert.StrictEqual(TableColumnChangeType.Alter, changeType);
		Assert.StrictEqual(2, column.Scale);
	}

	private sealed class DecimalMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("TestTable").InSchema("TestSchema")
				.AlterColumn("Id").AsDecimal().WithPrecision(5).WithScale(2).NotNull();
		}
	}

	[Fact]
	public void StatementReturnsGuidType()
	{
		var migration = new GuidMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		var (changeType, column) = Assert.Single(statement.ColumnChanges);
		Assert.StrictEqual(TableColumnChangeType.Alter, changeType);
		Assert.StrictEqual(typeof(Guid), column.Type);
	}

	private sealed class GuidMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("TestTable").InSchema("TestSchema")
				.AlterColumn("Id").AsGuid().NotNull();
		}
	}

	[Fact]
	public void StatementReturnsStringType()
	{
		var migration = new StringMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		var (changeType, column) = Assert.Single(statement.ColumnChanges);
		Assert.StrictEqual(TableColumnChangeType.Alter, changeType);
		Assert.StrictEqual(typeof(string), column.Type);
	}

	[Fact]
	public void StatementReturnsNullFixedStringLengthWhenNotDefined()
	{
		var migration = new StringMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		var (changeType, column) = Assert.Single(statement.ColumnChanges);
		Assert.StrictEqual(TableColumnChangeType.Alter, changeType);
		Assert.Null(column.FixedLength);
	}

	[Fact]
	public void StatementReturnsNullMaximumStringLengthWhenNotDefined()
	{
		var migration = new StringMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		var (changeType, column) = Assert.Single(statement.ColumnChanges);
		Assert.StrictEqual(TableColumnChangeType.Alter, changeType);
		Assert.Null(column.MaximumLength);
	}

	private sealed class StringMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("TestTable").InSchema("TestSchema")
				.AlterColumn("Name").AsString().NotNull();
		}
	}

	[Fact]
	public void StatementReturnsFixedStringLength()
	{
		var migration = new FixedStringLengthMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		var (changeType, column) = Assert.Single(statement.ColumnChanges);
		Assert.StrictEqual(TableColumnChangeType.Alter, changeType);
		Assert.StrictEqual(350, column.FixedLength);
	}

	private sealed class FixedStringLengthMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("TestTable").InSchema("TestSchema")
				.AlterColumn("Name").AsString().FixedLength(350).NotNull();
		}
	}

	[Fact]
	public void StatementReturnsMaximumStringLength()
	{
		var migration = new MaximumStringLengthMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		var (changeType, column) = Assert.Single(statement.ColumnChanges);
		Assert.StrictEqual(TableColumnChangeType.Alter, changeType);
		Assert.StrictEqual(760, column.MaximumLength);
	}

	private sealed class MaximumStringLengthMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("TestTable").InSchema("TestSchema")
				.AlterColumn("Name").AsString().MaximumLength(760).NotNull();
		}
	}

	[Fact]
	public void StatementReturnsDateType()
	{
		var migration = new DateMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		var (changeType, column) = Assert.Single(statement.ColumnChanges);
		Assert.StrictEqual(TableColumnChangeType.Alter, changeType);
		Assert.StrictEqual(typeof(DateOnly), column.Type);
	}

	private sealed class DateMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("TestTable").InSchema("TestSchema")
				.AlterColumn("TimeStamp").AsDate().NotNull();
		}
	}

	[Fact]
	public void StatementReturnsTimeType()
	{
		var migration = new TimeMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		var (changeType, column) = Assert.Single(statement.ColumnChanges);
		Assert.StrictEqual(TableColumnChangeType.Alter, changeType);
		Assert.StrictEqual(typeof(TimeOnly), column.Type);
	}

	private sealed class TimeMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("TestTable").InSchema("TestSchema")
				.AlterColumn("TimeStamp").AsTime().NotNull();
		}
	}

	[Fact]
	public void StatementReturnsDateTimeType()
	{
		var migration = new DateTimeMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		var (changeType, column) = Assert.Single(statement.ColumnChanges);
		Assert.StrictEqual(TableColumnChangeType.Alter, changeType);
		Assert.StrictEqual(typeof(DateTime), column.Type);
	}

	private sealed class DateTimeMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("TestTable").InSchema("TestSchema")
				.AlterColumn("TimeStamp").AsDateTime().NotNull();
		}
	}

	[Fact]
	public void StatementReturnsDateTimeOffsetType()
	{
		var migration = new DateTimeOffsetMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		var (changeType, column) = Assert.Single(statement.ColumnChanges);
		Assert.StrictEqual(TableColumnChangeType.Alter, changeType);
		Assert.StrictEqual(typeof(DateTimeOffset), column.Type);
	}

	private sealed class DateTimeOffsetMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("TestTable").InSchema("TestSchema")
				.AlterColumn("TimeStamp").AsDateTimeOffset().NotNull();
		}
	}

	[Fact]
	public void StatementReturnsNullable()
	{
		var migration = new NullableMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		var (changeType, column) = Assert.Single(statement.ColumnChanges);
		Assert.StrictEqual(TableColumnChangeType.Alter, changeType);
		Assert.True(column.Nullable);
	}

	private sealed class NullableMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("TestTable").InSchema("TestSchema")
				.AlterColumn("Name").AsString().Null();
		}
	}

	[Fact]
	public void StatementReturnsNotNullable()
	{
		var migration = new NotNullableMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		var (changeType, column) = Assert.Single(statement.ColumnChanges);
		Assert.StrictEqual(TableColumnChangeType.Alter, changeType);
		Assert.False(column.Nullable);
	}

	private sealed class NotNullableMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("TestTable").InSchema("TestSchema")
				.AlterColumn("Name").AsString().NotNull();
		}
	}

	[Fact]
	public void StatementReturnsMultipleColumns()
	{
		var migration = new MultipleColumnsMigration();
		migration.Up();

		var statement = Assert.Single(migration.Statements) as IAlterTableMigrationStatement;
		Assert.NotNull(statement);
		Assert.StrictEqual(3, statement.ColumnChanges.Count);

		var (changeType, column) = statement.ColumnChanges[0];
		Assert.StrictEqual(TableColumnChangeType.Alter, changeType);
		Assert.Equal("Count", column.Name);
		Assert.StrictEqual(typeof(int), column.Type);
		Assert.True(column.Nullable);

		(changeType, column) = statement.ColumnChanges[1];
		Assert.StrictEqual(TableColumnChangeType.Alter, changeType);
		Assert.Equal("Description", column.Name);
		Assert.StrictEqual(typeof(string), column.Type);
		Assert.True(column.Nullable);
		Assert.Null(column.FixedLength);
		Assert.StrictEqual(1000, column.MaximumLength);

		(changeType, column) = statement.ColumnChanges[2];
		Assert.StrictEqual(TableColumnChangeType.Alter, changeType);
		Assert.Equal("LastUpdated", column.Name);
		Assert.StrictEqual(typeof(DateTimeOffset), column.Type);
		Assert.True(column.Nullable);
	}

	private sealed class MultipleColumnsMigration : UpMigration
	{
		public override void Up()
		{
			Alter.Table("TestTable").InSchema("TestSchema")
				.AlterColumn("Count").AsInt().Null()
				.AlterColumn("Description").AsString().MaximumLength(1000).Null()
				.AlterColumn("LastUpdated").AsDateTimeOffset().Null();
		}
	}
}
