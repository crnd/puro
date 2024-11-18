using NSubstitute;
using Puro.SqlServer.Runner.Generators.Alter;
using Puro.Statements;
using Puro.Statements.Alter.Table;
using System.Collections.ObjectModel;
using Xunit;

namespace Puro.SqlServer.Runner.Tests.Unit.Generators.Alter;

public class AlterTableGeneratorDropColumnTests
{
	[Fact]
	public void SingleDropColumnGeneratedCorrectly()
	{
		var changes = GenerateColumnChanges("column1");
		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes);

		var sql = AlterTableGenerator.Generate(statement, "schema");

		const string expected = """
			ALTER TABLE [schema].[table]
			DROP COLUMN [column1];
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void MultipleDropColumnsGeneratedCorrectly()
	{
		var changes = GenerateColumnChanges("column1", "column2", "column3");
		var statement = Substitute.For<IAlterTableMigrationStatement>();
		statement.Table.Returns("table");
		statement.Schema.Returns("schema");
		statement.ColumnChanges.Returns(changes);

		var sql = AlterTableGenerator.Generate(statement, "schema");

		const string expected = """
			ALTER TABLE [schema].[table]
			DROP COLUMN [column1], [column2], [column3];
			""";

		Assert.Equal(expected, sql, ignoreLineEndingDifferences: true);
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
