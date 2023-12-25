namespace Puro.Statements.Drop.Constraint;

/// <summary>
/// Methods for defining the schema.
/// </summary>
public interface IDropConstraintFromTableStatement
{
	/// <summary>
	/// Defines the schema for the drop constraint statement.
	/// </summary>
	/// <param name="name">Name of the constraint.</param>
	public void InSchema(string name);
}