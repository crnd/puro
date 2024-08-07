﻿using Puro.Statements.Rename.Table;

namespace Puro.Statements.Rename;

internal sealed class RenameBuilder : IRenameBuilder
{
	private readonly List<IMigrationStatement> statements;

	public RenameBuilder(List<IMigrationStatement> statements)
	{
		this.statements = statements;
	}

	public IRenameTableStatement Table(string name)
	{
		var statement = new RenameTableStatement(name);

		statements.Add(statement);

		return statement;
	}
}
