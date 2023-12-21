namespace Puro.Statements.Drop.Table;

internal sealed class DropTableStatement :
	IDropTableStatement,
	IDropTableMigrationStatement
{
	public string? Schema { get; private set; }

	public string Table { get; }

	public DropTableStatement(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		Table = name;
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