namespace Puro.Statements.Rename.Index;

internal sealed class RenameIndexStatement :
	IRenameIndexStatement,
	IRenameIndexInTableStatement,
	IRenameIndexInSchemaStatement,
	IRenameIndexMigrationStatement
{
	public string? Schema { get; private set; }

	public string? Table { get; private set; }

	public string CurrentName { get; }

	public string? NewName { get; private set; }

	public RenameIndexStatement(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		CurrentName = name;
	}

	public IRenameIndexInTableStatement InTable(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		Table = name;

		return this;
	}

	public IRenameIndexInSchemaStatement InSchema(string name)
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
