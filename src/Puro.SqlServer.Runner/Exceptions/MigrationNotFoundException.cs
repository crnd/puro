using Puro.Exceptions;

namespace Puro.SqlServer.Runner.Exceptions;

/// <summary>
/// 
/// </summary>
public class MigrationNotFoundException : PuroException
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="migrationName"></param>
	public MigrationNotFoundException(string migrationName)
		: base($"Migration with name {migrationName} was not found")
	{
	}
}
