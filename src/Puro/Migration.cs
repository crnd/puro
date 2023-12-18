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
	private readonly IMigrationContext context = new MigrationContext();

	/// <summary>
	/// Starts building a new statement for creating a new item.
	/// </summary>
	protected ICreateBuilder Create => new CreateBuilder(context);

	/// <summary>
	/// Starts building a new statement for editing an existing item.
	/// </summary>
	protected IAlterBuilder Alter => new AlterBuilder(context);

	/// <summary>
	/// Starts building a new statement for deleting an existing item.
	/// </summary>
	protected IDropBuilder Drop => new DropBuilder(context);

	/// <summary>
	/// Creates a raw SQL statement.
	/// </summary>
	/// <param name="statement">Raw SQL statement.</param>
	protected void Sql(string statement) => context.AddStatement(new SqlStatement(statement));

	public IReadOnlyList<IMigrationStatement> Statements => context.Statements;

	public string Name => GetMigrationName();

	public abstract void Up();

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
