using Puro.Statements.Alter.Table;

namespace Puro.Statements.Alter;

internal sealed class AlterBuilder : IAlterBuilder
{
	private readonly List<IMigrationStatement> statements;

	public AlterBuilder(List<IMigrationStatement> statements)
	{
		this.statements = statements;
	}

	public IAlterTableStatement Table(string name)
	{
		var statement = new AlterTableStatement(name);

		statements.Add(statement);

		return statement;
	}
}
