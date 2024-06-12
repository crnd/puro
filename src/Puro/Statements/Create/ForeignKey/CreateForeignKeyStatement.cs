using Puro.Exceptions;

namespace Puro.Statements.Create.ForeignKey;

internal sealed class CreateForeignKeyStatement :
	ICreateForeignKeyStatement,
	ICreateForeignKeyReferencingTableStatement,
	ICreateForeignKeyReferencingTableSchemaStatement,
	ICreateForeignKeyReferencingColumnStatement,
	ICreateForeignKeyReferencedTableStatement,
	ICreateForeignKeyReferencedTableSchemaStatement,
	ICreateForeignKeyReferencedColumnStatement,
	ICreateForeignKeyMigrationStatement
{
	private readonly List<string> referencingColumns = [];

	private readonly List<string> referencedColumns = [];

	public string ForeignKey { get; }

	public string? ReferencingTable { get; private set; }

	public string? ReferencingTableSchema { get; private set; }

	public IReadOnlyList<string> ReferencingColumns => referencingColumns.AsReadOnly();

	public string? ReferencedTable { get; private set; }

	public string? ReferencedTableSchema { get; private set; }

	public IReadOnlyList<string> ReferencedColumns => referencedColumns.AsReadOnly();

	public OnDeleteBehavior? OnDelete { get; private set; }

	public CreateForeignKeyStatement(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		ForeignKey = name;
	}

	public ICreateForeignKeyReferencingTableStatement FromTable(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		ReferencingTable = name;

		return this;
	}

	public ICreateForeignKeyReferencingTableSchemaStatement FromSchema(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		ReferencingTableSchema = name;

		return this;
	}

	public ICreateForeignKeyReferencingColumnStatement FromColumn(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		if (referencingColumns.Contains(name))
		{
			throw new ConstraintColumnExistsException(ForeignKey, name);
		}

		referencingColumns.Add(name);

		return this;
	}

	public ICreateForeignKeyReferencedTableStatement ToTable(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		ReferencedTable = name;

		return this;
	}

	public ICreateForeignKeyReferencedTableSchemaStatement ToSchema(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		ReferencedTableSchema = name;

		return this;
	}

	public ICreateForeignKeyReferencedColumnStatement ToColumn(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		if (referencedColumns.Contains(name))
		{
			throw new ConstraintColumnExistsException(ForeignKey, name);
		}

		referencedColumns.Add(name);

		return this;
	}

	public void OnDeleteCascade()
	{
		OnDelete = OnDeleteBehavior.Cascade;
	}

	public void OnDeleteRestrict()
	{
		OnDelete = OnDeleteBehavior.Restrict;
	}

	public void OnDeleteSetNull()
	{
		OnDelete = OnDeleteBehavior.SetNull;
	}
}
