using Puro.Statements.Rename.Column;
using Puro.Statements.Rename.Table;

namespace Puro.Statements.Rename;

/// <summary>
/// Starting point for building rename statements.
/// </summary>
public interface IRenameBuilder
{
	/// <summary>
	/// Renames an existing table.
	/// </summary>
	/// <param name="name">Name of the table.</param>
	/// <returns>Interface for defining the schema.</returns>
	public IRenameTableStatement Table(string name);

	/// <summary>
	/// Renames a table column.
	/// </summary>
	/// <param name="name">Name of the column.</param>
	/// <returns>Interface for defining the table.</returns>
	public IRenameColumnStatement Column(string name);
}
