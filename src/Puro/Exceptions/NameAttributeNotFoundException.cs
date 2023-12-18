namespace Puro.Exceptions;

public class NameAttributeNotFoundException : PuroException
{
	public NameAttributeNotFoundException(Type migration)
		: base($"Migration {migration.Name} does not define the migration name with {nameof(MigrationNameAttribute)}.")
	{
	}
}
