using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Puro.SqlServer.Runner.Exceptions;
using Puro.SqlServer.Runner.Generators.Create;
using Puro.SqlServer.Runner.Tests.Extensions;
using Puro.Statements;
using Puro.Statements.Create.ForeignKey;
using Xunit;

namespace Puro.SqlServer.Runner.Tests.Generators.Create;

public class CreateForeignKeyGeneratorTests
{
	[Fact]
	public void NullSchemaThrows()
	{
		var statement = Substitute.For<ICreateForeignKeyMigrationStatement>();
		statement.ForeignKey.Returns("FK_referencingTable_referencedTable");
		statement.ReferencingTable.Returns("referencingTable");
		statement.ReferencingTableSchema.Returns("schema");
		statement.ReferencingColumns.Returns(["Id"]);
		statement.ReferencedTable.Returns("referencedTable");
		statement.ReferencedTableSchema.Returns("schema");
		statement.ReferencedColumns.Returns(["Id"]);
		statement.OnDelete.Returns(OnDeleteBehavior.Restrict);

		Assert.Throws<ArgumentNullException>(() => CreateForeignKeyGenerator.Generate(statement, null!));
	}

	[Fact]
	public void EmptySchemaThrows()
	{
		var statement = Substitute.For<ICreateForeignKeyMigrationStatement>();
		statement.ForeignKey.Returns("FK_referencingTable_referencedTable");
		statement.ReferencingTable.Returns("referencingTable");
		statement.ReferencingTableSchema.Returns("schema");
		statement.ReferencingColumns.Returns(["Id"]);
		statement.ReferencedTable.Returns("referencedTable");
		statement.ReferencedTableSchema.Returns("schema");
		statement.ReferencedColumns.Returns(["Id"]);
		statement.OnDelete.Returns(OnDeleteBehavior.Restrict);

		Assert.Throws<ArgumentNullException>(() => CreateForeignKeyGenerator.Generate(statement, string.Empty));
	}

	[Fact]
	public void WhiteSpaceSchemaThrows()
	{
		var statement = Substitute.For<ICreateForeignKeyMigrationStatement>();
		statement.ForeignKey.Returns("FK_referencingTable_referencedTable");
		statement.ReferencingTable.Returns("referencingTable");
		statement.ReferencingTableSchema.Returns("schema");
		statement.ReferencingColumns.Returns(["Id"]);
		statement.ReferencedTable.Returns("referencedTable");
		statement.ReferencedTableSchema.Returns("schema");
		statement.ReferencedColumns.Returns(["Id"]);
		statement.OnDelete.Returns(OnDeleteBehavior.Restrict);

		Assert.Throws<ArgumentNullException>(() => CreateForeignKeyGenerator.Generate(statement, "     "));
	}

