namespace Puro.Statements.Use;

/// <summary>
/// Starting point for defining use statements.
/// </summary>
public interface IUseBuilder
{
	/// <summary>
	/// Defines a default schema for the migration.
	/// </summary>
	/// <param name="name">Name of the schema.</param>
	public void Schema(string name);
}
