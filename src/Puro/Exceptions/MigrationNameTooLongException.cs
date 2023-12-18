namespace Puro.Exceptions;

public class MigrationNameTooLongException : PuroException
{
	public MigrationNameTooLongException(short maximumLength)
		: base($"Maximum migration name length is {maximumLength} characters.")
	{
	}
}
