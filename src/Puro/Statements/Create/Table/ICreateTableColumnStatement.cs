namespace Puro.Statements.Create.Table;

/// <summary>
/// 
/// </summary>
public interface ICreateTableColumnStatement
{
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public ICreateTableColumnTypeStatement AsBool();

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public ICreateTableColumnNumberTypeStatement AsShort();

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public ICreateTableColumnNumberTypeStatement AsInt();

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public ICreateTableColumnNumberTypeStatement AsLong();

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public ICreateTableColumnNumberTypeStatement AsDouble();

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public ICreateTableDecimalColumnTypeStatement AsDecimal();

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public ICreateTableColumnTypeStatement AsGuid();

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public ICreateTableStringColumnTypeStatement AsString();

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public ICreateTableColumnTypeStatement AsDate();

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public ICreateTableColumnTypeStatement AsTime();

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public ICreateTableColumnTypeStatement AsDateTime();

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public ICreateTableColumnTypeStatement AsDateTimeOffset();
}