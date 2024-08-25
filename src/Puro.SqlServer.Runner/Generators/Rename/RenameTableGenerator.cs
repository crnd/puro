using Puro.SqlServer.Runner.Exceptions;
using Puro.Statements.Rename.Table;

namespace Puro.SqlServer.Runner.Generators.Rename;

internal static class RenameTableGenerator
{
	public static string Generate(IRenameTableMigrationStatement statement, string schema)
	{
		if (statement.NewName is null)
		{
			throw new IncompleteRenameTableStatementException(statement.CurrentName);
		}

		if (string.IsNullOrWhiteSpace(schema))
		{
			throw new ArgumentNullException(nameof(schema));
		}

		// New name for the table cannot be enclosed in square brackets as
		// then the square brackets would be included in the new table name.
		return $"EXEC sp_rename N'[{statement.Schema ?? schema}].[{statement.CurrentName}]', N'{statement.NewName}';";
	}
}
