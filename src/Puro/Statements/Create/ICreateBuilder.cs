using Puro.Statements.Create.ForeignKey;
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

	/// <summary>
	/// Creates a new foreign key constraint.
	/// </summary>
	/// <param name="name">Name of the foreign key.</param>
	/// <returns>Interface for defining the table where the foreign key exists.</returns>
	public ICreateForeignKeyStatement ForeignKey(string name);
}
