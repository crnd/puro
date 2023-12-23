using Puro.Statements.Drop.Index;
using Puro.Statements.Drop.Table;

namespace Puro.Statements.Drop;

internal sealed class DropBuilder : IDropBuilder
{
	private readonly IMigrationContext context;

	public DropBuilder(IMigrationContext context)
	{
		this.context = context;
	}

	public IDropTableStatement Table(string name)
	{
		var statement = new DropTableStatement(name);

		context.AddStatement(statement);

		return statement;
	}

	public IDropIndexStatement Index(string name)
	{
		var statement = new DropIndexStatement(name);

		context.AddStatement(statement);

		return statement;
	}
}
