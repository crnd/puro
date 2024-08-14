using Puro.SqlServer.Runner.Exceptions;
using Puro.Statements.Rename.Table;

namespace Puro.SqlServer.Runner.Generators.Rename;

/// <summary>
/// T-SQL generator for rename table statements.
/// </summary>
internal static class RenameTableGenerator
{
	/// <summary>
	/// Generates T-SQL from <paramref name="statement"/> to rename a table.
	/// </summary>
	/// <param name="statement">Migration statement definition.</param>
	/// <param name="schema">Schema name from the migration.</param>
	/// <returns>T-SQL for renaming the defined table.</returns>
	/// <exception cref="IncompleteRenameTableStatementException">Thrown if <paramref name="statement"/> is not correctly defined.</exception>
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
