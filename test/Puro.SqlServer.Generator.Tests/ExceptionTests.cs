using Puro.Exceptions;
using Puro.SqlServer.Generator.Exceptions;
using Xunit;

namespace Puro.SqlServer.Generator.Tests;

public class ExceptionTests
{
	[Fact]
	public void AllExceptionsBasedOnPuroException()
	{
		var exceptions = typeof(IncompleteMigrationStatementException)
			.Assembly
			.GetTypes()
			.Where(t => typeof(Exception).IsAssignableFrom(t))
			.ToArray();

		Assert.All(exceptions, ex => Assert.True(ex.IsAssignableTo(typeof(PuroException))));
	}
}
