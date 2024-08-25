using Puro.SqlServer.Runner.Exceptions;
using Puro.Statements.Rename.Column;

namespace Puro.SqlServer.Runner.Generators.Rename;

internal static class RenameColumnGenerator
{
	public static string Generate(IRenameColumnMigrationStatement statement, string schema)
	{
		if (statement.Table is null || statement.NewName is null)
		{
			throw new IncompleteRenameColumnStatementException(statement.CurrentName);
		}

		if (string.IsNullOrWhiteSpace(schema))
		{
			throw new ArgumentNullException(nameof(schema));
		}

		// New name for the column cannot be enclosed in square brackets as
		// then the square brackets would be included in the new column name.
		return $"EXEC sp_rename N'[{statement.Schema ?? schema}].[{statement.Table}].[{statement.CurrentName}]', N'{statement.NewName}', N'COLUMN';";
	}
}
