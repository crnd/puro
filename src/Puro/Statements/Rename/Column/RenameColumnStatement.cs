namespace Puro.Statements.Rename.Column;

internal sealed class RenameColumnStatement :
	IRenameColumnStatement,
	IRenameColumnInTableStatement,
	IRenameColumnInSchemaStatement,
	IRenameColumnMigrationStatement
{
	public string? Schema { get; private set; }

	public string? Table { get; private set; }

	public string CurrentName { get; }

	public string? NewName { get; private set; }

	public RenameColumnStatement(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		CurrentName = name;
	}

	public IRenameColumnInTableStatement InTable(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		Table = name;

		return this;
	}

	public IRenameColumnInSchemaStatement InSchema(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		Schema = name;

		return this;
	}

	public void To(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		NewName = name;
	}
}
