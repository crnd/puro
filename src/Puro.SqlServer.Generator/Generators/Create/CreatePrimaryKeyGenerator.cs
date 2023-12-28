using Puro.SqlServer.Generator.Exceptions;
using Puro.Statements.Create.PrimaryKey;

namespace Puro.SqlServer.Generator.Generators.Create;

/// <summary>
/// T-SQL generator for create primary key statements.
/// </summary>
internal static class CreatePrimaryKeyGenerator
{
	/// <summary>
	/// Generates T-SQL from <paramref name="statement"/> to create a primary key constraint.
	/// </summary>
	/// <param name="statement">Migration statement definition.</param>
	/// <returns>T-SQL for creating a primary key constraint.</returns>
	/// <exception cref="IncompleteCreatePrimaryKeyStatementException">Thrown if <paramref name="statement"/> is not correctly defined.</exception>
	public static string Generate(ICreatePrimaryKeyMigrationStatement statement)
	{
		if (statement.Schema is null || statement.Table is null || statement.Columns.Count == 0)
		{
			throw new IncompleteCreatePrimaryKeyStatementException(statement.PrimaryKey);
		}

		return $"ALTER TABLE [{statement.Schema}].[{statement.Table}] ADD CONSTRAINT [{statement.PrimaryKey}] PRIMARY KEY CLUSTERED ([{GetColumns(statement.Columns)}]);";
	}

	private static string GetColumns(IReadOnlyList<string> columns) => string.Join("], [", columns);
}
