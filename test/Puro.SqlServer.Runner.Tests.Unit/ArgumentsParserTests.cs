using Puro.SqlServer.Runner.Exceptions;
using Xunit;

namespace Puro.SqlServer.Runner.Tests.Unit;

public class ArgumentsParserTests
{
	[Fact]
	public void NoArgumentsThrows()
	{
		Assert.Throws<InvalidRunnerArgumentsException>(() => ArgumentsParser.Parse([]));
	}

	[Fact]
	public void AssemblyShortFormNoValueThrows()
	{
		string[] arguments = ["-a"];

		Assert.Throws<InvalidRunnerArgumentsException>(() => ArgumentsParser.Parse(arguments));
	}

	[Fact]
	public void AssemblyShortFormSetsAssembly()
	{
		string[] arguments = ["-a", "path/to/assembly.dll"];
		var settings = ArgumentsParser.Parse(arguments);

		Assert.Equal("path/to/assembly.dll", settings.AssemblyLocation);
	}

	[Fact]
	public void AssemblyLongFormNoValueThrows()
	{
		string[] arguments = ["--assembly"];

		Assert.Throws<InvalidRunnerArgumentsException>(() => ArgumentsParser.Parse(arguments));
	}

	[Fact]
	public void AssemblyLongFormSetsAssembly()
	{
		string[] arguments = ["--assembly", "path/to/assembly.dll"];
		var settings = ArgumentsParser.Parse(arguments);

		Assert.Equal("path/to/assembly.dll", settings.AssemblyLocation);
	}

	[Fact]
	public void FromShortFormNoValueThrows()
	{
		string[] arguments = ["-a", "path/to/assembly.dll", "-f"];

		Assert.Throws<InvalidRunnerArgumentsException>(() => ArgumentsParser.Parse(arguments));
	}

	[Fact]
	public void FromShortFormSetsFromMigration()
	{
		string[] arguments = ["-a", "path/to/assembly.dll", "-f", "1_FirstMigration"];
		var settings = ArgumentsParser.Parse(arguments);

		Assert.Equal("1_FirstMigration", settings.FromMigration);
	}

	[Fact]
	public void FromLongFormNoValueThrows()
	{
		string[] arguments = ["-a", "path/to/assembly.dll", "--from-migration"];

		Assert.Throws<InvalidRunnerArgumentsException>(() => ArgumentsParser.Parse(arguments));
	}

	[Fact]
	public void FromLongFormSetsFromMigration()
	{
		string[] arguments = ["-a", "path/to/assembly.dll", "--from-migration", "1_FirstMigration"];
		var settings = ArgumentsParser.Parse(arguments);

		Assert.Equal("1_FirstMigration", settings.FromMigration);
	}

	[Fact]
	public void ToShortFormNoValueThrows()
	{
		string[] arguments = ["-a", "path/to/assembly.dll", "-f", "1_FirstMigration", "-t"];

		Assert.Throws<InvalidRunnerArgumentsException>(() => ArgumentsParser.Parse(arguments));
	}

	[Fact]
	public void ToShortFormSetsFromMigration()
	{
		string[] arguments = ["-a", "path/to/assembly.dll", "-f", "1_FirstMigration", "-t", "3_ThirdMigration"];
		var settings = ArgumentsParser.Parse(arguments);

		Assert.Equal("3_ThirdMigration", settings.ToMigration);
	}

	[Fact]
	public void ToLongFormNoValueThrows()
	{
		string[] arguments = ["-a", "path/to/assembly.dll", "-f", "1_FirstMigration", "--to-migration"];

		Assert.Throws<InvalidRunnerArgumentsException>(() => ArgumentsParser.Parse(arguments));
	}

	[Fact]
	public void ToLongFormSetsFromMigration()
	{
		string[] arguments = ["-a", "path/to/assembly.dll", "-f", "1_FirstMigration", "--to-migration", "3_ThirdMigration"];
		var settings = ArgumentsParser.Parse(arguments);

		Assert.Equal("3_ThirdMigration", settings.ToMigration);
	}

	[Fact]
	public void ConnectionShortFormNoValueThrows()
	{
		string[] arguments = ["-a", "path/to/assembly.dll", "-f", "1_FirstMigration", "-t", "3_ThirdMigration", "-c"];

		Assert.Throws<InvalidRunnerArgumentsException>(() => ArgumentsParser.Parse(arguments));
	}

	[Fact]
	public void ConnectionShortFormSetsFromMigration()
	{
		string[] arguments = ["-a", "path/to/assembly.dll", "-f", "1_FirstMigration", "-t", "3_ThirdMigration", "-c", "localhost:12345"];
		var settings = ArgumentsParser.Parse(arguments);

		Assert.Equal("localhost:12345", settings.ConnectionString);
	}

