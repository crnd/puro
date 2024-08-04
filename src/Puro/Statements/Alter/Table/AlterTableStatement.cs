using Puro.Exceptions;
using Puro.Statements.Alter.Table.AddColumn;
using Puro.Statements.Alter.Table.DropColumn;

namespace Puro.Statements.Alter.Table;

internal sealed class AlterTableStatement :
	IAlterTableStatement,
	IAlterTableSchemaStatement,
	IDropColumnStatement,
	IAlterTableAddColumnTypeStatement,
	IAddColumnTypeStatement,
	IAlterTableAddColumnDecimalTypeStatement,
	IAlterTableAddColumnDecimalTypePrecisionStatement,
	IAlterTableAddColumnStringTypeStatement,
	IAlterTableMigrationStatement
{
	private readonly List<(TableColumnChangeType ChangeType, TableColumn Column)> columnChanges = [];

	public string? Schema { get; private set; }

	public string Table { get; }

	public IReadOnlyList<(TableColumnChangeType ChangeType, ITableColumn Column)> ColumnChanges => columnChanges.Select(c => (c.ChangeType, (ITableColumn)c.Column)).ToList().AsReadOnly();

	public AlterTableStatement(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		Table = name;
	}

	public IAlterTableSchemaStatement InSchema(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		Schema = name;

		return this;
	}

	public IAlterTableSchemaStatement DropColumn(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		columnChanges.Add((TableColumnChangeType.Drop, new TableColumn(name)));

		return this;
	}

	public IAddColumnTypeStatement AddColumn(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		var columnExists = columnChanges
			.Where(c => c.ChangeType == TableColumnChangeType.Add)
			.Any(c => c.Column.Name == name);
		if (columnExists)
		{
			throw new TableColumnExistsException(Table, name);
		}

		columnChanges.Add((TableColumnChangeType.Add, new TableColumn(name)));

		return this;
	}

	public IAlterTableAddColumnTypeStatement AsBool()
	{
		columnChanges.Last().Column.Type = typeof(bool);

		return this;
	}

	public IAlterTableAddColumnTypeStatement AsShort()
	{
		columnChanges.Last().Column.Type = typeof(short);

		return this;
	}

	public IAlterTableAddColumnTypeStatement AsInt()
	{
		columnChanges.Last().Column.Type = typeof(int);

		return this;
	}

	public IAlterTableAddColumnTypeStatement AsLong()
	{
		columnChanges.Last().Column.Type = typeof(long);

		return this;
	}

	public IAlterTableAddColumnTypeStatement AsDouble()
	{
		columnChanges.Last().Column.Type = typeof(double);

		return this;
	}

	public IAlterTableAddColumnDecimalTypeStatement AsDecimal()
	{
		columnChanges.Last().Column.Type = typeof(decimal);

		return this;
	}

	public IAlterTableAddColumnDecimalTypePrecisionStatement WithPrecision(short precision)
	{
		columnChanges.Last().Column.Precision = precision;

		return this;
	}

	public IAlterTableAddColumnTypeStatement WithScale(short scale)
	{
		columnChanges.Last().Column.Scale = scale;

		return this;
	}

	public IAlterTableAddColumnTypeStatement AsGuid()
	{
		columnChanges.Last().Column.Type = typeof(Guid);

		return this;
	}

	public IAlterTableAddColumnStringTypeStatement AsString()
	{
		columnChanges.Last().Column.Type = typeof(string);

		return this;
	}

	public IAlterTableAddColumnTypeStatement FixedLength(int length)
	{
		if (length < 1)
		{
			throw new InvalidStringLengthException(length);
		}

		columnChanges.Last().Column.FixedLength = length;

		return this;
	}

	public IAlterTableAddColumnTypeStatement MaximumLength(int length)
	{
		if (length < 1)
		{
			throw new InvalidStringLengthException(length);
		}

		columnChanges.Last().Column.MaximumLength = length;

		return this;
	}

	public IAlterTableAddColumnTypeStatement AsDate()
	{
		columnChanges.Last().Column.Type = typeof(DateOnly);

		return this;
	}

	public IAlterTableAddColumnTypeStatement AsTime()
	{
		columnChanges.Last().Column.Type = typeof(TimeOnly);

		return this;
	}

	public IAlterTableAddColumnTypeStatement AsDateTime()
	{
		columnChanges.Last().Column.Type = typeof(DateTime);

		return this;
	}

	public IAlterTableAddColumnTypeStatement AsDateTimeOffset()
	{
		columnChanges.Last().Column.Type = typeof(DateTimeOffset);

		return this;
	}

	public IAlterTableSchemaStatement Null()
	{
		columnChanges.Last().Column.Nullable = true;

		return this;
	}

	public IAlterTableSchemaStatement NotNull()
	{
		columnChanges.Last().Column.Nullable = false;

		return this;
	}
}
