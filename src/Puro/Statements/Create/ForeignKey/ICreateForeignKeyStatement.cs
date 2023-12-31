namespace Puro.Statements.Create.ForeignKey;

/// <summary>
/// 
/// </summary>
public interface ICreateForeignKeyStatement
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="name">Name of the table.</param>
	/// <returns></returns>
	public ICreateForeignKeyReferencingTableStatement FromTable(string name);
}