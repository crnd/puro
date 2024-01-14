namespace Puro.Statements.Create.Table;

/// <summary>
/// 
/// </summary>
public interface ICreateTableSchemaStatement
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	public ICreateTableColumnStatement WithColumn(string name);
}