namespace Puro.Statements;

/// <summary>
/// Behaviors for delete handling with referencing tables.
/// </summary>
public enum OnDeleteBehavior
{
	/// <summary>
	/// Delete all referenced entries.
	/// </summary>
	Cascade,

	/// <summary>
	/// Prevent delete.
	/// </summary>
	Restrict,

	/// <summary>
	/// Set the referenced columns to null.
	/// </summary>
	SetNull
}
