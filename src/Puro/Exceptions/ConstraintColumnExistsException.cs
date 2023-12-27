namespace Puro.Exceptions;

public class ConstraintColumnExistsException : PuroException
{
	public ConstraintColumnExistsException(string constraintName, string columnName)
		: base($"Constraint {constraintName} contains column {columnName} more than once.")
	{
	}
}
