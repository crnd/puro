using Puro.Statements.Create.ForeignKey;
using Puro.Statements.Create.Index;
using Puro.Statements.Create.PrimaryKey;
using Puro.Statements.Create.Table;

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

	/// <summary>
	/// Creates a new table.
	/// </summary>
	/// <param name="name">Name of the table.</param>
	/// <returns>Interface for defining the schema for the table.</returns>
	public ICreateTableStatement Table(string name);

	/// <summary>
	/// Creates a new index.
	/// </summary>
	/// <param name="name">Name of the index.</param>
	/// <returns>Interface for defining the table for the index.</returns>
	public ICreateIndexStatement Index(string name);

	/// <summary>
	/// Creates a new unique index.
	/// </summary>
	/// <param name="name">Name of the index.</param>
	/// <returns>Interface for defining the table for the index.</returns>
	public ICreateIndexStatement UniqueIndex(string name);
}
