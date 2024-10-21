using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Puro.SqlServer.Runner.Exceptions;
using Puro.SqlServer.Runner.Generators.Create;
using Puro.Statements.Create.Index;
using Xunit;

namespace Puro.SqlServer.Runner.Tests.Generators.Create;

public class CreateIndexGeneratorTests
{
	[Fact]
	public void NullSchemaThrows()
	{
		var idColumn = Substitute.For<IIndexColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Descending.Returns(false);

		var columns = new List<IIndexColumn> { idColumn };
		var statement = Substitute.For<ICreateIndexMigrationStatement>();
		statement.Schema.Returns("schema");
		statement.Table.Returns("table");
		statement.Index.Returns("index");
		statement.Unique.Returns(false);
		statement.Columns.Returns(columns.AsReadOnly());
		statement.Filter.ReturnsNull();

		Assert.Throws<ArgumentNullException>(() => CreateIndexGenerator.Generate(statement, null!));
	}

	[Fact]
	public void EmptySchemaThrows()
	{
		var idColumn = Substitute.For<IIndexColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Descending.Returns(false);

		var columns = new List<IIndexColumn> { idColumn };
		var statement = Substitute.For<ICreateIndexMigrationStatement>();
		statement.Schema.Returns("schema");
		statement.Table.Returns("table");
		statement.Index.Returns("index");
		statement.Unique.Returns(false);
		statement.Columns.Returns(columns.AsReadOnly());
		statement.Filter.ReturnsNull();

		Assert.Throws<ArgumentNullException>(() => CreateIndexGenerator.Generate(statement, string.Empty));
	}

	[Fact]
	public void WhiteSpaceSchemaThrows()
	{
		var idColumn = Substitute.For<IIndexColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Descending.Returns(false);

		var columns = new List<IIndexColumn> { idColumn };
		var statement = Substitute.For<ICreateIndexMigrationStatement>();
		statement.Schema.Returns("schema");
		statement.Table.Returns("table");
		statement.Index.Returns("index");
		statement.Unique.Returns(false);
		statement.Columns.Returns(columns.AsReadOnly());
		statement.Filter.ReturnsNull();

		Assert.Throws<ArgumentNullException>(() => CreateIndexGenerator.Generate(statement, "     "));
	}

