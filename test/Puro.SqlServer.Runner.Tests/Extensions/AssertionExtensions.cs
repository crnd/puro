﻿using Xunit;

namespace Puro.SqlServer.Runner.Tests.Extensions;

/// <summary>
/// Extension methods for assertions.
/// </summary>
internal static class AssertionExtensions
{
	/// <summary>
	/// Verifies that two SQL string are effectively equivalent. Ignores line changes,
	/// empty rows and white space at the beginning and the end of rows.
	/// </summary>
	/// <param name="expected">The expected SQL string value.</param>
	/// <param name="actual">The actual SQL string value.</param>
	public static void SqlEqual(this string expected, string actual)
	{
		Assert.Equal(Normalize(expected), Normalize(actual));
	}

	private static readonly string[] separator = ["\r", "\n"];

	private static string Normalize(string input)
	{
		var rows = input.Split(
			separator,
			StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

		return string.Join(' ', rows)
			.Replace(" ( ", " (")
			.Replace(" );", ");");
	}
}
