namespace Puro.Statements.Drop.Index;

/// <summary>
/// Methods for defining the table.
/// </summary>
public interface IDropIndexStatement
{
	/// <summary>
	/// Defines the table for the drop index statement.
	/// </summary>
	/// <param name="name">Name of the table.</param>
	/// <returns>Interface for defining the schema.</returns>
	public IDropIndexFromTableStatement FromTable(string name);
}
