using Puro.Exceptions;

namespace Puro.Statements.Create.Index;

internal sealed class CreateIndexStatement :
	ICreateIndexStatement,
	ICreateIndexTableStatement,
	ICreateIndexTableSchemaStatement,
	ICreateIndexColumnStatement,
	ICreateIndexColumnDirectionStatement,
	ICreateIndexMigrationStatement
{
	private readonly List<IndexColumn> columns = [];

	public string Index { get; }

	public string? Schema { get; internal set; }

	public string? Table { get; internal set; }

	public bool Unique { get; }

	public string? Filter { get; internal set; }

	public IReadOnlyList<IIndexColumn> Columns => columns.AsReadOnly();

	public CreateIndexStatement(string name, bool unique)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		Index = name;
		Unique = unique;
	}

	public ICreateIndexTableStatement OnTable(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		Table = name;

		return this;
	}

	public ICreateIndexTableSchemaStatement InSchema(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		Schema = name;

		return this;
	}

	public ICreateIndexColumnStatement OnColumn(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		if (columns.Select(c => c.Name).Contains(name))
		{
			throw new IndexColumnExistsException(Index, name);
		}

		columns.Add(new IndexColumn(name));

		return this;
	}

	public ICreateIndexColumnDirectionStatement Ascending()
	{
		columns.Last().Descending = false;

		return this;
	}

	public ICreateIndexColumnDirectionStatement Descending()
	{
		columns.Last().Descending = true;

		return this;
	}

	public void WithFilter(string filter)
	{
		if (string.IsNullOrWhiteSpace(filter))
		{
			throw new ArgumentNullException(nameof(filter));
		}

		Filter = filter;
	}
}