	[Fact]
	public void ConnectionLongFormNoValueThrows()
	{
		string[] arguments = ["-a", "path/to/assembly.dll", "-f", "1_FirstMigration", "-t", "3_ThirdMigration", "--connection"];

		Assert.Throws<InvalidRunnerArgumentsException>(() => ArgumentsParser.Parse(arguments));
	}

	[Fact]
	public void ConnectionLongFormSetsFromMigration()
	{
		string[] arguments = ["-a", "path/to/assembly.dll", "-f", "1_FirstMigration", "-t", "3_ThirdMigration", "--connection", "localhost:12345"];
		var settings = ArgumentsParser.Parse(arguments);

		Assert.Equal("localhost:12345", settings.ConnectionString);
	}

	[Fact]
	public void SettingAssemblyTwiceThrows()
	{
		string[] arguments = ["-a", "path/to/assembly.dll", "-a", "path/to/assembly.dll"];

		Assert.Throws<InvalidRunnerArgumentsException>(() => ArgumentsParser.Parse(arguments));
	}

	[Fact]
	public void SettingFromMigrationTwiceThrows()
	{
		string[] arguments = ["-a", "path/to/assembly.dll", "-f", "1_FirstMigration", "-f", "1_FirstMigration"];

		Assert.Throws<InvalidRunnerArgumentsException>(() => ArgumentsParser.Parse(arguments));
	}

	[Fact]
	public void SettingToMigrationTwiceThrows()
	{
		string[] arguments = ["-a", "path/to/assembly.dll", "-t", "3_ThirdMigration", "-t", "3_ThirdMigration"];

		Assert.Throws<InvalidRunnerArgumentsException>(() => ArgumentsParser.Parse(arguments));
	}

	[Fact]
	public void SettingConnectionStringTwiceThrows()
	{
		string[] arguments = ["-a", "path/to/assembly.dll", "-c", "localhost:12345", "-c", "localhost:12345"];

		Assert.Throws<InvalidRunnerArgumentsException>(() => ArgumentsParser.Parse(arguments));
	}

	[Theory]
	[InlineData("-a", "a", "-f", "f", "-t", "t", "-c", "c")]
	[InlineData("-a", "a", "-t", "t", "-f", "f", "-c", "c")]
	[InlineData("-a", "a", "-c", "c", "-t", "t", "-f", "f")]
	[InlineData("-a", "a", "-t", "t", "-c", "c", "-f", "f")]
	[InlineData("-a", "a", "-f", "f", "-c", "c", "-t", "t")]
	[InlineData("-a", "a", "-c", "c", "-f", "f", "-t", "t")]
	public void ValidArgumentOrder(params string[] arguments)
	{
		var settings = ArgumentsParser.Parse(arguments);

		Assert.Equal("a", settings.AssemblyLocation);
		Assert.Equal("f", settings.FromMigration);
		Assert.Equal("t", settings.ToMigration);
		Assert.Equal("c", settings.ConnectionString);
	}

	[Theory]
	[InlineData("-f", "f", "-a", "a", "-t", "t", "-c", "c")]
	[InlineData("-t", "t", "-a", "a", "-f", "f", "-c", "c")]
	[InlineData("-f", "f", "-t", "t", "-a", "a", "-c", "c")]
	[InlineData("-t", "t", "-f", "f", "-a", "a", "-c", "c")]
	[InlineData("-t", "t", "-f", "f", "-c", "c", "-a", "a")]
	[InlineData("-f", "f", "-t", "t", "-c", "c", "-a", "a")]
	[InlineData("-c", "c", "-t", "t", "-f", "f", "-a", "a")]
	[InlineData("-t", "t", "-c", "c", "-f", "f", "-a", "a")]
	[InlineData("-f", "f", "-c", "c", "-t", "t", "-a", "a")]
	[InlineData("-c", "c", "-f", "f", "-t", "t", "-a", "a")]
	[InlineData("-c", "c", "-a", "a", "-t", "t", "-f", "f")]
	[InlineData("-t", "t", "-c", "c", "-a", "a", "-f", "f")]
	[InlineData("-c", "c", "-t", "t", "-a", "a", "-f", "f")]
	[InlineData("-t", "t", "-a", "a", "-c", "c", "-f", "f")]
	[InlineData("-f", "f", "-a", "a", "-c", "c", "-t", "t")]
	[InlineData("-c", "c", "-f", "f", "-a", "a", "-t", "t")]
	[InlineData("-f", "f", "-c", "c", "-a", "a", "-t", "t")]
	[InlineData("-c", "c", "-a", "a", "-f", "f", "-t", "t")]
	public void InvalidArgumentOrder(params string[] arguments)
	{
		Assert.Throws<InvalidRunnerArgumentsException>(() => ArgumentsParser.Parse(arguments));
	}
}