	[Fact]
	public void NullTableThrows()
	{
		var idColumn = Substitute.For<IIndexColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Descending.Returns(false);

		var columns = new List<IIndexColumn> { idColumn };
		var statement = Substitute.For<ICreateIndexMigrationStatement>();
		statement.Schema.Returns("schema");
		statement.Table.ReturnsNull();
		statement.Index.Returns("index");
		statement.Unique.Returns(false);
		statement.Columns.Returns(columns.AsReadOnly());
		statement.Filter.ReturnsNull();

		Assert.Throws<IncompleteCreateIndexStatementException>(() => CreateIndexGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void NoColumnDirectionThrows()
	{
		var idColumn = Substitute.For<IIndexColumn>();
		idColumn.Name.Returns("Id");
		idColumn.Descending.ReturnsNull();

		var columns = new List<IIndexColumn> { idColumn };
		var statement = Substitute.For<ICreateIndexMigrationStatement>();
		statement.Schema.Returns("schema");
		statement.Table.Returns("table");
		statement.Index.Returns("index");
		statement.Unique.Returns(false);
		statement.Columns.Returns(columns.AsReadOnly());
		statement.Filter.ReturnsNull();

		Assert.Throws<IncompleteCreateIndexStatementException>(() => CreateIndexGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void NoColumnsThrows()
	{
		var columns = new List<IIndexColumn>();
		var statement = Substitute.For<ICreateIndexMigrationStatement>();
		statement.Schema.Returns("schema");
		statement.Table.Returns("table");
		statement.Index.Returns("index");
		statement.Unique.Returns(false);
		statement.Columns.Returns(columns.AsReadOnly());
		statement.Filter.ReturnsNull();

		Assert.Throws<IncompleteCreateIndexStatementException>(() => CreateIndexGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void SingleColumnGeneratedCorrectly()
	{
		var nameColumn = Substitute.For<IIndexColumn>();
		nameColumn.Name.Returns("Name");
		nameColumn.Descending.Returns(false);

		var columns = new List<IIndexColumn> { nameColumn };
		var statement = Substitute.For<ICreateIndexMigrationStatement>();
		statement.Schema.Returns("dbo");
		statement.Table.Returns("Customer");
		statement.Index.Returns("IX_Customer_Name");
		statement.Unique.Returns(false);
		statement.Columns.Returns(columns.AsReadOnly());
		statement.Filter.ReturnsNull();

		var sql = CreateIndexGenerator.Generate(statement, "dbo");

		const string expected = """
			CREATE INDEX [IX_Customer_Name]
			ON [dbo].[Customer] ([Name] ASC);
			""";

		Assert.Equal(expected, sql);
	}

	[Fact]
	public void MultipleColumnsGeneratedCorrectly()
	{
		var nameColumn = Substitute.For<IIndexColumn>();
		nameColumn.Name.Returns("Name");
		nameColumn.Descending.Returns(false);

		var dateOfBirthColumn = Substitute.For<IIndexColumn>();
		dateOfBirthColumn.Name.Returns("DateOfBirth");
		dateOfBirthColumn.Descending.Returns(true);

		var addressIdColumn = Substitute.For<IIndexColumn>();
		addressIdColumn.Name.Returns("AddressId");
		addressIdColumn.Descending.Returns(false);

		var columns = new List<IIndexColumn> { nameColumn, dateOfBirthColumn, addressIdColumn };
		var statement = Substitute.For<ICreateIndexMigrationStatement>();
		statement.Schema.Returns("dbo");
		statement.Table.Returns("Customer");
		statement.Index.Returns("IX_Customer_Name_DateOfBirth_AddressId");
		statement.Unique.Returns(false);
		statement.Columns.Returns(columns.AsReadOnly());
		statement.Filter.ReturnsNull();

		var sql = CreateIndexGenerator.Generate(statement, "dbo");

		const string expected = """
			CREATE INDEX [IX_Customer_Name_DateOfBirth_AddressId]
			ON [dbo].[Customer] ([Name] ASC, [DateOfBirth] DESC, [AddressId] ASC);
			""";

		Assert.Equal(expected, sql);
	}

	[Fact]
	public void UniqueIndexGeneratedCorrectly()
	{
		var addressIdColumn = Substitute.For<IIndexColumn>();
		addressIdColumn.Name.Returns("AddressId");
		addressIdColumn.Descending.Returns(true);

		var columns = new List<IIndexColumn> { addressIdColumn };
		var statement = Substitute.For<ICreateIndexMigrationStatement>();
		statement.Schema.Returns("dbo");
		statement.Table.Returns("Customer");
		statement.Index.Returns("IX_Customer_AddressId");
		statement.Unique.Returns(true);
		statement.Columns.Returns(columns.AsReadOnly());
		statement.Filter.ReturnsNull();

		var sql = CreateIndexGenerator.Generate(statement, "dbo");

		const string expected = """
			CREATE UNIQUE INDEX [IX_Customer_AddressId]
			ON [dbo].[Customer] ([AddressId] DESC);
			""";

		Assert.Equal(expected, sql);
	}

	[Fact]
	public void FilteredIndexGeneratedCorrectly()
	{
		var nameColumn = Substitute.For<IIndexColumn>();
		nameColumn.Name.Returns("Name");
		nameColumn.Descending.Returns(false);

		var addressIdColumn = Substitute.For<IIndexColumn>();
		addressIdColumn.Name.Returns("AddressId");
		addressIdColumn.Descending.Returns(false);

		var columns = new List<IIndexColumn> { nameColumn, addressIdColumn };
		var statement = Substitute.For<ICreateIndexMigrationStatement>();
		statement.Schema.Returns("dbo");
		statement.Table.Returns("Customer");
		statement.Index.Returns("IX_Customer_Name_AddressId");
		statement.Unique.Returns(false);
		statement.Columns.Returns(columns.AsReadOnly());
		statement.Filter.Returns("[Enabled] = 1");

		var sql = CreateIndexGenerator.Generate(statement, "dbo");

		const string expected = """
			CREATE INDEX [IX_Customer_Name_AddressId]
			ON [dbo].[Customer] ([Name] ASC, [AddressId] ASC)
			WHERE [Enabled] = 1;
			""";

		Assert.Equal(expected, sql);
	}

	[Fact]
	public void StatementSchemaSupersedesMigrationSchema()
	{
		var nameColumn = Substitute.For<IIndexColumn>();
		nameColumn.Name.Returns("Name");
		nameColumn.Descending.Returns(false);

		var columns = new List<IIndexColumn> { nameColumn };
		var statement = Substitute.For<ICreateIndexMigrationStatement>();
		statement.Schema.Returns("correct");
		statement.Table.Returns("Customer");
		statement.Index.Returns("IX_Customer_Name");
		statement.Unique.Returns(false);
		statement.Columns.Returns(columns.AsReadOnly());
		statement.Filter.ReturnsNull();

		var sql = CreateIndexGenerator.Generate(statement, "wrong");

		const string expected = """
			CREATE INDEX [IX_Customer_Name]
			ON [correct].[Customer] ([Name] ASC);
			""";

		Assert.Equal(expected, sql);
	}

	[Fact]
	public void MigrationSchemaUsedWhenStatementSchemaNull()
	{
		var nameColumn = Substitute.For<IIndexColumn>();
		nameColumn.Name.Returns("Name");
		nameColumn.Descending.Returns(false);

		var columns = new List<IIndexColumn> { nameColumn };
		var statement = Substitute.For<ICreateIndexMigrationStatement>();
		statement.Schema.ReturnsNull();
		statement.Table.Returns("Customer");
		statement.Index.Returns("IX_Customer_Name");
		statement.Unique.Returns(false);
		statement.Columns.Returns(columns.AsReadOnly());
		statement.Filter.ReturnsNull();

		var sql = CreateIndexGenerator.Generate(statement, "dbo");

		const string expected = """
			CREATE INDEX [IX_Customer_Name]
			ON [dbo].[Customer] ([Name] ASC);
			""";

		Assert.Equal(expected, sql);
	}
}
