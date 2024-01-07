namespace Puro.Exceptions;

/// <summary>
/// Exception that is thrown when a constraint includes a column more than once.
/// </summary>
public class ConstraintColumnExistsException : PuroException
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ConstraintColumnExistsException"/> class.
	/// </summary>
	/// <param name="constraintName">Name of the constraint that contains duplicate columns.</param>
	/// <param name="columnName">Name of the column that is duplicated.</param>
	public ConstraintColumnExistsException(string constraintName, string columnName)
		: base($"Constraint {constraintName} contains column {columnName} more than once.")
	{
	}
}
