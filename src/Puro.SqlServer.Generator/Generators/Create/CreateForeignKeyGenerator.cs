﻿using Puro.SqlServer.Generator.Exceptions;
using Puro.Statements;
using Puro.Statements.Create.ForeignKey;

namespace Puro.SqlServer.Generator.Generators.Create;

/// <summary>
/// T-SQL generator for create foreign key statements.
/// </summary>
internal static class CreateForeignKeyGenerator
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="statement"></param>
	/// <returns></returns>
	/// <exception cref="IncompleteCreateForeignKeyStatementException"></exception>
	public static string Generate(ICreateForeignKeyMigrationStatement statement)
	{
		if (!IsComplete(statement))
		{
			throw new IncompleteCreateForeignKeyStatementException(statement.ForeignKey);
		}

		return @$"
ALTER TABLE [{statement.ReferencingTableSchema}].[{statement.ReferencingTable}]
	ADD CONSTRAINT [{statement.ForeignKey}] FOREIGN KEY ([{GetColumns(statement.ReferencingColumns)}])
	REFERENCES [{statement.ReferencedTableSchema}].[{statement.ReferencedTable}] ([{GetColumns(statement.ReferencedColumns)}])
	ON DELETE {ConvertOnDeleteToString(statement.OnDelete!.Value)};";
	}

	private static bool IsComplete(ICreateForeignKeyMigrationStatement statement)
	{
		if (statement.ReferencingTableSchema is null || statement.ReferencingTable is null)
		{
			return false;
		}

		if (statement.ReferencedTableSchema is null || statement.ReferencedTable is null)
		{
			return false;
		}

		if (statement.OnDelete is null)
		{
			return false;
		}

		if (statement.ReferencedColumns.Count == 0)
		{
			return false;
		}

		return statement.ReferencingColumns.Count == statement.ReferencedColumns.Count;
	}

	private static string GetColumns(IReadOnlyList<string> columns) => string.Join("], [", columns);

	private static string ConvertOnDeleteToString(OnDeleteBehavior behavior)
	{
		return behavior switch
		{
			OnDeleteBehavior.Cascade => "CASCADE",
			OnDeleteBehavior.Restrict => "NO ACTION",
			OnDeleteBehavior.SetNull => "SET NULL",
			_ => throw new ArgumentOutOfRangeException(nameof(behavior)),
		};
	}
}