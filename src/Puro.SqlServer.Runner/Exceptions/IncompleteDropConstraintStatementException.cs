namespace Puro.SqlServer.Runner.Exceptions;

/// <summary>
/// Exception that is thrown when drop constraint migration statement is incomplete.
/// </summary>
public class IncompleteDropConstraintStatementException : IncompleteMigrationStatementException
{
	/// <summary>
	/// Initializes a new instance of <see cref="IncompleteDropConstraintStatementException"/> class.
	/// </summary>
	/// <param name="constraint">Name of the constraint.</param>
	public IncompleteDropConstraintStatementException(string constraint)
		: base($"Drop constraint statement for constraint {constraint} is incomplete.")
	{
	}
}
