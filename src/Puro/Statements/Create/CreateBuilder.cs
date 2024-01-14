using Puro.Statements.Create.ForeignKey;
using Puro.Statements.Create.PrimaryKey;
using Puro.Statements.Create.Table;

namespace Puro.Statements.Create;

internal sealed class CreateBuilder : ICreateBuilder
{
	private readonly List<IMigrationStatement> statements;

	public CreateBuilder(List<IMigrationStatement> statements)
	{
		this.statements = statements;
	}

	public ICreatePrimaryKeyStatement PrimaryKey(string name)
	{
		var statement = new CreatePrimaryKeyStatement(name);

		statements.Add(statement);

		return statement;
	}

	public ICreateForeignKeyStatement ForeignKey(string name)
	{
		var statement = new CreateForeignKeyStatement(name);

		statements.Add(statement);

		return statement;
	}

	public ICreateTableStatement Table(string name)
	{
		var statement = new CreateTableStatement(name);

		statements.Add(statement);

		return statement;
	}
}
