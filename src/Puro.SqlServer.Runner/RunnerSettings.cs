namespace Puro.SqlServer.Runner;

internal sealed record RunnerSettings
{
	public required string AssemblyLocation { get; set; }

	public string? ConnectionString { get; set; }

	public string? FromMigration { get; set; }

	public string? ToMigration { get; set; }
}
