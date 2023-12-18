namespace Puro.Statements.Alter;

internal sealed class AlterBuilder : IAlterBuilder
{
	private readonly IMigrationContext context;

	public AlterBuilder(IMigrationContext context)
	{
		this.context = context;
	}
}
