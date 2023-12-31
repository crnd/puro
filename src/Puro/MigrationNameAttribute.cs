using Puro.Exceptions;

namespace Puro;

/// <summary>
/// Attribute for defining a name for a migration.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class MigrationNameAttribute : Attribute
{
	private const int MaximumNameLength = 150;

	/// <summary>
	/// Gets the name of the migration.
	/// </summary>
	public string Name { get; }

	/// <summary>
	/// Initializes a new <see cref="MigrationNameAttribute"/> class.
	/// </summary>
	/// <param name="name">Name of the migration.</param>
	/// <exception cref="ArgumentNullException">Thrown when <paramref name="name"/> is null, empty or white space.</exception>
	/// <exception cref="MigrationNameTooLongException">Thrown when <paramref name="name"/> is over 150 characters long.</exception>
	public MigrationNameAttribute(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		if (name.Length > MaximumNameLength)
		{
			throw new MigrationNameTooLongException(MaximumNameLength);
		}

		Name = name;
	}
}
