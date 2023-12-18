using Puro.Statements;

namespace Puro;

internal interface IMigrationContext
{
	public IReadOnlyList<IMigrationStatement> Statements { get; }

	public void AddStatement(IMigrationStatement statement);
}