	[Fact]
	public void NullReferencingTableThrows()
	{
		var statement = Substitute.For<ICreateForeignKeyMigrationStatement>();
		statement.ForeignKey.Returns("FK_referencingTable_referencedTable");
		statement.ReferencingTable.ReturnsNull();
		statement.ReferencingTableSchema.Returns("schema");
		statement.ReferencingColumns.Returns(["Id"]);
		statement.ReferencedTable.Returns("referencedTable");
		statement.ReferencedTableSchema.Returns("schema");
		statement.ReferencedColumns.Returns(["Id"]);
		statement.OnDelete.Returns(OnDeleteBehavior.Restrict);

		Assert.Throws<IncompleteCreateForeignKeyStatementException>(() => CreateForeignKeyGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void NullReferencedTableThrows()
	{
		var statement = Substitute.For<ICreateForeignKeyMigrationStatement>();
		statement.ForeignKey.Returns("FK_referencingTable_referencedTable");
		statement.ReferencingTable.Returns("referencingTable");
		statement.ReferencingTableSchema.Returns("schema");
		statement.ReferencingColumns.Returns(["Id"]);
		statement.ReferencedTable.ReturnsNull();
		statement.ReferencedTableSchema.Returns("schema");
		statement.ReferencedColumns.Returns(["Id"]);
		statement.OnDelete.Returns(OnDeleteBehavior.Restrict);

		Assert.Throws<IncompleteCreateForeignKeyStatementException>(() => CreateForeignKeyGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void EmptyReferencedColumnsThrows()
	{
		var statement = Substitute.For<ICreateForeignKeyMigrationStatement>();
		statement.ForeignKey.Returns("FK_referencingTable_referencedTable");
		statement.ReferencingTable.Returns("referencingTable");
		statement.ReferencingTableSchema.Returns("schema");
		statement.ReferencingColumns.Returns(["Id"]);
		statement.ReferencedTable.Returns("referencedTable");
		statement.ReferencedTableSchema.Returns("schema");
		statement.ReferencedColumns.Returns([]);
		statement.OnDelete.Returns(OnDeleteBehavior.Restrict);

		Assert.Throws<IncompleteCreateForeignKeyStatementException>(() => CreateForeignKeyGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void MismatchingColumnsCountThrows()
	{
		var statement = Substitute.For<ICreateForeignKeyMigrationStatement>();
		statement.ForeignKey.Returns("FK_referencingTable_referencedTable");
		statement.ReferencingTable.Returns("referencingTable");
		statement.ReferencingTableSchema.Returns("schema");
		statement.ReferencingColumns.Returns(["Id"]);
		statement.ReferencedTable.Returns("referencedTable");
		statement.ReferencedTableSchema.Returns("schema");
		statement.ReferencedColumns.Returns(["Id1", "Id2"]);
		statement.OnDelete.Returns(OnDeleteBehavior.Restrict);

		Assert.Throws<IncompleteCreateForeignKeyStatementException>(() => CreateForeignKeyGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void UndefinedOnDeleteThrows()
	{
		var statement = Substitute.For<ICreateForeignKeyMigrationStatement>();
		statement.ForeignKey.Returns("FK_referencingTable_referencedTable");
		statement.ReferencingTable.Returns("referencingTable");
		statement.ReferencingTableSchema.Returns("schema");
		statement.ReferencingColumns.Returns(["Id"]);
		statement.ReferencedTable.Returns("referencedTable");
		statement.ReferencedTableSchema.Returns("schema");
		statement.ReferencedColumns.Returns(["Id"]);
		statement.OnDelete.ReturnsNull();

		Assert.Throws<IncompleteCreateForeignKeyStatementException>(() => CreateForeignKeyGenerator.Generate(statement, "schema"));
	}

	[Fact]
	public void SameSchemaCorrectlyGenerated()
	{
		var statement = Substitute.For<ICreateForeignKeyMigrationStatement>();
		statement.ForeignKey.Returns("FK_referencingTable_referencedTable");
		statement.ReferencingTable.Returns("referencingTable");
		statement.ReferencingTableSchema.Returns("schema");
		statement.ReferencingColumns.Returns(["ReferencingId"]);
		statement.ReferencedTable.Returns("referencedTable");
		statement.ReferencedTableSchema.Returns("schema");
		statement.ReferencedColumns.Returns(["ReferencedId"]);
		statement.OnDelete.Returns(OnDeleteBehavior.Restrict);

		var sql = CreateForeignKeyGenerator.Generate(statement, "schema");

		const string expected = """
			ALTER TABLE [schema].[referencingTable]
			ADD CONSTRAINT [FK_referencingTable_referencedTable] FOREIGN KEY ([ReferencingId])
			REFERENCES [schema].[referencedTable] ([ReferencedId])
			ON DELETE NO ACTION;
			""";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void DifferentSchemaCorrectlyGenerated()
	{
		var statement = Substitute.For<ICreateForeignKeyMigrationStatement>();
		statement.ForeignKey.Returns("FK_referencingTable_referencedTable");
		statement.ReferencingTable.Returns("referencingTable");
		statement.ReferencingTableSchema.Returns("fromSchema");
		statement.ReferencingColumns.Returns(["ReferencingId"]);
		statement.ReferencedTable.Returns("referencedTable");
		statement.ReferencedTableSchema.Returns("toSchema");
		statement.ReferencedColumns.Returns(["ReferencedId"]);
		statement.OnDelete.Returns(OnDeleteBehavior.Restrict);

		var sql = CreateForeignKeyGenerator.Generate(statement, "schema");

		const string expected = """
			ALTER TABLE [fromSchema].[referencingTable]
			ADD CONSTRAINT [FK_referencingTable_referencedTable] FOREIGN KEY ([ReferencingId])
			REFERENCES [toSchema].[referencedTable] ([ReferencedId])
			ON DELETE NO ACTION;
			""";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void MultipleColumnsCorrectlyGenerated()
	{
		var statement = Substitute.For<ICreateForeignKeyMigrationStatement>();
		statement.ForeignKey.Returns("FK_referencingTable_referencedTable");
		statement.ReferencingTable.Returns("referencingTable");
		statement.ReferencingTableSchema.Returns("fromSchema");
		statement.ReferencingColumns.Returns(["ReferencingId1", "ReferencingId2"]);
		statement.ReferencedTable.Returns("referencedTable");
		statement.ReferencedTableSchema.Returns("toSchema");
		statement.ReferencedColumns.Returns(["ReferencedId1", "ReferencedId2"]);
		statement.OnDelete.Returns(OnDeleteBehavior.Restrict);

		var sql = CreateForeignKeyGenerator.Generate(statement, "schema");

		const string expected = """
			ALTER TABLE [fromSchema].[referencingTable]
			ADD CONSTRAINT [FK_referencingTable_referencedTable] FOREIGN KEY ([ReferencingId1], [ReferencingId2])
			REFERENCES [toSchema].[referencedTable] ([ReferencedId1], [ReferencedId2])
			ON DELETE NO ACTION;
			""";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void CascadeCorrectlyGenerated()
	{
		var statement = Substitute.For<ICreateForeignKeyMigrationStatement>();
		statement.ForeignKey.Returns("FK_referencingTable_referencedTable");
		statement.ReferencingTable.Returns("referencingTable");
		statement.ReferencingTableSchema.Returns("schema");
		statement.ReferencingColumns.Returns(["ReferencingId"]);
		statement.ReferencedTable.Returns("referencedTable");
		statement.ReferencedTableSchema.Returns("schema");
		statement.ReferencedColumns.Returns(["ReferencedId"]);
		statement.OnDelete.Returns(OnDeleteBehavior.Cascade);

		var sql = CreateForeignKeyGenerator.Generate(statement, "schema");

		const string expected = """
			ALTER TABLE [schema].[referencingTable]
			ADD CONSTRAINT [FK_referencingTable_referencedTable] FOREIGN KEY ([ReferencingId])
			REFERENCES [schema].[referencedTable] ([ReferencedId])
			ON DELETE CASCADE;
			""";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void SetNullCorrectlyGenerated()
	{
		var statement = Substitute.For<ICreateForeignKeyMigrationStatement>();
		statement.ForeignKey.Returns("FK_referencingTable_referencedTable");
		statement.ReferencingTable.Returns("referencingTable");
		statement.ReferencingTableSchema.Returns("schema");
		statement.ReferencingColumns.Returns(["ReferencingId"]);
		statement.ReferencedTable.Returns("referencedTable");
		statement.ReferencedTableSchema.Returns("schema");
		statement.ReferencedColumns.Returns(["ReferencedId"]);
		statement.OnDelete.Returns(OnDeleteBehavior.SetNull);

		var sql = CreateForeignKeyGenerator.Generate(statement, "schema");

		const string expected = """
			ALTER TABLE [schema].[referencingTable]
			ADD CONSTRAINT [FK_referencingTable_referencedTable] FOREIGN KEY ([ReferencingId])
			REFERENCES [schema].[referencedTable] ([ReferencedId])
			ON DELETE SET NULL;
			""";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void StatementSchemaSupersedesMigrationSchema()
	{
		var statement = Substitute.For<ICreateForeignKeyMigrationStatement>();
		statement.ForeignKey.Returns("FK_referencingTable_referencedTable");
		statement.ReferencingTable.Returns("referencingTable");
		statement.ReferencingTableSchema.Returns("correct");
		statement.ReferencingColumns.Returns(["ReferencingId1", "ReferencingId2"]);
		statement.ReferencedTable.Returns("referencedTable");
		statement.ReferencedTableSchema.Returns("correct");
		statement.ReferencedColumns.Returns(["ReferencedId1", "ReferencedId2"]);
		statement.OnDelete.Returns(OnDeleteBehavior.Restrict);

		var sql = CreateForeignKeyGenerator.Generate(statement, "wrong");

		const string expected = """
			ALTER TABLE [correct].[referencingTable]
			ADD CONSTRAINT [FK_referencingTable_referencedTable] FOREIGN KEY ([ReferencingId1], [ReferencingId2])
			REFERENCES [correct].[referencedTable] ([ReferencedId1], [ReferencedId2])
			ON DELETE NO ACTION;
			""";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void MigrationSchemaUsedWhenStatementSchemaNull()
	{
		var statement = Substitute.For<ICreateForeignKeyMigrationStatement>();
		statement.ForeignKey.Returns("FK_referencingTable_referencedTable");
		statement.ReferencingTable.Returns("referencingTable");
		statement.ReferencingTableSchema.ReturnsNull();
		statement.ReferencingColumns.Returns(["ReferencingId1", "ReferencingId2"]);
		statement.ReferencedTable.Returns("referencedTable");
		statement.ReferencedTableSchema.ReturnsNull();
		statement.ReferencedColumns.Returns(["ReferencedId1", "ReferencedId2"]);
		statement.OnDelete.Returns(OnDeleteBehavior.Restrict);

		var sql = CreateForeignKeyGenerator.Generate(statement, "testschema");

		const string expected = """
			ALTER TABLE [testschema].[referencingTable]
			ADD CONSTRAINT [FK_referencingTable_referencedTable] FOREIGN KEY ([ReferencingId1], [ReferencingId2])
			REFERENCES [testschema].[referencedTable] ([ReferencedId1], [ReferencedId2])
			ON DELETE NO ACTION;
			""";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void MigrationSchemaUsedWhenStatementReferencingTableSchemaNull()
	{
		var statement = Substitute.For<ICreateForeignKeyMigrationStatement>();
		statement.ForeignKey.Returns("FK_referencingTable_referencedTable");
		statement.ReferencingTable.Returns("referencingTable");
		statement.ReferencingTableSchema.ReturnsNull();
		statement.ReferencingColumns.Returns(["ReferencingId"]);
		statement.ReferencedTable.Returns("referencedTable");
		statement.ReferencedTableSchema.Returns("statement");
		statement.ReferencedColumns.Returns(["ReferencedId"]);
		statement.OnDelete.Returns(OnDeleteBehavior.Cascade);

		var sql = CreateForeignKeyGenerator.Generate(statement, "migration");

		const string expected = """
			ALTER TABLE [migration].[referencingTable]
			ADD CONSTRAINT [FK_referencingTable_referencedTable] FOREIGN KEY ([ReferencingId])
			REFERENCES [statement].[referencedTable] ([ReferencedId])
			ON DELETE CASCADE;
			""";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void MigrationSchemaUsedWhenStatementReferencedTableSchemaNull()
	{
		var statement = Substitute.For<ICreateForeignKeyMigrationStatement>();
		statement.ForeignKey.Returns("FK_referencingTable_referencedTable");
		statement.ReferencingTable.Returns("referencingTable");
		statement.ReferencingTableSchema.Returns("statement");
		statement.ReferencingColumns.Returns(["ReferencingId"]);
		statement.ReferencedTable.Returns("referencedTable");
		statement.ReferencedTableSchema.ReturnsNull();
		statement.ReferencedColumns.Returns(["ReferencedId"]);
		statement.OnDelete.Returns(OnDeleteBehavior.Cascade);

		var sql = CreateForeignKeyGenerator.Generate(statement, "migration");

		const string expected = """
			ALTER TABLE [statement].[referencingTable]
			ADD CONSTRAINT [FK_referencingTable_referencedTable] FOREIGN KEY ([ReferencingId])
			REFERENCES [migration].[referencedTable] ([ReferencedId])
			ON DELETE CASCADE;
			""";

		expected.SqlEqual(sql);
	}
}
