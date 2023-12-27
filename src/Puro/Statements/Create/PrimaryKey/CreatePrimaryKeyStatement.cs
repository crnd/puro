using Puro.Exceptions;

namespace Puro.Statements.Create.PrimaryKey;

internal sealed class CreatePrimaryKeyStatement :
	ICreatePrimaryKeyStatement,
	ICreatePrimaryKeyTableStatement,
	ICreatePrimaryKeySchemaStatement,
	ICreatePrimaryKeyMigrationStatement
{
	private readonly List<string> columns = new();

	public string? Schema { get; private set; }

	public string? Table { get; private set; }

	public string PrimaryKey { get; }

	public IReadOnlyList<string> Columns => columns.AsReadOnly();

	public CreatePrimaryKeyStatement(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		PrimaryKey = name;
	}

	public ICreatePrimaryKeyTableStatement OnTable(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		Table = name;

		return this;
	}

	public ICreatePrimaryKeySchemaStatement InSchema(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		Schema = name;

		return this;
	}

	public ICreatePrimaryKeySchemaStatement WithColumn(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		if (columns.Contains(name))
		{
			throw new ConstraintColumnExistsException(PrimaryKey, name);
		}

		columns.Add(name);

		return this;
	}
}
