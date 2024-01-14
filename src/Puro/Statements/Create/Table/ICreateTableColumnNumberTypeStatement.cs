namespace Puro.Statements.Create.Table;

/// <summary>
/// 
/// </summary>
public interface ICreateTableColumnNumberTypeStatement : ICreateTableColumnTypeStatement
{
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public ICreateTableSchemaStatement Identity();
}