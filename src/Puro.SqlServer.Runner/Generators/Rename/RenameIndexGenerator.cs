using Puro.SqlServer.Runner.Exceptions;
using Puro.Statements.Rename.Index;

namespace Puro.SqlServer.Runner.Generators.Rename;

/// <summary>
/// T-SQL generator for rename index statements.
/// </summary>
internal static class RenameIndexGenerator
{
	/// <summary>
	/// Generates T-SQL from <paramref name="statement"/> to rename an index.
	/// </summary>
	/// <param name="statement">Migration statement definition.</param>
	/// <param name="schema">Schema name from the migration.</param>
	/// <returns>T-SQL for renaming the defined index.</returns>
	/// <exception cref="IncompleteRenameIndexStatementException">Thrown if <paramref name="statement"/> is not correctly defined.</exception>
	/// <exception cref="ArgumentNullException">Thrown if <paramref name="schema"/> is not defined.</exception>
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
