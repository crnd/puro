namespace Puro.Statements.Rename.Table;

internal sealed class RenameTableStatement :
	IRenameTableStatement,
	IRenameTableInSchemaStatement,
	IRenameTableMigrationStatement
{
	public string? Schema { get; private set; }

	public string CurrentName { get; }

	public string? NewName { get; private set; }

	public RenameTableStatement(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		CurrentName = name;
	}

	public IRenameTableInSchemaStatement InSchema(string name)
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
