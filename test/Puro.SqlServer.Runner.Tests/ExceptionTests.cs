using Puro.Exceptions;
using Xunit;

namespace Puro.SqlServer.Runner.Tests;

public class ExceptionTests
{
	[Fact]
	public void AllExceptionsBasedOnPuroException()
	{
		var exceptions = typeof(Program)
			.Assembly
			.GetTypes()
			.Where(t => typeof(Exception).IsAssignableFrom(t))
			.ToArray();

		Assert.All(exceptions, ex => Assert.True(ex.IsAssignableTo(typeof(PuroException))));
	}
}