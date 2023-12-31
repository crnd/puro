namespace Puro.Statements.Alter;

internal sealed class AlterBuilder : IAlterBuilder
{
	private readonly List<IMigrationStatement> statements;

	public AlterBuilder(List<IMigrationStatement> statements)
	{
		this.statements = statements;
	}
}
