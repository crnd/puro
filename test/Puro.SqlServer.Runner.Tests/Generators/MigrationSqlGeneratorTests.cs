using Puro.SqlServer.Runner.Generators;
using Puro.SqlServer.Runner.Tests.Extensions;
using Xunit;

namespace Puro.SqlServer.Runner.Tests.Generators;

public class MigrationSqlGeneratorTests
{
	[Fact]
	public void NoMigrations()
	{
		var sql = MigrationSqlGenerator.Generate([]);

		const string expected = $"""
			{Constants.MigrationTableCreation}
			BEGIN TRANSACTION;
			COMMIT TRANSACTION;
			""";

		expected.SqlEqual(sql);
	}
}
