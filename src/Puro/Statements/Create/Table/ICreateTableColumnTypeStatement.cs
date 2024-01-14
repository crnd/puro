namespace Puro.Statements.Create.Table;

/// <summary>
/// 
/// </summary>
public interface ICreateTableColumnTypeStatement
{
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public ICreateTableSchemaStatement Null();

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public ICreateTableSchemaStatement NotNull();
}