using Puro.Exceptions;
using Puro.Statements.Alter.Table.AddColumn;

namespace Puro.Statements.Alter.Table;

internal sealed class AlterTableStatement :
	IAlterTableStatement,
	IAlterTableSchemaStatement,
	IAddColumnTypeStatement,
	IAddColumnStatement,
	IAddColumnDecimalTypeStatement,
	IAddColumnDecimalTypePrecisionStatement,
	IAddColumnStringTypeStatement,
	IAlterTableMigrationStatement
{
	private readonly List<(TableColumnChangeType ChangeType, TableColumn Column)> columnChanges = [];

	public string? Schema { get; private set; }

	public string Table { get; }

	public IReadOnlyList<(TableColumnChangeType ChangeType, ITableColumn Column)> ColumnChanges =>
		columnChanges.Select(c => (c.ChangeType, (ITableColumn)c.Column)).ToList().AsReadOnly();

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

	public IAddColumnStatement AddColumn(string name)
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

	public IAddColumnTypeStatement AsBool()
	{
		columnChanges.Last().Column.Type = typeof(bool);

		return this;
	}

	public IAddColumnTypeStatement AsShort()
	{
		columnChanges.Last().Column.Type = typeof(short);

		return this;
	}

	public IAddColumnTypeStatement AsInt()
	{
		columnChanges.Last().Column.Type = typeof(int);

		return this;
	}

	public IAddColumnTypeStatement AsLong()
	{
		columnChanges.Last().Column.Type = typeof(long);

		return this;
	}

	public IAddColumnTypeStatement AsDouble()
	{
		columnChanges.Last().Column.Type = typeof(double);

		return this;
	}

	public IAddColumnDecimalTypeStatement AsDecimal()
	{
		columnChanges.Last().Column.Type = typeof(decimal);

		return this;
	}

	public IAddColumnDecimalTypePrecisionStatement WithPrecision(short precision)
	{
		columnChanges.Last().Column.Precision = precision;

		return this;
	}

	public IAddColumnTypeStatement WithScale(short scale)
	{
		columnChanges.Last().Column.Scale = scale;

		return this;
	}

	public IAddColumnTypeStatement AsGuid()
	{
		columnChanges.Last().Column.Type = typeof(Guid);

		return this;
	}

	public IAddColumnStringTypeStatement AsString()
	{
		columnChanges.Last().Column.Type = typeof(string);

		return this;
	}

	public IAddColumnTypeStatement FixedLength(int length)
	{
		if (length < 1)
		{
			throw new InvalidStringLengthException(length);
		}

		columnChanges.Last().Column.FixedLength = length;

		return this;
	}

	public IAddColumnTypeStatement MaximumLength(int length)
	{
		if (length < 1)
		{
			throw new InvalidStringLengthException(length);
		}

		columnChanges.Last().Column.MaximumLength = length;

		return this;
	}

	public IAddColumnTypeStatement AsDate()
	{
		columnChanges.Last().Column.Type = typeof(DateOnly);

		return this;
	}

	public IAddColumnTypeStatement AsTime()
	{
		columnChanges.Last().Column.Type = typeof(TimeOnly);

		return this;
	}

	public IAddColumnTypeStatement AsDateTime()
	{
		columnChanges.Last().Column.Type = typeof(DateTime);

		return this;
	}

	public IAddColumnTypeStatement AsDateTimeOffset()
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
