namespace Puro.SqlServer.Runner.Tests.Integration;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
internal sealed class TestPriorityAttribute : Attribute
{
	public int Priority { get; }

	public TestPriorityAttribute(int priority)
	{
		Priority = priority;
	}
}
