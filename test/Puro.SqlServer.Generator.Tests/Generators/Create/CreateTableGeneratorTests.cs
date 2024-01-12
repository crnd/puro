using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Puro.Exceptions;
using Puro.SqlServer.Generator.Exceptions;
using Puro.SqlServer.Generator.Generators.Create;
using Puro.SqlServer.Generator.Tests.Extensions;
using Puro.Statements.Create.Table;
using Xunit;

namespace Puro.SqlServer.Generator.Tests.Generators.Create;

public class CreateTableGeneratorTests
{
	[Fact]
	public void NullSchemaThrows()
	{
		var idColumn = Substitute.For<ITableColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Type.Returns(typeof(int));
		idColumn.Nullable.Returns(false);
		idColumn.Identity.Returns(false);
		idColumn.Precision.ReturnsNull();
		idColumn.Scale.ReturnsNull();
		idColumn.ExactLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var nameColumn = Substitute.For<ITableColumn>();
		nameColumn.Name.Returns("Name");
		nameColumn.Type.Returns(typeof(string));
		nameColumn.Nullable.Returns(false);
		nameColumn.Identity.Returns(false);
		nameColumn.Precision.ReturnsNull();
		nameColumn.Scale.ReturnsNull();
		nameColumn.ExactLength.ReturnsNull();
		nameColumn.MaximumLength.Returns(250);

		var columns = new List<ITableColumn> { idColumn, nameColumn };
		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.ReturnsNull();
		statement.Columns.Returns(columns.AsReadOnly());

		Assert.Throws<IncompleteCreateTableStatementException>(() => CreateTableGenerator.Generate(statement));
	}

	[Fact]
	public void NoColumnsThrows()
	{
		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(new List<ITableColumn>().AsReadOnly());

		Assert.Throws<IncompleteCreateTableStatementException>(() => CreateTableGenerator.Generate(statement));
	}

