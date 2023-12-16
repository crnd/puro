using Puro.Exceptions;
using Xunit;

namespace Puro.Tests;

public class ExceptionTests
{
	[Fact]
	public void AllExceptionsBasedOnPuroException()
	{
		var exceptions = typeof(IMigration)
			.Assembly
			.GetTypes()
			.Where(t => typeof(Exception).IsAssignableFrom(t))
			.ToArray();

		Assert.All(exceptions, ex => Assert.True(ex.IsAssignableTo(typeof(PuroException))));
	}
}
