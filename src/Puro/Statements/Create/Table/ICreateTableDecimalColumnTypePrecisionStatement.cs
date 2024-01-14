namespace Puro.Statements.Create.Table;

/// <summary>
/// 
/// </summary>
public interface ICreateTableDecimalColumnTypePrecisionStatement
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="scale"></param>
	/// <returns></returns>
	public ICreateTableColumnNumberTypeStatement WithScale(short scale);
}