	[Fact]
	public void MultipleIdentityColumnsThrows()
	{
		var idColumn = Substitute.For<ITableColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Type.Returns(typeof(int));
		idColumn.Nullable.Returns(false);
		idColumn.Identity.Returns(true);
		idColumn.Precision.ReturnsNull();
		idColumn.Scale.ReturnsNull();
		idColumn.ExactLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var nameColumn = Substitute.For<ITableColumn>();
		nameColumn.Name.Returns("Name");
		nameColumn.Type.Returns(typeof(string));
		nameColumn.Nullable.Returns(false);
		nameColumn.Identity.Returns(true);
		nameColumn.Precision.ReturnsNull();
		nameColumn.Scale.ReturnsNull();
		nameColumn.ExactLength.ReturnsNull();
		nameColumn.MaximumLength.Returns(250);

		var columns = new List<ITableColumn> { idColumn, nameColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		Assert.Throws<MultipleIdentityColumnsException>(() => CreateTableGenerator.Generate(statement));
	}

	[Fact]
	public void NullColumnTypeThrows()
	{
		var idColumn = Substitute.For<ITableColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Type.Returns(typeof(int));
		idColumn.Nullable.Returns(false);
		idColumn.Identity.Returns(true);
		idColumn.Precision.ReturnsNull();
		idColumn.Scale.ReturnsNull();
		idColumn.ExactLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var nameColumn = Substitute.For<ITableColumn>();
		nameColumn.Name.Returns("Name");
		nameColumn.Type.ReturnsNull();
		nameColumn.Nullable.Returns(false);
		nameColumn.Identity.Returns(false);
		nameColumn.Precision.ReturnsNull();
		nameColumn.Scale.ReturnsNull();
		nameColumn.ExactLength.ReturnsNull();
		nameColumn.MaximumLength.Returns(250);

		var columns = new List<ITableColumn> { idColumn, nameColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		Assert.Throws<IncompleteCreateTableStatementException>(() => CreateTableGenerator.Generate(statement));
	}

	[Fact]
	public void NullColumnNullableThrows()
	{
		var idColumn = Substitute.For<ITableColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Type.Returns(typeof(int));
		idColumn.Nullable.Returns(false);
		idColumn.Identity.Returns(true);
		idColumn.Precision.ReturnsNull();
		idColumn.Scale.ReturnsNull();
		idColumn.ExactLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var nameColumn = Substitute.For<ITableColumn>();
		nameColumn.Name.Returns("Name");
		nameColumn.Type.Returns(typeof(string));
		nameColumn.Nullable.ReturnsNull();
		nameColumn.Identity.Returns(false);
		nameColumn.Precision.ReturnsNull();
		nameColumn.Scale.ReturnsNull();
		nameColumn.ExactLength.ReturnsNull();
		nameColumn.MaximumLength.Returns(250);

		var columns = new List<ITableColumn> { idColumn, nameColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		Assert.Throws<IncompleteCreateTableStatementException>(() => CreateTableGenerator.Generate(statement));
	}

	[Fact]
	public void NullDecimalColumnPrecisionThrows()
	{
		var idColumn = Substitute.For<ITableColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Type.Returns(typeof(int));
		idColumn.Nullable.Returns(false);
		idColumn.Identity.Returns(true);
		idColumn.Precision.ReturnsNull();
		idColumn.Scale.ReturnsNull();
		idColumn.ExactLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var amountColumn = Substitute.For<ITableColumn>();
		amountColumn.Name.Returns("Amount");
		amountColumn.Type.Returns(typeof(decimal));
		amountColumn.Nullable.ReturnsNull();
		amountColumn.Identity.Returns(false);
		amountColumn.Precision.ReturnsNull();
		amountColumn.Scale.Returns(3);
		amountColumn.ExactLength.ReturnsNull();
		amountColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn, amountColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		Assert.Throws<IncompleteCreateTableStatementException>(() => CreateTableGenerator.Generate(statement));
	}

	[Fact]
	public void NullDecimalColumnScaleThrows()
	{
		var idColumn = Substitute.For<ITableColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Type.Returns(typeof(int));
		idColumn.Nullable.Returns(false);
		idColumn.Identity.Returns(true);
		idColumn.Precision.ReturnsNull();
		idColumn.Scale.ReturnsNull();
		idColumn.ExactLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var amountColumn = Substitute.For<ITableColumn>();
		amountColumn.Name.Returns("Amount");
		amountColumn.Type.Returns(typeof(decimal));
		amountColumn.Nullable.ReturnsNull();
		amountColumn.Identity.Returns(true);
		amountColumn.Precision.Returns(38);
		amountColumn.Scale.ReturnsNull();
		amountColumn.ExactLength.ReturnsNull();
		amountColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn, amountColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		Assert.Throws<IncompleteCreateTableStatementException>(() => CreateTableGenerator.Generate(statement));
	}

	[Fact]
	public void ZeroDecimalColumnPrecisionThrows()
	{
		var idColumn = Substitute.For<ITableColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Type.Returns(typeof(int));
		idColumn.Nullable.Returns(false);
		idColumn.Identity.Returns(true);
		idColumn.Precision.ReturnsNull();
		idColumn.Scale.ReturnsNull();
		idColumn.ExactLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var amountColumn = Substitute.For<ITableColumn>();
		amountColumn.Name.Returns("Amount");
		amountColumn.Type.Returns(typeof(decimal));
		amountColumn.Nullable.ReturnsNull();
		amountColumn.Identity.Returns(true);
		amountColumn.Precision.Returns(0);
		amountColumn.Scale.Returns(3);
		amountColumn.ExactLength.ReturnsNull();
		amountColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn, amountColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		Assert.Throws<IncompleteCreateTableStatementException>(() => CreateTableGenerator.Generate(statement));
	}

	[Fact]
	public void TooBigDecimalColumnPrecisionThrows()
	{
		var idColumn = Substitute.For<ITableColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Type.Returns(typeof(int));
		idColumn.Nullable.Returns(false);
		idColumn.Identity.Returns(true);
		idColumn.Precision.ReturnsNull();
		idColumn.Scale.ReturnsNull();
		idColumn.ExactLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var amountColumn = Substitute.For<ITableColumn>();
		amountColumn.Name.Returns("Amount");
		amountColumn.Type.Returns(typeof(decimal));
		amountColumn.Nullable.ReturnsNull();
		amountColumn.Identity.Returns(true);
		amountColumn.Precision.Returns(40);
		amountColumn.Scale.Returns(3);
		amountColumn.ExactLength.ReturnsNull();
		amountColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn, amountColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		Assert.Throws<IncompleteCreateTableStatementException>(() => CreateTableGenerator.Generate(statement));
	}

	[Fact]
	public void NegativeDecimalColumnScaleThrows()
	{
		var idColumn = Substitute.For<ITableColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Type.Returns(typeof(int));
		idColumn.Nullable.Returns(false);
		idColumn.Identity.Returns(true);
		idColumn.Precision.ReturnsNull();
		idColumn.Scale.ReturnsNull();
		idColumn.ExactLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var amountColumn = Substitute.For<ITableColumn>();
		amountColumn.Name.Returns("Amount");
		amountColumn.Type.Returns(typeof(decimal));
		amountColumn.Nullable.ReturnsNull();
		amountColumn.Identity.Returns(true);
		amountColumn.Precision.Returns(18);
		amountColumn.Scale.Returns(-3);
		amountColumn.ExactLength.ReturnsNull();
		amountColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn, amountColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		Assert.Throws<IncompleteCreateTableStatementException>(() => CreateTableGenerator.Generate(statement));
	}

	[Fact]
	public void BiggerDecimalColumnScaleThanPrecisionThrows()
	{
		var idColumn = Substitute.For<ITableColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Type.Returns(typeof(int));
		idColumn.Nullable.Returns(false);
		idColumn.Identity.Returns(true);
		idColumn.Precision.ReturnsNull();
		idColumn.Scale.ReturnsNull();
		idColumn.ExactLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var amountColumn = Substitute.For<ITableColumn>();
		amountColumn.Name.Returns("Amount");
		amountColumn.Type.Returns(typeof(decimal));
		amountColumn.Nullable.ReturnsNull();
		amountColumn.Identity.Returns(true);
		amountColumn.Precision.Returns(18);
		amountColumn.Scale.Returns(20);
		amountColumn.ExactLength.ReturnsNull();
		amountColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn, amountColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		Assert.Throws<IncompleteCreateTableStatementException>(() => CreateTableGenerator.Generate(statement));
	}

	[Fact]
	public void TooBigStringColumnMaximumLengthThrows()
	{
		var nameColumn = Substitute.For<ITableColumn>();
		nameColumn.Name.Returns("Name");
		nameColumn.Type.Returns(typeof(string));
		nameColumn.Nullable.Returns(false);
		nameColumn.Identity.Returns(false);
		nameColumn.Precision.ReturnsNull();
		nameColumn.Scale.ReturnsNull();
		nameColumn.ExactLength.ReturnsNull();
		nameColumn.MaximumLength.Returns(5000);

		var columns = new List<ITableColumn> { nameColumn };
		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		Assert.Throws<InvalidStringLengthException>(() => CreateTableGenerator.Generate(statement));
	}

	[Fact]
	public void TooBigStringColumnExactLengthThrows()
	{
		var nameColumn = Substitute.For<ITableColumn>();
		nameColumn.Name.Returns("Name");
		nameColumn.Type.Returns(typeof(string));
		nameColumn.Nullable.Returns(false);
		nameColumn.Identity.Returns(false);
		nameColumn.Precision.ReturnsNull();
		nameColumn.Scale.ReturnsNull();
		nameColumn.ExactLength.Returns(5000);
		nameColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { nameColumn };
		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		Assert.Throws<InvalidStringLengthException>(() => CreateTableGenerator.Generate(statement));
	}

	[Fact]
	public void ShortColumnGeneratedCorrectly()
	{
		var idColumn = Substitute.For<ITableColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Type.Returns(typeof(short));
		idColumn.Nullable.Returns(false);
		idColumn.Identity.Returns(false);
		idColumn.Precision.ReturnsNull();
		idColumn.Scale.ReturnsNull();
		idColumn.ExactLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement);

		var expected = @"
			CREATE TABLE [schema].[table] (
				[Id] SMALLINT NOT NULL
			);";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void IntColumnGeneratedCorrectly()
	{
		var idColumn = Substitute.For<ITableColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Type.Returns(typeof(int));
		idColumn.Nullable.Returns(false);
		idColumn.Identity.Returns(false);
		idColumn.Precision.ReturnsNull();
		idColumn.Scale.ReturnsNull();
		idColumn.ExactLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement);

		var expected = @"
			CREATE TABLE [schema].[table] (
				[Id] INT NOT NULL
			);";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void LongColumnGeneratedCorrectly()
	{
		var idColumn = Substitute.For<ITableColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Type.Returns(typeof(long));
		idColumn.Nullable.Returns(false);
		idColumn.Identity.Returns(false);
		idColumn.Precision.ReturnsNull();
		idColumn.Scale.ReturnsNull();
		idColumn.ExactLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement);

		var expected = @"
			CREATE TABLE [schema].[table] (
				[Id] BIGINT NOT NULL
			);";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void BoolColumnGeneratedCorrectly()
	{
		var idColumn = Substitute.For<ITableColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Type.Returns(typeof(bool));
		idColumn.Nullable.Returns(false);
		idColumn.Identity.Returns(false);
		idColumn.Precision.ReturnsNull();
		idColumn.Scale.ReturnsNull();
		idColumn.ExactLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement);

		var expected = @"
			CREATE TABLE [schema].[table] (
				[Id] BIT NOT NULL
			);";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void DoubleColumnGeneratedCorrectly()
	{
		var idColumn = Substitute.For<ITableColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Type.Returns(typeof(double));
		idColumn.Nullable.Returns(false);
		idColumn.Identity.Returns(false);
		idColumn.Precision.ReturnsNull();
		idColumn.Scale.ReturnsNull();
		idColumn.ExactLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement);

		var expected = @"
			CREATE TABLE [schema].[table] (
				[Id] FLOAT(53) NOT NULL
			);";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void DecimalColumnGeneratedCorrectly()
	{
		var idColumn = Substitute.For<ITableColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Type.Returns(typeof(decimal));
		idColumn.Nullable.Returns(false);
		idColumn.Identity.Returns(false);
		idColumn.Precision.Returns(17);
		idColumn.Scale.Returns(5);
		idColumn.ExactLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement);

		var expected = @"
			CREATE TABLE [schema].[table] (
				[Id] DECIMAL(17, 5) NOT NULL
			);";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void StringColumnGeneratedCorrectly()
	{
		var idColumn = Substitute.For<ITableColumn>();
		idColumn.Name.Returns("Name");
		idColumn.Type.Returns(typeof(string));
		idColumn.Nullable.Returns(false);
		idColumn.Identity.Returns(false);
		idColumn.Precision.ReturnsNull();
		idColumn.Scale.ReturnsNull();
		idColumn.ExactLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement);

		var expected = @"
			CREATE TABLE [schema].[table] (
				[Name] NVARCHAR(MAX) NOT NULL
			);";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void ExactLengthStringColumnGeneratedCorrectly()
	{
		var idColumn = Substitute.For<ITableColumn>();
		idColumn.Name.Returns("Name");
		idColumn.Type.Returns(typeof(string));
		idColumn.Nullable.Returns(false);
		idColumn.Identity.Returns(false);
		idColumn.Precision.ReturnsNull();
		idColumn.Scale.ReturnsNull();
		idColumn.ExactLength.Returns(250);
		idColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement);

		var expected = @"
			CREATE TABLE [schema].[table] (
				[Name] NCHAR(250) NOT NULL
			);";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void MaximumLengthStringColumnGeneratedCorrectly()
	{
		var nameColumn = Substitute.For<ITableColumn>();
		nameColumn.Name.Returns("Name");
		nameColumn.Type.Returns(typeof(string));
		nameColumn.Nullable.Returns(false);
		nameColumn.Identity.Returns(false);
		nameColumn.Precision.ReturnsNull();
		nameColumn.Scale.ReturnsNull();
		nameColumn.ExactLength.ReturnsNull();
		nameColumn.MaximumLength.Returns(750);

		var columns = new List<ITableColumn> { nameColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement);

		var expected = @"
			CREATE TABLE [schema].[table] (
				[Name] NVARCHAR(750) NOT NULL
			);";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void GuidColumnGeneratedCorrectly()
	{
		var idColumn = Substitute.For<ITableColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Type.Returns(typeof(Guid));
		idColumn.Nullable.Returns(false);
		idColumn.Identity.Returns(false);
		idColumn.Precision.ReturnsNull();
		idColumn.Scale.ReturnsNull();
		idColumn.ExactLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement);

		var expected = @"
			CREATE TABLE [schema].[table] (
				[Id] UNIQUEIDENTIFIER NOT NULL
			);";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void DateColumnGeneratedCorrectly()
	{
		var dateColumn = Substitute.For<ITableColumn>();
		dateColumn.Name.Returns("Date");
		dateColumn.Type.Returns(typeof(DateOnly));
		dateColumn.Nullable.Returns(false);
		dateColumn.Identity.Returns(false);
		dateColumn.Precision.ReturnsNull();
		dateColumn.Scale.ReturnsNull();
		dateColumn.ExactLength.ReturnsNull();
		dateColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { dateColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement);

		var expected = @"
			CREATE TABLE [schema].[table] (
				[Date] DATE NOT NULL
			);";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void TimeColumnGeneratedCorrectly()
	{
		var timeColumn = Substitute.For<ITableColumn>();
		timeColumn.Name.Returns("Time");
		timeColumn.Type.Returns(typeof(TimeOnly));
		timeColumn.Nullable.Returns(false);
		timeColumn.Identity.Returns(false);
		timeColumn.Precision.ReturnsNull();
		timeColumn.Scale.ReturnsNull();
		timeColumn.ExactLength.ReturnsNull();
		timeColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { timeColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement);

		var expected = @"
			CREATE TABLE [schema].[table] (
				[Time] TIME NOT NULL
			);";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void DateTimeColumnGeneratedCorrectly()
	{
		var dateTimeColumn = Substitute.For<ITableColumn>();
		dateTimeColumn.Name.Returns("DateTime");
		dateTimeColumn.Type.Returns(typeof(DateTime));
		dateTimeColumn.Nullable.Returns(false);
		dateTimeColumn.Identity.Returns(false);
		dateTimeColumn.Precision.ReturnsNull();
		dateTimeColumn.Scale.ReturnsNull();
		dateTimeColumn.ExactLength.ReturnsNull();
		dateTimeColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { dateTimeColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement);

		var expected = @"
			CREATE TABLE [schema].[table] (
				[DateTime] DATETIME NOT NULL
			);";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void DateTimeOffsetColumnGeneratedCorrectly()
	{
		var dateTimeColumn = Substitute.For<ITableColumn>();
		dateTimeColumn.Name.Returns("DateTime");
		dateTimeColumn.Type.Returns(typeof(DateTimeOffset));
		dateTimeColumn.Nullable.Returns(false);
		dateTimeColumn.Identity.Returns(false);
		dateTimeColumn.Precision.ReturnsNull();
		dateTimeColumn.Scale.ReturnsNull();
		dateTimeColumn.ExactLength.ReturnsNull();
		dateTimeColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { dateTimeColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement);

		var expected = @"
			CREATE TABLE [schema].[table] (
				[DateTime] DATETIMEOFFSET NOT NULL
			);";

		expected.SqlEqual(sql);
	}
}
