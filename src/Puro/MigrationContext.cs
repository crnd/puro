using Puro.Statements;

namespace Puro;

internal sealed class MigrationContext : IMigrationContext
{
	private readonly List<IMigrationStatement> statements = new();

	public IReadOnlyList<IMigrationStatement> Statements => statements.AsReadOnly();

	public void AddStatement(IMigrationStatement statement) => statements.Add(statement);
}
