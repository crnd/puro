using Puro.Exceptions;

namespace Puro.SqlServer.Generator.Exceptions;

public abstract class IncompleteMigrationStatementException : PuroException
{
	protected IncompleteMigrationStatementException(string message)
		: base(message)
	{
	}
}
