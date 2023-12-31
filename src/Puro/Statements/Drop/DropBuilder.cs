using Puro.Statements.Drop.Constraint;
using Puro.Statements.Drop.Index;
using Puro.Statements.Drop.Table;

namespace Puro.Statements.Drop;

internal sealed class DropBuilder : IDropBuilder
{
	private readonly List<IMigrationStatement> statements;

	public DropBuilder(List<IMigrationStatement> statements)
	{
		this.statements = statements;
	}

	public IDropTableStatement Table(string name)
	{
		var statement = new DropTableStatement(name);

		statements.Add(statement);

		return statement;
	}

	public IDropIndexStatement Index(string name)
	{
		var statement = new DropIndexStatement(name);

		statements.Add(statement);

		return statement;
	}

	public IDropConstraintStatement Constraint(string name)
	{
		var statement = new DropConstraintStatement(name);

		statements.Add(statement);

		return statement;
	}
}
