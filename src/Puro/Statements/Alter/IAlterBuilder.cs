using Puro.Statements.Alter.Table;

namespace Puro.Statements.Alter;

/// <summary>
/// Starting point for building alter statements.
/// </summary>
public interface IAlterBuilder
{
	/// <summary>
	/// Alters an existing table.
	/// </summary>
	/// <param name="name">Name of the table.</param>
	/// <returns>Interface for defining the schema for the table.</returns>
	public IAlterTableStatement Table(string name);
}