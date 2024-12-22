using Puro.Exceptions;
using Puro.Statements;
using Puro.Statements.Alter;
using Puro.Statements.Create;
using Puro.Statements.Drop;
using Puro.Statements.Rename;
using Puro.Statements.Sql;
using Puro.Statements.Use;
using System.Reflection;

namespace Puro;

/// <summary>
/// Base class for a migration.
/// </summary>
public abstract class Migration
{
	private readonly List<IMigrationStatement> statements = [];

	/// <summary>
	/// Starts building a new statement for creating a new item.
	/// </summary>
	protected ICreateBuilder Create => new CreateBuilder(statements);

	/// <summary>
	/// Starts building a new statement for editing an existing item.
	/// </summary>
	protected IAlterBuilder Alter => new AlterBuilder(statements);

	/// <summary>
	/// Starts building a new statement for deleting an existing item.
	/// </summary>
	protected IDropBuilder Drop => new DropBuilder(statements);

	/// <summary>
	/// Starts building a new statement for renaming an existing item.
	/// </summary>
	protected IRenameBuilder Rename => new RenameBuilder(statements);

	/// <summary>
	/// Starts defining a new use statement.
	/// </summary>
	protected IUseBuilder Use => new UseBuilder(this);

	/// <summary>
	/// Creates a raw SQL statement.
	/// </summary>
	/// <param name="statement">Raw SQL statement.</param>
	protected void Sql(string statement) => statements.Add(new SqlStatement(statement));

	/// <summary>
	/// Returns the statements that have been processed after running the <see cref="Up"/> or <see cref="Down"/> method.
	/// </summary>
	public IReadOnlyList<IMigrationStatement> Statements => statements.AsReadOnly();

	/// <summary>
	/// Default schema for the migration.
	/// </summary>
	public string? Schema { get; internal set; }

	/// <summary>
	/// Gets the name of the migration that has been defined with <see cref="MigrationNameAttribute"/>.
	/// </summary>
	/// <exception cref="NameAttributeNotFoundException">Thrown if no name has been defined with <see cref="MigrationNameAttribute"/>.</exception>
	public string Name => GetMigrationName();

	/// <summary>
	/// Processes the defined migration statements.
	/// </summary>
	public abstract void Up();

	private string GetMigrationName()
	{
		var type = GetType();
		var nameAttribute = type.GetCustomAttribute<MigrationNameAttribute>();
		if (nameAttribute is null)
		{
			throw new NameAttributeNotFoundException(type);
		}

		return nameAttribute.Name;
	}
}
