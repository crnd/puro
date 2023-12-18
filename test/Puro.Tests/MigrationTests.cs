using Puro.Exceptions;
using Xunit;

namespace Puro.Tests;

public class MigrationTests
{
	[Fact]
	public void GettingMigrationNameWithoutNameAttributeThrows()
	{
		var migration = new NamelessMigration();

		var exception = Assert.Throws<NameAttributeNotFoundException>(() => migration.Name);
		Assert.NotNull(exception);
		Assert.StartsWith("Migration NamelessMigration ", exception.Message);
	}

	private sealed class NamelessMigration : UpMigration
	{
		public override void Up() { }
	}

	[Fact]
	public void MigrationNameReturned()
	{
		var migration = new NameMigration();

		Assert.Equal("TestMigration", migration.Name);
	}

	[MigrationName("TestMigration")]
	private sealed class NameMigration : UpMigration
	{
		public override void Up() { }
	}

	[Fact]
	public void EmptyCollectionReturnedFromMigrationWithNoStatements()
	{
		var migration = new NamelessMigration();

		Assert.Empty(migration.Statements);
	}
}
