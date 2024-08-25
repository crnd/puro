using Puro.SqlServer.Runner.Exceptions;
using Puro.Statements.Rename.Index;

namespace Puro.SqlServer.Runner.Generators.Rename;

internal static class RenameIndexGenerator
{
	public static string Generate(IRenameIndexMigrationStatement statement, string schema)
	{
		if (statement.Table is null || statement.NewName is null)
		{
			throw new IncompleteRenameIndexStatementException(statement.CurrentName);
		}

		if (string.IsNullOrWhiteSpace(schema))
		{
			throw new ArgumentNullException(nameof(schema));
		}

		// New name for the index cannot be enclosed in square brackets as
		// then the square brackets would be included in the new index name.
		return $"EXEC sp_rename N'[{statement.Schema ?? schema}].[{statement.Table}].[{statement.CurrentName}]', N'{statement.NewName}', N'INDEX';";
	}
}
