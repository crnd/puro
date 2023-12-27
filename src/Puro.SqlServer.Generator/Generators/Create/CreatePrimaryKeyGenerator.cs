using Puro.SqlServer.Generator.Exceptions;
using Puro.Statements.Create.PrimaryKey;

namespace Puro.SqlServer.Generator.Generators.Create;

/// <summary>
/// T-SQL generator for create primary key statements.
/// </summary>
internal static class CreatePrimaryKeyGenerator
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="statement"></param>
	/// <returns></returns>
	/// <exception cref="IncompleteCreatePrimaryKeyStatementException"></exception>
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
