﻿using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Puro.Exceptions;
using Puro.SqlServer.Runner.Exceptions;
using Puro.SqlServer.Runner.Generators.Create;
using Puro.Statements;
using Puro.Statements.Create.Table;
using Xunit;

namespace Puro.SqlServer.Runner.Tests.Unit.Generators.Create;

public class CreateTableGeneratorTests
{
	[Fact]
	public void NullSchemaThrows()
	{
		var idColumn = Substitute.For<ITableColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Type.Returns(typeof(int));
		idColumn.Nullable.Returns(false);
		idColumn.Identity.Returns(true);
		idColumn.Precision.ReturnsNull();
		idColumn.Scale.ReturnsNull();
		idColumn.FixedLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn };
		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.ReturnsNull();
		statement.Columns.Returns(columns.AsReadOnly());

		Assert.Throws<ArgumentNullException>(() => CreateTableGenerator.Generate(statement, null!));
	}

	[Fact]
	public void EmptySchemaThrows()
	{
		var idColumn = Substitute.For<ITableColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Type.Returns(typeof(int));
		idColumn.Nullable.Returns(false);
		idColumn.Identity.Returns(true);
		idColumn.Precision.ReturnsNull();
		idColumn.Scale.ReturnsNull();
		idColumn.FixedLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn };
		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.ReturnsNull();
		statement.Columns.Returns(columns.AsReadOnly());

		Assert.Throws<ArgumentNullException>(() => CreateTableGenerator.Generate(statement, string.Empty));
	}

	[Fact]
	public void WhiteSpaceSchemaThrows()
	{
		var idColumn = Substitute.For<ITableColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Type.Returns(typeof(int));
		idColumn.Nullable.Returns(false);
		idColumn.Identity.Returns(true);
		idColumn.Precision.ReturnsNull();
		idColumn.Scale.ReturnsNull();
		idColumn.FixedLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn };
		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.ReturnsNull();
		statement.Columns.Returns(columns.AsReadOnly());

		Assert.Throws<ArgumentNullException>(() => CreateTableGenerator.Generate(statement, "     "));
	}

	[Fact]
	public void NoColumnsThrows()
	{
		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns([]);

		Assert.Throws<IncompleteCreateTableStatementException>(() => CreateTableGenerator.Generate(statement, "schema"));
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
		idColumn.FixedLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var nameColumn = Substitute.For<ITableColumn>();
		nameColumn.Name.Returns("Name");
		nameColumn.Type.Returns(typeof(string));
		nameColumn.Nullable.Returns(false);
		nameColumn.Identity.Returns(true);
		nameColumn.Precision.ReturnsNull();
		nameColumn.Scale.ReturnsNull();
		nameColumn.FixedLength.ReturnsNull();
		nameColumn.MaximumLength.Returns(250);

		var columns = new List<ITableColumn> { idColumn, nameColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		Assert.Throws<MultipleIdentityColumnsException>(() => CreateTableGenerator.Generate(statement, "schema"));
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
		idColumn.FixedLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var nameColumn = Substitute.For<ITableColumn>();
		nameColumn.Name.Returns("Name");
		nameColumn.Type.ReturnsNull();
		nameColumn.Nullable.Returns(false);
		nameColumn.Identity.Returns(false);
		nameColumn.Precision.ReturnsNull();
		nameColumn.Scale.ReturnsNull();
		nameColumn.FixedLength.ReturnsNull();
		nameColumn.MaximumLength.Returns(250);

		var columns = new List<ITableColumn> { idColumn, nameColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		Assert.Throws<IncompleteCreateTableStatementException>(() => CreateTableGenerator.Generate(statement, "schema"));
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
		idColumn.FixedLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var nameColumn = Substitute.For<ITableColumn>();
		nameColumn.Name.Returns("Name");
		nameColumn.Type.Returns(typeof(string));
		nameColumn.Nullable.ReturnsNull();
		nameColumn.Identity.Returns(false);
		nameColumn.Precision.ReturnsNull();
		nameColumn.Scale.ReturnsNull();
		nameColumn.FixedLength.ReturnsNull();
		nameColumn.MaximumLength.Returns(250);

		var columns = new List<ITableColumn> { idColumn, nameColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		Assert.Throws<IncompleteCreateTableStatementException>(() => CreateTableGenerator.Generate(statement, "schema"));
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
		idColumn.FixedLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var amountColumn = Substitute.For<ITableColumn>();
		amountColumn.Name.Returns("Amount");
		amountColumn.Type.Returns(typeof(decimal));
		amountColumn.Nullable.ReturnsNull();
		amountColumn.Identity.Returns(false);
		amountColumn.Precision.ReturnsNull();
		amountColumn.Scale.Returns(3);
		amountColumn.FixedLength.ReturnsNull();
		amountColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn, amountColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		Assert.Throws<IncompleteCreateTableStatementException>(() => CreateTableGenerator.Generate(statement, "schema"));
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
		idColumn.FixedLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var amountColumn = Substitute.For<ITableColumn>();
		amountColumn.Name.Returns("Amount");
		amountColumn.Type.Returns(typeof(decimal));
		amountColumn.Nullable.ReturnsNull();
		amountColumn.Identity.Returns(true);
		amountColumn.Precision.Returns(38);
		amountColumn.Scale.ReturnsNull();
		amountColumn.FixedLength.ReturnsNull();
		amountColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn, amountColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		Assert.Throws<IncompleteCreateTableStatementException>(() => CreateTableGenerator.Generate(statement, "schema"));
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
		idColumn.FixedLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var amountColumn = Substitute.For<ITableColumn>();
		amountColumn.Name.Returns("Amount");
		amountColumn.Type.Returns(typeof(decimal));
		amountColumn.Nullable.ReturnsNull();
		amountColumn.Identity.Returns(true);
		amountColumn.Precision.Returns(0);
		amountColumn.Scale.Returns(3);
		amountColumn.FixedLength.ReturnsNull();
		amountColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn, amountColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		Assert.Throws<IncompleteCreateTableStatementException>(() => CreateTableGenerator.Generate(statement, "schema"));
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
		idColumn.FixedLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var amountColumn = Substitute.For<ITableColumn>();
		amountColumn.Name.Returns("Amount");
		amountColumn.Type.Returns(typeof(decimal));
		amountColumn.Nullable.ReturnsNull();
		amountColumn.Identity.Returns(true);
		amountColumn.Precision.Returns(40);
		amountColumn.Scale.Returns(3);
		amountColumn.FixedLength.ReturnsNull();
		amountColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn, amountColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		Assert.Throws<IncompleteCreateTableStatementException>(() => CreateTableGenerator.Generate(statement, "schema"));
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
		idColumn.FixedLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var amountColumn = Substitute.For<ITableColumn>();
		amountColumn.Name.Returns("Amount");
		amountColumn.Type.Returns(typeof(decimal));
		amountColumn.Nullable.ReturnsNull();
		amountColumn.Identity.Returns(true);
		amountColumn.Precision.Returns(18);
		amountColumn.Scale.Returns(-3);
		amountColumn.FixedLength.ReturnsNull();
		amountColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn, amountColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		Assert.Throws<IncompleteCreateTableStatementException>(() => CreateTableGenerator.Generate(statement, "schema"));
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
		idColumn.FixedLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var amountColumn = Substitute.For<ITableColumn>();
		amountColumn.Name.Returns("Amount");
		amountColumn.Type.Returns(typeof(decimal));
		amountColumn.Nullable.ReturnsNull();
		amountColumn.Identity.Returns(true);
		amountColumn.Precision.Returns(18);
		amountColumn.Scale.Returns(20);
		amountColumn.FixedLength.ReturnsNull();
		amountColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn, amountColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		Assert.Throws<IncompleteCreateTableStatementException>(() => CreateTableGenerator.Generate(statement, "schema"));
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
		nameColumn.FixedLength.ReturnsNull();
		nameColumn.MaximumLength.Returns(5000);

		var columns = new List<ITableColumn> { nameColumn };
		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		Assert.Throws<InvalidStringLengthException>(() => CreateTableGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void TooBigStringColumnFixedLengthThrows()
	{
		var nameColumn = Substitute.For<ITableColumn>();
		nameColumn.Name.Returns("Name");
		nameColumn.Type.Returns(typeof(string));
		nameColumn.Nullable.Returns(false);
		nameColumn.Identity.Returns(false);
		nameColumn.Precision.ReturnsNull();
		nameColumn.Scale.ReturnsNull();
		nameColumn.FixedLength.Returns(5000);
		nameColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { nameColumn };
		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		Assert.Throws<InvalidStringLengthException>(() => CreateTableGenerator.Generate(statement, "schema"));
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
		idColumn.FixedLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement, "schema");

		const string expected = """
			CREATE TABLE [schema].[table] (
			[Id] SMALLINT NOT NULL);
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
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
		idColumn.FixedLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement, "schema");

		const string expected = """
			CREATE TABLE [schema].[table] (
			[Id] INT NOT NULL);
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
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
		idColumn.FixedLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement, "schema");

		const string expected = """
			CREATE TABLE [schema].[table] (
			[Id] BIGINT NOT NULL);
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
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
		idColumn.FixedLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement, "schema");

		const string expected = """
			CREATE TABLE [schema].[table] (
			[Id] BIT NOT NULL);
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
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
		idColumn.FixedLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement, "schema");

		const string expected = """
			CREATE TABLE [schema].[table] (
			[Id] FLOAT(53) NOT NULL);
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
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
		idColumn.FixedLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement, "schema");

		const string expected = """
			CREATE TABLE [schema].[table] (
			[Id] DECIMAL(17, 5) NOT NULL);
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
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
		idColumn.FixedLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement, "schema");

		const string expected = """
			CREATE TABLE [schema].[table] (
			[Name] NVARCHAR(MAX) NOT NULL);
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void FixedLengthStringColumnGeneratedCorrectly()
	{
		var idColumn = Substitute.For<ITableColumn>();
		idColumn.Name.Returns("Name");
		idColumn.Type.Returns(typeof(string));
		idColumn.Nullable.Returns(false);
		idColumn.Identity.Returns(false);
		idColumn.Precision.ReturnsNull();
		idColumn.Scale.ReturnsNull();
		idColumn.FixedLength.Returns(250);
		idColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement, "schema");

		const string expected = """
			CREATE TABLE [schema].[table] (
			[Name] NCHAR(250) NOT NULL);
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
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
		nameColumn.FixedLength.ReturnsNull();
		nameColumn.MaximumLength.Returns(750);

		var columns = new List<ITableColumn> { nameColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement, "schema");

		const string expected = """
			CREATE TABLE [schema].[table] (
			[Name] NVARCHAR(750) NOT NULL);
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
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
		idColumn.FixedLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement, "schema");

		const string expected = """
			CREATE TABLE [schema].[table] (
			[Id] UNIQUEIDENTIFIER NOT NULL);
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
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
		dateColumn.FixedLength.ReturnsNull();
		dateColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { dateColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement, "schema");

		const string expected = """
			CREATE TABLE [schema].[table] (
			[Date] DATE NOT NULL);
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
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
		timeColumn.FixedLength.ReturnsNull();
		timeColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { timeColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement, "schema");

		const string expected = """
			CREATE TABLE [schema].[table] (
			[Time] TIME NOT NULL);
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
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
		dateTimeColumn.FixedLength.ReturnsNull();
		dateTimeColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { dateTimeColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement, "schema");

		const string expected = """
			CREATE TABLE [schema].[table] (
			[DateTime] DATETIME2 NOT NULL);
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
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
		dateTimeColumn.FixedLength.ReturnsNull();
		dateTimeColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { dateTimeColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement, "schema");

		const string expected = """
			CREATE TABLE [schema].[table] (
			[DateTime] DATETIMEOFFSET NOT NULL);
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void MultipleColumnsGeneratedCorrectly()
	{
		var idColumn = Substitute.For<ITableColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Type.Returns(typeof(int));
		idColumn.Nullable.Returns(false);
		idColumn.Identity.Returns(true);
		idColumn.Precision.ReturnsNull();
		idColumn.Scale.ReturnsNull();
		idColumn.FixedLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var nameColumn = Substitute.For<ITableColumn>();
		nameColumn.Name.Returns("Name");
		nameColumn.Type.Returns(typeof(string));
		nameColumn.Nullable.Returns(false);
		nameColumn.Identity.Returns(false);
		nameColumn.Precision.ReturnsNull();
		nameColumn.Scale.ReturnsNull();
		nameColumn.FixedLength.ReturnsNull();
		nameColumn.MaximumLength.Returns(200);

		var descriptionColumn = Substitute.For<ITableColumn>();
		descriptionColumn.Name.Returns("Description");
		descriptionColumn.Type.Returns(typeof(string));
		descriptionColumn.Nullable.Returns(true);
		descriptionColumn.Identity.Returns(false);
		descriptionColumn.Precision.ReturnsNull();
		descriptionColumn.Scale.ReturnsNull();
		descriptionColumn.FixedLength.ReturnsNull();
		descriptionColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn, nameColumn, descriptionColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement, "schema");

		const string expected = """
			CREATE TABLE [schema].[table] (
			[Id] INT NOT NULL IDENTITY(1, 1),
			[Name] NVARCHAR(200) NOT NULL,
			[Description] NVARCHAR(MAX) NULL);
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void StatementSchemaSupersedesMigrationSchema()
	{
		var idColumn = Substitute.For<ITableColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Type.Returns(typeof(bool));
		idColumn.Nullable.Returns(false);
		idColumn.Identity.Returns(false);
		idColumn.Precision.ReturnsNull();
		idColumn.Scale.ReturnsNull();
		idColumn.FixedLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("correct");
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement, "wrong");

		const string expected = """
			CREATE TABLE [correct].[table] (
			[Id] BIT NOT NULL);
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void MigrationSchemaUsedWhenStatementSchemaNull()
	{
		var idColumn = Substitute.For<ITableColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Type.Returns(typeof(bool));
		idColumn.Nullable.Returns(false);
		idColumn.Identity.Returns(false);
		idColumn.Precision.ReturnsNull();
		idColumn.Scale.ReturnsNull();
		idColumn.FixedLength.ReturnsNull();
		idColumn.MaximumLength.ReturnsNull();

		var columns = new List<ITableColumn> { idColumn };

		var statement = Substitute.For<ICreateTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.ReturnsNull();
		statement.Columns.Returns(columns.AsReadOnly());

		var sql = CreateTableGenerator.Generate(statement, "schema");

		const string expected = """
			CREATE TABLE [schema].[table] (
			[Id] BIT NOT NULL);
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
	}
}
