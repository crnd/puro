using Puro.Exceptions;
using Xunit;

namespace Puro.Tests;

public class MigrationNameAttributeTests
{
	[Fact]
	public void TooLongNameThrows()
	{
		var name = string.Concat(Enumerable.Repeat('a', 155));

		Assert.Throws<MigrationNameTooLongException>(() => new MigrationNameAttribute(name));
	}

	[Fact]
	public void NullNameThrows()
	{
		Assert.Throws<ArgumentNullException>(() => new MigrationNameAttribute(null!));
	}

	[Fact]
	public void EmptyNameThrows()
	{
		Assert.Throws<ArgumentNullException>(() => new MigrationNameAttribute(string.Empty));
	}

	[Fact]
	public void WhiteSpaceNameThrows()
	{
		Assert.Throws<ArgumentNullException>(() => new MigrationNameAttribute("     "));
	}

	[Fact]
	public void DefinedNameReturned()
	{
		const string name = "TestMigrationName";
		var attribute = new MigrationNameAttribute(name);

		Assert.Equal(name, attribute.Name);
	}
}
