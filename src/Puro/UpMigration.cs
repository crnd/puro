namespace Puro;

/// <summary>
/// Base class for a migration that supports only the up direction.
/// </summary>
public abstract class UpMigration : Migration
{
	public sealed override void Down()
	{
	}
}
