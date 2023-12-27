using Puro.Statements.Create.PrimaryKey;

namespace Puro.Statements.Create;

/// <summary>
/// Starting point for building create statements.
/// </summary>
public interface ICreateBuilder
{
	/// <summary>
	/// Creates a new primary key constraint.
	/// </summary>
	/// <param name="name">Name of the primary key.</param>
	/// <returns>Interface for defining the table.</returns>
	public ICreatePrimaryKeyStatement PrimaryKey(string name);
}
