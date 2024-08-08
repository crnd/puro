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
	/// <returns>T-SQL for renaming the defined table.</returns>
	/// <exception cref="IncompleteRenameTableStatementException">Thrown if <paramref name="statement"/> is not correctly defined.</exception>
	public static string Generate(IRenameTableMigrationStatement statement)
	{
		if (statement.Schema is null || statement.NewName is null)
		{
			throw new IncompleteRenameTableStatementException(statement.CurrentName);
		}

		// New name for the table cannot be enclosed in square brackets as
		// then the square brackets would be included in the new table name.
		return $"EXEC sp_rename '[{statement.Schema}].[{statement.CurrentName}]', '{statement.NewName}';";
	}
}
