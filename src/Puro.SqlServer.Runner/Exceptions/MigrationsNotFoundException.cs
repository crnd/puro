using Puro.Exceptions;

namespace Puro.SqlServer.Runner.Exceptions;

/// <summary>
/// Exception that is thrown when no migrations were found.
/// </summary>
public class MigrationsNotFoundException : PuroException
{
	/// <summary>
	/// Initializes a new instance of the <see cref="MigrationsNotFoundException"/> class.
	/// </summary>
	public MigrationsNotFoundException()
		: base("No migrations found.")
	{
	}
}
