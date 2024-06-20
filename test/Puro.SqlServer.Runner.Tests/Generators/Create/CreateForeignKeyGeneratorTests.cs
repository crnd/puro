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

		Assert.Throws<IncompleteCreateForeignKeyStatementException>(() => CreateForeignKeyGenerator.Generate(statement));
	}

	[Fact]
	public void NullReferencingTableSchemaThrows()
	{
		var statement = Substitute.For<ICreateForeignKeyMigrationStatement>();
		statement.ForeignKey.Returns("FK_referencingTable_referencedTable");
		statement.ReferencingTable.Returns("referencingTable");
		statement.ReferencingTableSchema.ReturnsNull();
		statement.ReferencingColumns.Returns(["Id"]);
		statement.ReferencedTable.Returns("referencedTable");
		statement.ReferencedTableSchema.Returns("schema");
		statement.ReferencedColumns.Returns(["Id"]);
		statement.OnDelete.Returns(OnDeleteBehavior.Restrict);

		Assert.Throws<IncompleteCreateForeignKeyStatementException>(() => CreateForeignKeyGenerator.Generate(statement));
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

		Assert.Throws<IncompleteCreateForeignKeyStatementException>(() => CreateForeignKeyGenerator.Generate(statement));
	}

	[Fact]
	public void NullReferencedTableSchemaThrows()
	{
		var statement = Substitute.For<ICreateForeignKeyMigrationStatement>();
		statement.ForeignKey.Returns("FK_referencingTable_referencedTable");
		statement.ReferencingTable.Returns("referencingTable");
		statement.ReferencingTableSchema.Returns("schema");
		statement.ReferencingColumns.Returns(["Id"]);
		statement.ReferencedTable.Returns("referencedTable");
		statement.ReferencedTableSchema.ReturnsNull();
		statement.ReferencedColumns.Returns(["Id"]);
		statement.OnDelete.Returns(OnDeleteBehavior.Restrict);

		Assert.Throws<IncompleteCreateForeignKeyStatementException>(() => CreateForeignKeyGenerator.Generate(statement));
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

		Assert.Throws<IncompleteCreateForeignKeyStatementException>(() => CreateForeignKeyGenerator.Generate(statement));
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

		Assert.Throws<IncompleteCreateForeignKeyStatementException>(() => CreateForeignKeyGenerator.Generate(statement));
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

		Assert.Throws<IncompleteCreateForeignKeyStatementException>(() => CreateForeignKeyGenerator.Generate(statement));
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

		var sql = CreateForeignKeyGenerator.Generate(statement);

		var expected = """
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

		var sql = CreateForeignKeyGenerator.Generate(statement);

		var expected = """
			ALTER TABLE [fromSchema].[referencingTable]
				ADD CONSTRAINT [FK_referencingTable_referencedTable] FOREIGN KEY ([ReferencingId])
					REFERENCES [toSchema].[referencedTable] ([ReferencedId])
					ON DELETE NO ACTION;
			""";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void MultipleColumnCorrectlyGenerated()
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

		var sql = CreateForeignKeyGenerator.Generate(statement);

		var expected = """
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

		var sql = CreateForeignKeyGenerator.Generate(statement);

		var expected = """
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

		var sql = CreateForeignKeyGenerator.Generate(statement);

		var expected = """
			ALTER TABLE [schema].[referencingTable]
				ADD CONSTRAINT [FK_referencingTable_referencedTable] FOREIGN KEY ([ReferencingId])
					REFERENCES [schema].[referencedTable] ([ReferencedId])
					ON DELETE SET NULL;
			""";

		expected.SqlEqual(sql);
	}
}
