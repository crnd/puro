using Puro.Exceptions;

namespace Puro.SqlServer.Runner.Exceptions;

/// <summary>
/// 
/// </summary>
public class UnsupportedMigrationStatementException : PuroException
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="migrationStatementType"></param>
	public UnsupportedMigrationStatementException(Type migrationStatementType)
		: base($"Migration statement type {migrationStatementType} is not supported.")
	{
	}
}
