namespace Puro.Statements.Drop;

internal sealed class DropBuilder : IDropBuilder
{
	private readonly IMigrationContext context;

	public DropBuilder(IMigrationContext context)
	{
		this.context = context;
	}
}
