using Xunit.Abstractions;
using Xunit.Sdk;

namespace Puro.SqlServer.Runner.Tests.Integration;

internal sealed class PriorityTestCaseOrderer : ITestCaseOrderer
{
	public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
		where TTestCase : ITestCase
	{
		var prioritizedTestCases = new SortedDictionary<int, List<TTestCase>>();
		foreach (var testCase in testCases)
		{
			var priority = testCase
				.TestMethod
				.Method
				.GetCustomAttributes(typeof(TestPriorityAttribute))
				.FirstOrDefault()
				?.GetNamedArgument<int>(nameof(TestPriorityAttribute.Priority)) ?? int.MaxValue;

			if (prioritizedTestCases.TryGetValue(priority, out List<TTestCase>? priorityTestCases))
			{
				priorityTestCases.Add(testCase);
			}
			else
			{
				prioritizedTestCases.Add(priority, [testCase]);
			}
		}

		foreach (var prioritizedTestCase in prioritizedTestCases.Values.SelectMany(c => c))
		{
			yield return prioritizedTestCase;
		}
	}
}
