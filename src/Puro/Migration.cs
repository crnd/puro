using Puro.Exceptions;
using Puro.Statements;
using Puro.Statements.Alter;
using Puro.Statements.Create;
using Puro.Statements.Drop;
using Puro.Statements.Sql;
using System.Reflection;

namespace Puro;

/// <summary>
/// Base class for a migration.
/// </summary>
public abstract class Migration
{
	private readonly List<IMigrationStatement> statements = new();

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
	/// Creates a raw SQL statement.
	/// </summary>
	/// <param name="statement">Raw SQL statement.</param>
	protected void Sql(string statement) => statements.Add(new SqlStatement(statement));

	/// <summary>
	/// Returns the statements that have been processed after running the <see cref="Up"/> or <see cref="Down"/> method.
	/// </summary>
	public IReadOnlyList<IMigrationStatement> Statements => statements.AsReadOnly();

	/// <summary>
	/// Gets the name of the migration that has been defined with <see cref="MigrationNameAttribute"/>.
	/// </summary>
	/// <exception cref="NameAttributeNotFoundException">Thrown if no name has been defined with <see cref="MigrationNameAttribute"/>.</exception>
	public string Name => GetMigrationName();

	/// <summary>
	/// Processes the migration statements that are defined for the up direction.
	/// </summary>
	public abstract void Up();

	/// <summary>
	/// Processes the migration statements that are defined for the down direction.
	/// </summary>
	public abstract void Down();

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
