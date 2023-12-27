using Puro.SqlServer.Generator.Statements.Create;
using Puro.SqlServer.Generator.Statements.Drop;
using Puro.Statements;
using Puro.Statements.Create.PrimaryKey;
using Puro.Statements.Drop.Constraint;
using Puro.Statements.Drop.Index;
using Puro.Statements.Drop.Table;
using Puro.Statements.Sql;

namespace Puro.SqlServer.Generator;

public static class SqlGenerator
{
	private static string ConvertStatementToSql(IMigrationStatement migrationStatement)
	{
		return migrationStatement switch
		{
			ICreatePrimaryKeyMigrationStatement statement => CreatePrimaryKeyGenerator.Generate(statement),
			IDropIndexMigrationStatement statement => DropIndexGenerator.Generate(statement),
			IDropTableMigrationStatement statement => DropTableGenerator.Generate(statement),
			IDropConstraintMigrationStatement statement => DropConstraintGenerator.Generate(statement),
			ISqlMigrationStatement statement => statement.Sql,
			_ => throw new NotImplementedException(),
		};
	}
}
