namespace Puro.Statements.Alter.Table.AddColumn;

/// <summary>
/// 
/// </summary>
public interface IAlterTableAddColumnStatement
{
	public IAddColumnTypeStatement AddColumn(string name);
}