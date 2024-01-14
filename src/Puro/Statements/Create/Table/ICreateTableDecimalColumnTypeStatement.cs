namespace Puro.Statements.Create.Table;

/// <summary>
/// 
/// </summary>
public interface ICreateTableDecimalColumnTypeStatement
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="precision"></param>
	/// <returns></returns>
	public ICreateTableDecimalColumnTypePrecisionStatement WithPrecision(short precision);
}