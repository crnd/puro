using Puro.Statements.Alter.Table.DropColumn;

namespace Puro.Statements.Alter.Table;

internal sealed class AlterTableStatement :
	IAlterTableStatement,
	IAlterTableSchemaStatement,
	IDropColumnStatement,
	IAlterTableMigrationStatement
{
	private readonly List<(TableColumnChangeType, ITableColumn)> columnChanges = [];

	public string? Schema { get; private set; }

	public string Table { get; }

	public IReadOnlyList<(TableColumnChangeType, ITableColumn)> ColumnChanges => columnChanges.AsReadOnly();

	public AlterTableStatement(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		Table = name;
	}

	public IAlterTableSchemaStatement InSchema(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		Schema = name;

		return this;
	}

	public IAlterTableSchemaStatement DropColumn(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		columnChanges.Add((TableColumnChangeType.Drop, new TableColumn(name)));

		return this;
	}
}
