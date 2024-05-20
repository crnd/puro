using Puro.Exceptions;

namespace Puro.Statements.Alter.Table;

internal sealed class AlterTableStatement :
	IAlterTableStatement,
	IAlterTableSchemaStatement,
	IDropColumnStatement,
	IAlterTableMigrationStatement
{
	private readonly List<string> dropColumns = [];

	public string? Schema { get; private set; }

	public string Table { get; }

	public IReadOnlyList<string> DropColumns => dropColumns.AsReadOnly();

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

	public IDropColumnStatement DropColumn(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		if (dropColumns.Contains(name))
		{
			throw new DuplicateDropColumnException(name);
		}

		dropColumns.Add(name);

		return this;
	}
}
