namespace Puro.Statements.Create.Index;

internal sealed class IndexColumn : IIndexColumn
{
	public IndexColumn(string name)
	{
		Name = name;
	}

	public string Name { get; }

	public bool? Descending { get; internal set; }
}
