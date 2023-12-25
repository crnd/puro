namespace Puro.Statements.Drop.Constraint;

internal sealed class DropConstraintStatement :
	IDropConstraintStatement,
	IDropConstraintFromTableStatement,
	IDropConstraintMigrationStatement
{
	public string Constraint { get; }

	public string? Table { get; private set; }

	public string? Schema { get; private set; }

	public DropConstraintStatement(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		Constraint = name;
	}

	public IDropConstraintFromTableStatement FromTable(string name)
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
