namespace Puro.Statements.Create.PrimaryKey;

/// <summary>
/// Methods for defining the columns.
/// </summary>
public interface ICreatePrimaryKeySchemaStatement
{
	/// <summary>
	/// Defines a column for the primary key constraint.
	/// </summary>
	/// <param name="name">Name of the column.</param>
	/// <returns>Interface for defining columns.</returns>
	public ICreatePrimaryKeySchemaStatement WithColumn(string name);
}