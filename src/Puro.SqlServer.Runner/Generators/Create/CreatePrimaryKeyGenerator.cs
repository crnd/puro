using Puro.SqlServer.Runner.Exceptions;
using Puro.Statements.Create.PrimaryKey;

namespace Puro.SqlServer.Runner.Generators.Create;

/// <summary>
/// T-SQL generator for create primary key statements.
/// </summary>
internal static class CreatePrimaryKeyGenerator
{
	/// <summary>
	/// Generates T-SQL from <paramref name="statement"/> to create a primary key constraint.
	/// </summary>
	/// <param name="statement">Migration statement definition.</param>
	/// <param name="schema">Schema name from the migration.</param>
	/// <returns>T-SQL for creating a primary key constraint.</returns>
	/// <exception cref="IncompleteCreatePrimaryKeyStatementException">Thrown if <paramref name="statement"/> is not correctly defined.</exception>
	public static string Generate(ICreatePrimaryKeyMigrationStatement statement, string schema)
	{
		if (statement.Table is null || statement.Columns.Count == 0)
		{
			throw new IncompleteCreatePrimaryKeyStatementException(statement.PrimaryKey);
		}

		if (string.IsNullOrWhiteSpace(schema))
		{
			throw new ArgumentNullException(nameof(schema));
		}

		return $"ALTER TABLE [{statement.Schema ?? schema}].[{statement.Table}] ADD CONSTRAINT [{statement.PrimaryKey}] PRIMARY KEY CLUSTERED ([{GetColumns(statement.Columns)}]);";
	}

	private static string GetColumns(IReadOnlyList<string> columns) => string.Join("], [", columns);
}
