namespace Puro.Statements.Drop.Constraint;

/// <summary>
/// Methods for defining the table.
/// </summary>
public interface IDropConstraintStatement
{
	/// <summary>
	/// Defines the table for the drop constraint statement.
	/// </summary>
	/// <param name="name">Name of the table.</param>
	/// <returns>Interface for defining the schema.</returns>
	public IDropConstraintFromTableStatement FromTable(string name);
}