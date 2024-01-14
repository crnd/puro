using Puro.Exceptions;

namespace Puro.Statements.Create.Table;

internal sealed class CreateTableStatement :
	ICreateTableStatement,
	ICreateTableSchemaStatement,
	ICreateTableColumnStatement,
	ICreateTableColumnTypeStatement,
	ICreateTableDecimalColumnTypeStatement,
	ICreateTableDecimalColumnTypePrecisionStatement,
	ICreateTableColumnNumberTypeStatement,
	ICreateTableStringColumnTypeStatement,
	ICreateTableMigrationStatement
{
	private readonly List<TableColumn> columns = new();

	public string? Schema { get; private set; }

	public string Table { get; }

	public IReadOnlyList<ITableColumn> Columns => columns.AsReadOnly();

	public CreateTableStatement(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		Table = name;
	}

	public ICreateTableSchemaStatement InSchema(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		Schema = name;

		return this;
	}

	public ICreateTableColumnStatement WithColumn(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException(nameof(name));
		}

		if (columns.Select(c => c.Name).Contains(name))
		{
			throw new TableColumnExistsException(Table, name);
		}

		columns.Add(new TableColumn(name));

		return this;
	}

	public ICreateTableColumnTypeStatement AsBool()
	{
		columns.Last().Type = typeof(bool);

		return this;
	}

	public ICreateTableColumnNumberTypeStatement AsShort()
	{
		columns.Last().Type = typeof(short);

		return this;
	}

	public ICreateTableColumnNumberTypeStatement AsInt()
	{
		columns.Last().Type = typeof(int);

		return this;
	}

	public ICreateTableColumnNumberTypeStatement AsLong()
	{
		columns.Last().Type = typeof(long);

		return this;
	}

	public ICreateTableColumnNumberTypeStatement AsDouble()
	{
		columns.Last().Type = typeof(double);

		return this;
	}

	public ICreateTableDecimalColumnTypeStatement AsDecimal()
	{
		columns.Last().Type = typeof(decimal);

		return this;
	}

	public ICreateTableDecimalColumnTypePrecisionStatement WithPrecision(short precision)
	{
		columns.Last().Precision = precision;

		return this;
	}

	public ICreateTableColumnNumberTypeStatement WithScale(short scale)
	{
		columns.Last().Scale = scale;

		return this;
	}

	public ICreateTableColumnTypeStatement AsGuid()
	{
		columns.Last().Type = typeof(Guid);

		return this;
	}

	public ICreateTableStringColumnTypeStatement AsString()
	{
		columns.Last().Type = typeof(string);

		return this;
	}

	public ICreateTableColumnTypeStatement FixedLength(int length)
	{
		if (length < 1)
		{
			throw new InvalidStringLengthException(length);
		}

		columns.Last().ExactLength = length;

		return this;
	}

	public ICreateTableColumnTypeStatement MaximumLength(int length)
	{
		if (length < 1)
		{
			throw new InvalidStringLengthException(length);
		}

		columns.Last().MaximumLength = length;

		return this;
	}

	public ICreateTableColumnTypeStatement AsDate()
	{
		columns.Last().Type = typeof(DateOnly);

		return this;
	}

	public ICreateTableColumnTypeStatement AsTime()
	{
		columns.Last().Type = typeof(TimeOnly);

		return this;
	}

	public ICreateTableColumnTypeStatement AsDateTime()
	{
		columns.Last().Type = typeof(DateTime);

		return this;
	}

	public ICreateTableColumnTypeStatement AsDateTimeOffset()
	{
		columns.Last().Type = typeof(DateTimeOffset);

		return this;
	}

	public ICreateTableSchemaStatement Null()
	{
		columns.Last().Nullable = true;

		return this;
	}

	public ICreateTableSchemaStatement NotNull()
	{
		columns.Last().Nullable = false;

		return this;
	}

	public ICreateTableSchemaStatement Identity()
	{
		columns.Last().Identity = true;
		columns.Last().Nullable = false;

		return this;
	}
}
