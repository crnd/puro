namespace Puro.Statements.Create.Table;

/// <summary>
/// 
/// </summary>
public interface ICreateTableStringColumnTypeStatement : ICreateTableColumnTypeStatement
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="length"></param>
	/// <returns></returns>
	public ICreateTableColumnTypeStatement FixedLength(int length);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="length"></param>
	/// <returns></returns>
	public ICreateTableColumnTypeStatement MaximumLength(int length);
}