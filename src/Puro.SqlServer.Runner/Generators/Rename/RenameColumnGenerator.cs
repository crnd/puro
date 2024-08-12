using Puro.SqlServer.Runner.Exceptions;
using Puro.Statements.Rename.Column;

namespace Puro.SqlServer.Runner.Generators.Rename;

/// <summary>
/// T-SQL generator for rename column statements.
/// </summary>
internal static class RenameColumnGenerator
{
	/// <summary>
	/// Generates T-SQL from <paramref name="statement"/> to rename a column.
	/// </summary>
	/// <param name="statement">Migration statement definition.</param>
	/// <param name="schema">Schema name from the migration.</param>
	/// <returns>T-SQL for renaming the defined column.</returns>
	/// <exception cref="IncompleteRenameColumnStatementException">Thrown if <paramref name="statement"/> is not correctly defined.</exception>
	/// <exception cref="ArgumentNullException">Thrown if <paramref name="schema"/> is not defined.</exception>
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
		return $"EXEC sp_rename '[{statement.Schema ?? schema}].[{statement.Table}].[{statement.CurrentName}]', '{statement.NewName}', 'COLUMN';";
	}
}
