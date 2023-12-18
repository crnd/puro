namespace Puro.Statements.Create;

internal sealed class CreateBuilder : ICreateBuilder
{
	private readonly IMigrationContext context;

	public CreateBuilder(IMigrationContext context)
	{
		this.context = context;
	}
}
