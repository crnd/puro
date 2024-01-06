namespace Puro.Exceptions;

public class MigrationNameTooLongException : PuroException
{
	public MigrationNameTooLongException(int maximumLength)
		: base($"Maximum migration name length is {maximumLength} characters.")
	{
	}
}
