namespace Puro.Exceptions;

/// <summary>
/// Exception that is thrown when migration does not have a <see cref="MigrationNameAttribute"/> defined.
/// </summary>
public class NameAttributeNotFoundException : PuroException
{
	/// <summary>
	/// Initializes a new instance of the <see cref="NameAttributeNotFoundException"/> class.
	/// </summary>
	/// <param name="migration">Type of the migration.</param>
	public NameAttributeNotFoundException(Type migration)
		: base($"Migration {migration.Name} does not define the migration name with {nameof(MigrationNameAttribute)}.")
	{
	}
}
