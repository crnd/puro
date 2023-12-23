namespace Puro.Statements.Drop.Index;

internal sealed class DropIndexStatement :
	IDropIndexStatement,
	IDropIndexFromTableStatement,
	IDropIndexMigrationStatement
{
	public string Index { get; }

	public string? Table { get; private set; }

	public string? Schema { get; private set; }

	public DropIndexStatement(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		Index = name;
	}

	public IDropIndexFromTableStatement FromTable(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		Table = name;

		return this;
	}

	public void InSchema(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		Schema = name;
	}
}
