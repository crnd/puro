namespace Puro.Statements.Create.Table;

/// <summary>
/// 
/// </summary>
public interface ICreateTableStatement
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="name">Name of the schema.</param>
	/// <returns>Interface to define columns.</returns>
	public ICreateTableSchemaStatement InSchema(string name);
}
