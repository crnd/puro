using Puro.Statements.Drop.Index;
using Puro.Statements.Drop.Table;

namespace Puro.Statements.Drop;

/// <summary>
/// Starting point for building drop statements.
/// </summary>
public interface IDropBuilder
{
	/// <summary>
	/// Drops an existing table.
	/// </summary>
	/// <param name="name">Name of the table.</param>
	/// <returns>Interface for defining the schema.</returns>
	public IDropTableStatement Table(string name);

	/// <summary>
	/// Drops an existing index.
	/// </summary>
	/// <param name="name">Name of the index.</param>
	/// <returns>Interface for defining the table.</returns>
	public IDropIndexStatement Index(string name);
}