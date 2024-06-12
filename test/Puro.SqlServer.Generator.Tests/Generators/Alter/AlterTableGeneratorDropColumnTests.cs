using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Puro.SqlServer.Generator.Exceptions;
using Puro.SqlServer.Generator.Generators.Alter;
using Puro.SqlServer.Generator.Tests.Extensions;
using Puro.Statements;
using Puro.Statements.Alter.Table;
using System.Collections.ObjectModel;
using Xunit;

namespace Puro.SqlServer.Generator.Tests.Generators.Alter;

public class AlterTableGeneratorDropColumnTests
{
	[Fact]
	public void NullSchemaThrows()
	{
		var changes = GenerateColumnChanges("column1", "column2");
		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.ReturnsNull();
		statement.ColumnChanges.Returns(changes);

		Assert.Throws<IncompleteAlterTableStatementException>(() => AlterTableGenerator.Generate(statement));
	}

	[Fact]
	public void SingleDropColumnGeneratedCorrectly()
	{
		var changes = GenerateColumnChanges("column1");
		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes);

		var sql = AlterTableGenerator.Generate(statement);

		var expected = """
			ALTER TABLE [schema].[table]
				DROP COLUMN [column1];
			""";

		expected.SqlEqual(sql);
	}

	[Fact]
	public void MultipleDropColumnsGeneratedCorrectly()
	{
		var changes = GenerateColumnChanges("column1", "column2", "column3");
		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes);

		var sql = AlterTableGenerator.Generate(statement);

		var expected = """
			ALTER TABLE [schema].[table]
				DROP COLUMN [column1], [column2], [column3];
			""";

		expected.SqlEqual(sql);
	}

	private static ReadOnlyCollection<(TableColumnChangeType, ITableColumn)> GenerateColumnChanges(params string[] columnNames)
	{
		var changes = new List<(TableColumnChangeType, ITableColumn)>();
		foreach (var name in columnNames)
		{
			var column = Substitute.For<ITableColumn>();
			column.Name.Returns(name);
			changes.Add((TableColumnChangeType.Drop, column));
		}

		return changes.AsReadOnly();
	}
}
