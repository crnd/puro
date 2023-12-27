using Puro.Statements.Create.PrimaryKey;

namespace Puro.Statements.Create;

internal sealed class CreateBuilder : ICreateBuilder
{
	private readonly IMigrationContext context;

	public CreateBuilder(IMigrationContext context)
	{
		this.context = context;
	}

	public ICreatePrimaryKeyStatement PrimaryKey(string name)
	{
		var statement = new CreatePrimaryKeyStatement(name);

		context.AddStatement(statement);

		return statement;
	}
}
