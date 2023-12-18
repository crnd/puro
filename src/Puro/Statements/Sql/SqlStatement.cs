namespace Puro.Statements.Sql;

internal sealed class SqlStatement : ISqlMigrationStatement
{
	public string Sql { get; }

	public SqlStatement(string sql)
	{
		if (string.IsNullOrWhiteSpace(sql))
		{
			throw new ArgumentNullException(nameof(sql));
		}

		Sql = sql;
	}
}
