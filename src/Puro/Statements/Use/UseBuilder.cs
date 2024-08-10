using Puro.Exceptions;

namespace Puro.Statements.Use;

internal sealed class UseBuilder : IUseBuilder
{
	private readonly Migration migration;

	public UseBuilder(Migration migration)
	{
		this.migration = migration;
	}

	public void Schema(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		if (migration.Schema is not null || migration.Statements.Count != 0)
		{
			throw new InvalidUseSchemaStatementException();
		}

		migration.Schema = name;
	}
}
