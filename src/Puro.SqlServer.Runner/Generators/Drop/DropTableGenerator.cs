using Puro.Statements.Drop.Table;

namespace Puro.SqlServer.Runner.Generators.Drop;

internal static class DropTableGenerator
{
	public static string Generate(IDropTableMigrationStatement statement, string schema)
	{
		if (string.IsNullOrWhiteSpace(schema))
		{
			throw new ArgumentNullException(nameof(schema));
		}

		return $"DROP TABLE [{statement.Schema ?? schema}].[{statement.Table}];";
	}
}
