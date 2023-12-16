namespace Puro.Exceptions;

public abstract class PuroException : Exception
{
	protected PuroException(string message)
		: base(message)
	{
	}
}
