using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Puro.SqlServer.Runner.Exceptions;
using Puro.SqlServer.Runner.Generators.Create;
using Puro.SqlServer.Runner.Tests.Extensions;
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
		statement.Schema.ReturnsNull();
		statement.Table.Returns("table");
		statement.Index.Returns("index");
		statement.Unique.Returns(false);
		statement.Columns.Returns(columns.AsReadOnly());
		statement.Filter.ReturnsNull();

		Assert.Throws<IncompleteCreateIndexStatementException>(() => CreateIndexGenerator.Generate(statement));
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

		Assert.Throws<IncompleteCreateIndexStatementException>(() => CreateIndexGenerator.Generate(statement));
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

		Assert.Throws<IncompleteCreateIndexStatementException>(() => CreateIndexGenerator.Generate(statement));
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

		Assert.Throws<IncompleteCreateIndexStatementException>(() => CreateIndexGenerator.Generate(statement));
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

		var sql = CreateIndexGenerator.Generate(statement);

		var expected = """
			CREATE INDEX [IX_Customer_Name]
				ON [dbo].[Customer] ([Name] ASC);
			""";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void MultipleColumnGeneratedCorrectly()
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

		var sql = CreateIndexGenerator.Generate(statement);

		var expected = """
			CREATE INDEX [IX_Customer_Name_DateOfBirth_AddressId]
				ON [dbo].[Customer] ([Name] ASC, [DateOfBirth] DESC, [AddressId] ASC);
			""";

		expected.SqlEqual(sql);
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

		var sql = CreateIndexGenerator.Generate(statement);

		var expected = """
			CREATE UNIQUE INDEX [IX_Customer_AddressId]
				ON [dbo].[Customer] ([AddressId] DESC);
			""";

		expected.SqlEqual(sql);
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

		var sql = CreateIndexGenerator.Generate(statement);

		var expected = """
			CREATE INDEX [IX_Customer_Name_AddressId]
				ON [dbo].[Customer] ([Name] ASC, [AddressId] ASC)
				WHERE [Enabled] = 1;
			""";

		expected.SqlEqual(sql);
	}
}
