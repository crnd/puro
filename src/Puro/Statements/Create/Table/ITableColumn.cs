namespace Puro.Statements.Create.Table;

/// <summary>
/// Column definition for a table.
/// </summary>
public interface ITableColumn
{
	/// <summary>
	/// Name of the column.
	/// </summary>
	public string Name { get; }

	/// <summary>
	/// Type of the column.
	/// </summary>
	public Type? Type { get; }

	/// <summary>
	/// Precision of a decimal column.
	/// </summary>
	public int? Precision { get; }

	/// <summary>
	/// Scale of a decimal column.
	/// </summary>
	public int? Scale { get; }

	/// <summary>
	/// Exact length of a string column.
	/// </summary>
	public int? ExactLength { get; }

	/// <summary>
	/// Maximum length of a string column.
	/// </summary>
	public int? MaximumLength { get; }

	/// <summary>
	/// True if the column is nullable.
	/// </summary>
	public bool? Nullable { get; }

	/// <summary>
	/// True if the column is an identity column.
	/// </summary>
	public bool Identity { get; }
}
