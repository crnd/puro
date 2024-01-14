namespace Puro.Statements.Create.Table;

internal sealed class TableColumn : ITableColumn
{
	public string Name { get; }

	public Type? Type { get; internal set; }

	public int? Precision { get; internal set; }

	public int? Scale { get; internal set; }

	public int? ExactLength { get; internal set; }

	public int? MaximumLength { get; internal set; }

	public bool? Nullable { get; internal set; }

	public bool Identity { get; internal set; }

	public TableColumn(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		Name = name;
		Identity = false;
	}
}
