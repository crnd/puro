using Microsoft.Data.SqlClient;
using Puro.SqlServer.Runner.Generators;
using Xunit;

namespace Puro.SqlServer.Runner.Tests.Integration;

[TestCaseOrderer(
	ordererTypeName: "Puro.SqlServer.Runner.Tests.Integration.PriorityTestCaseOrderer",
	ordererAssemblyName: "Puro.SqlServer.Runner.Tests.Integration")]
public class SchemaChangesTests : IClassFixture<ContainerFixture>
{
	private readonly string connectionString;

	public SchemaChangesTests(ContainerFixture fixture)
	{
		connectionString = fixture.container.GetConnectionString();
	}

	[Fact]
	[TestPriority(1)]
	public void MigrationsTableCreated()
	{
		var migrations = MigrationsProcessor.Prepare([typeof(CreateTablesMigration)], null, null);
		var migrationSql = MigrationSqlGenerator.Generate(migrations);

		using var connection = new SqlConnection(connectionString);
		connection.Open();

		using var command = new SqlCommand(migrationSql, connection);
		command.ExecuteNonQuery();

		command.CommandText = GenerateTableExistsSql("dbo", "__PuroMigrationsHistory"); ;
		var tableCreated = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(tableCreated);
	}

	[Fact]
	[TestPriority(2)]
	public void TablesCreated()
	{
		using var connection = new SqlConnection(connectionString);
		connection.Open();

		using var command = new SqlCommand(GenerateTableExistsSql("Test", "Table1"), connection);
		command.ExecuteNonQuery();

		var tableCreated = Convert.ToBoolean(command.ExecuteScalar());
		Assert.True(tableCreated);

		command.CommandText = GenerateTableExistsSql("Test", "Table2"); ;
		tableCreated = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(tableCreated);

		command.CommandText = GenerateTableExistsSql("Test", "Table3"); ;
		tableCreated = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(tableCreated);

		command.CommandText = GenerateTableExistsSql("Test", "Table4"); ;
		tableCreated = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(tableCreated);

		command.CommandText = GenerateTableExistsSql("Funky", "Table5");
		tableCreated = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(tableCreated);
	}

	[Fact]
	[TestPriority(3)]
	public void PrimaryKeysCreated()
	{
		var migrations = MigrationsProcessor.Prepare([typeof(CreatePrimaryKeysMigration)], null, null);
		var migrationSql = MigrationSqlGenerator.Generate(migrations);

		using var connection = new SqlConnection(connectionString);
		connection.Open();

		using var command = new SqlCommand(migrationSql, connection);
		command.ExecuteNonQuery();

		command.CommandText = GeneratePrimaryKeyExistsSql("Test", "Table1");
		var primaryKeyCreated = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(primaryKeyCreated);

		command.CommandText = GeneratePrimaryKeyExistsSql("Test", "Table2");
		primaryKeyCreated = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(primaryKeyCreated);

		command.CommandText = GeneratePrimaryKeyExistsSql("Test", "Table3");
		primaryKeyCreated = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(primaryKeyCreated);

		command.CommandText = GeneratePrimaryKeyExistsSql("Test", "Table4");
		primaryKeyCreated = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(primaryKeyCreated);
	}

	[Fact]
	[TestPriority(4)]
	public void ForeignKeysCreated()
	{
		var migrations = MigrationsProcessor.Prepare([typeof(CreateForeignKeysMigration)], null, null);
		var migrationSql = MigrationSqlGenerator.Generate(migrations);

		using var connection = new SqlConnection(connectionString);
		connection.Open();

		using var command = new SqlCommand(migrationSql, connection);
		command.ExecuteNonQuery();

		command.CommandText = GenerateForeignKeyExistsSql("Test", "FK_Table4_Table1");
		var foreignKeyCreated = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(foreignKeyCreated);

		command.CommandText = GenerateForeignKeyExistsSql("Test", "FK_Table4_Table2");
		foreignKeyCreated = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(foreignKeyCreated);

		command.CommandText = GenerateForeignKeyExistsSql("Test", "FK_Table4_Table3");
		foreignKeyCreated = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(foreignKeyCreated);
	}

	[Fact]
	[TestPriority(5)]
	public void IndexesCreated()
	{
		var migrations = MigrationsProcessor.Prepare([typeof(CreateIndexesMigration)], null, null);
		var migrationSql = MigrationSqlGenerator.Generate(migrations);

		using var connection = new SqlConnection(connectionString);
		connection.Open();

		using var command = new SqlCommand(migrationSql, connection);
		command.ExecuteNonQuery();

		command.CommandText = GenerateIndexExistsSql("Test", "Table1", "UIX_Table1_Guid");
		var indexCreated = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(indexCreated);

		command.CommandText = GenerateIndexExistsSql("Test", "Table2", "IX_Table2_Date_Time");
		indexCreated = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(indexCreated);
	}

	[Fact]
	[TestPriority(6)]
	public void TablesAltered()
	{
		var migrations = MigrationsProcessor.Prepare([typeof(AlterMigration)], null, null);
		var migrationSql = MigrationSqlGenerator.Generate(migrations);

		using var connection = new SqlConnection(connectionString);
		connection.Open();

		using var command = new SqlCommand(migrationSql, connection);
		command.ExecuteNonQuery();

		command.CommandText = GenerateColumnExistsSql("Funky", "Table5", "Description");
		var columnExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(columnExists);

		command.CommandText = GenerateColumnExistsSql("Test", "Table2", "Code");
		columnExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(columnExists);

		const string table1ColumnAlteredSql = """
			SELECT TOP 1
				[DATA_TYPE],
				[IS_NULLABLE]
			FROM [INFORMATION_SCHEMA].[COLUMNS]
			WHERE
				[TABLE_SCHEMA] = N'Test' AND
				[TABLE_NAME] = N'Table1' AND
				[COLUMN_NAME] = N'Bool';
			""";

		command.CommandText = table1ColumnAlteredSql;
		var reader = command.ExecuteReader();
		reader.Read();

		Assert.Equal("bit", reader.GetFieldValue<string>(0));
		Assert.Equal("YES", reader.GetFieldValue<string>(1));

		reader.Close();

		const string table2ColumnAlteredSql = """
			SELECT TOP 1
				[DATA_TYPE],
				[IS_NULLABLE]
			FROM [INFORMATION_SCHEMA].[COLUMNS]
			WHERE
				[TABLE_SCHEMA] = N'Test' AND
				[TABLE_NAME] = N'Table2' AND
				[COLUMN_NAME] = N'Offset';
			""";

		command.CommandText = table2ColumnAlteredSql;
		reader = command.ExecuteReader();
		reader.Read();

		Assert.Equal("datetimeoffset", reader.GetFieldValue<string>(0));
		Assert.Equal("NO", reader.GetFieldValue<string>(1));

		reader.Close();

		const string table3ColumnAlteredSql = """
			SELECT TOP 1
				[DATA_TYPE],
				[CHARACTER_MAXIMUM_LENGTH],
				[IS_NULLABLE]
			FROM [INFORMATION_SCHEMA].[COLUMNS]
			WHERE
				[TABLE_SCHEMA] = N'Test' AND
				[TABLE_NAME] = N'Table3' AND
				[COLUMN_NAME] = N'MaxString';
			""";

		command.CommandText = table3ColumnAlteredSql;
		reader = command.ExecuteReader();
		reader.Read();

		Assert.Equal("nchar", reader.GetFieldValue<string>(0));
		Assert.StrictEqual(5, reader.GetFieldValue<int>(1));
		Assert.Equal("NO", reader.GetFieldValue<string>(2));

		reader.Close();

		command.CommandText = GenerateColumnExistsSql("Test", "Table3", "FixedString");
		columnExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(columnExists);
	}

	[Fact]
	[TestPriority(7)]
	public void ObjectsRenamed()
	{
		var migrations = MigrationsProcessor.Prepare([typeof(RenameMigration)], null, null);
		var migrationSql = MigrationSqlGenerator.Generate(migrations);

		using var connection = new SqlConnection(connectionString);
		connection.Open();

		using var command = new SqlCommand(migrationSql, connection);
		command.ExecuteNonQuery();

		const string indexRenamedSql = """
			IF
				INDEXPROPERTY(OBJECT_ID(N'[Test].[Table1]'), N'UIX_Table1_Guid', N'IndexID') IS NULL AND
				INDEXPROPERTY(OBJECT_ID(N'[Test].[Table1]'), N'UIX_Table1_Guid_Renamed', N'IndexID') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";

		command.CommandText = indexRenamedSql;
		var indexRenamed = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(indexRenamed);

		const string columnRenamedSql = """
			IF
				COL_LENGTH(N'[Funky].[Table6]', N'Name') IS NULL AND
				COL_LENGTH(N'[Funky].[Table6]', N'Code') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END;
			""";

		command.CommandText = columnRenamedSql;
		var columnRenamed = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(columnRenamed);

		const string tableRenamedSql = """
			IF
				OBJECT_ID('[Funky].[Table5]', 'U') IS NULL AND
				OBJECT_ID('[Funky].[Table6]', 'U') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";

		command.CommandText = tableRenamedSql;
		var tableRenamed = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(tableRenamed);
	}

	[Fact]
	[TestPriority(8)]
	public void SqlExecuted()
	{
		var migrations = MigrationsProcessor.Prepare([typeof(SqlMigration)], null, null);
		var migrationSql = MigrationSqlGenerator.Generate(migrations);

		using var connection = new SqlConnection(connectionString);
		connection.Open();

		using var command = new SqlCommand(migrationSql, connection);
		command.ExecuteNonQuery();

		command.CommandText = GenerateTableExistsSql("dbo", "Table5");
		var tableExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(tableExists);
	}

	[Fact]
	[TestPriority(9)]
	public void IndexesDropped()
	{
		var migrations = MigrationsProcessor.Prepare([typeof(DropIndexesMigration)], null, null);
		var migrationSql = MigrationSqlGenerator.Generate(migrations);

		using var connection = new SqlConnection(connectionString);
		connection.Open();

		using var command = new SqlCommand(migrationSql, connection);
		command.ExecuteNonQuery();

		command.CommandText = GenerateIndexExistsSql("Test", "Table1", "UIX_Table1_Guid_Renamed");
		var indexExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(indexExists);

		command.CommandText = GenerateIndexExistsSql("Test", "Table2", "IX_Table2_Date_Time");
		indexExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(indexExists);
	}

	[Fact]
	[TestPriority(10)]
	public void ForeignKeysDropped()
	{
		var migrations = MigrationsProcessor.Prepare([typeof(DropForeignKeysMigration)], null, null);
		var migrationSql = MigrationSqlGenerator.Generate(migrations);

		using var connection = new SqlConnection(connectionString);
		connection.Open();

		using var command = new SqlCommand(migrationSql, connection);
		command.ExecuteNonQuery();

		command.CommandText = GenerateForeignKeyExistsSql("Test", "FK_Table4_Table1");
		var foreignKeyExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(foreignKeyExists);

		command.CommandText = GenerateForeignKeyExistsSql("Test", "FK_Table4_Table2");
		foreignKeyExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(foreignKeyExists);

		command.CommandText = GenerateForeignKeyExistsSql("Test", "FK_Table4_Table3");
		foreignKeyExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(foreignKeyExists);
	}

	[Fact]
	[TestPriority(11)]
	public void PrimaryKeysDropped()
	{
		var migrations = MigrationsProcessor.Prepare([typeof(DropPrimaryKeysMigration)], null, null);
		var migrationSql = MigrationSqlGenerator.Generate(migrations);

		using var connection = new SqlConnection(connectionString);
		connection.Open();

		using var command = new SqlCommand(migrationSql, connection);
		command.ExecuteNonQuery();

		command.CommandText = GeneratePrimaryKeyExistsSql("Test", "Table1");
		var primaryKeyExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(primaryKeyExists);

		command.CommandText = GeneratePrimaryKeyExistsSql("Test", "Table2");
		primaryKeyExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(primaryKeyExists);

		command.CommandText = GeneratePrimaryKeyExistsSql("Test", "Table3");
		primaryKeyExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(primaryKeyExists);

		command.CommandText = GeneratePrimaryKeyExistsSql("Test", "Table4");
		primaryKeyExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(primaryKeyExists);
	}

	[Fact]
	[TestPriority(12)]
	public void TablesDropped()
	{
		var migrations = MigrationsProcessor.Prepare([typeof(DropTablesMigration)], null, null);
		var migrationSql = MigrationSqlGenerator.Generate(migrations);

		using var connection = new SqlConnection(connectionString);
		connection.Open();

		using var command = new SqlCommand(migrationSql, connection);
		command.ExecuteNonQuery();

		command.CommandText = GenerateTableExistsSql("Test", "Table1");
		var tableExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(tableExists);

		command.CommandText = GenerateTableExistsSql("Test", "Table2");
		tableExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(tableExists);

		command.CommandText = GenerateTableExistsSql("Test", "Table3");
		tableExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(tableExists);

		command.CommandText = GenerateTableExistsSql("Test", "Table4");
		tableExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(tableExists);

		command.CommandText = GenerateTableExistsSql("dbo", "Table5");
		tableExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(tableExists);

		command.CommandText = GenerateTableExistsSql("Funky", "Table6");
		tableExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(tableExists);
	}

	[Fact]
	[TestPriority(13)]
	public void MigrationNamesInsertedInOrder()
	{
		const string migrationsNamesSql = """
			SELECT [MigrationName]
			FROM [dbo].[__PuroMigrationsHistory]
			ORDER BY [AppliedOn] ASC;
			""";

		using var connection = new SqlConnection(connectionString);
		connection.Open();

		using var command = new SqlCommand(migrationsNamesSql, connection);
		var reader = command.ExecuteReader();
		var names = new List<string>(11);
		while (reader.Read())
		{
			names.Add(reader.GetFieldValue<string>(0));
		}

		Assert.StrictEqual(11, names.Count);
		Assert.Equal("1_CreateTablesMigration", names[0]);
		Assert.Equal("2_CreatePrimaryKeysMigration", names[1]);
		Assert.Equal("3_CreateForeignKeysMigration", names[2]);
		Assert.Equal("4_CreateIndexesMigration", names[3]);
		Assert.Equal("5_AlterMigration", names[4]);
		Assert.Equal("6_RenameMigration", names[5]);
		Assert.Equal("7_SqlMigration", names[6]);
		Assert.Equal("8_DropIndexesMigration", names[7]);
		Assert.Equal("9_DropForeignKeysMigration", names[8]);
		Assert.Equal("10_DropPrimaryKeysMigration", names[9]);
		Assert.Equal("11_DropTablesMigration", names[10]);
	}

	[MigrationName("1_CreateTablesMigration")]
	private sealed class CreateTablesMigration : Migration
	{
		public override void Up()
		{
			Use.Schema("Test");

			Create.Table("Table1")
				.WithColumn("Id").AsShort().Identity()
				.WithColumn("Decimal").AsDecimal().WithPrecision(5).WithScale(2).NotNull()
				.WithColumn("Double").AsDouble().NotNull()
				.WithColumn("Bool").AsBool().NotNull()
				.WithColumn("Guid").AsGuid().Null();

			Create.Table("Table2")
				.WithColumn("Id").AsInt().Identity()
				.WithColumn("Date").AsDate().Null()
				.WithColumn("Time").AsTime().Null()
				.WithColumn("DateTime").AsDateTime().Null()
				.WithColumn("Offset").AsDateTimeOffset().Null();

			Create.Table("Table3")
				.WithColumn("Id").AsLong().Identity()
				.WithColumn("String").AsString().Null()
				.WithColumn("MaxString").AsString().MaximumLength(150).NotNull()
				.WithColumn("FixedString").AsString().FixedLength(10).Null();

			Create.Table("Table4")
				.WithColumn("Table1Id").AsShort().NotNull()
				.WithColumn("Table2Id").AsInt().NotNull()
				.WithColumn("Table3Id").AsLong().Null();

			Create.Table("Table5")
				.InSchema("Funky")
				.WithColumn("Id").AsInt().Identity()
				.WithColumn("Name").AsString().MaximumLength(25).NotNull();
		}
	}

	[MigrationName("2_CreatePrimaryKeysMigration")]
	private sealed class CreatePrimaryKeysMigration : Migration
	{
		public override void Up()
		{
			Use.Schema("Test");

			Create.PrimaryKey("PK_Table1")
				.OnTable("Table1")
				.WithColumn("Id");

			Create.PrimaryKey("PK_Table2")
				.OnTable("Table2")
				.WithColumn("Id");

			Create.PrimaryKey("PK_Table3")
				.OnTable("Table3")
				.WithColumn("Id");

			Create.PrimaryKey("PK_Table4")
				.OnTable("Table4")
				.WithColumn("Table1Id")
				.WithColumn("Table2Id");
		}
	}

	[MigrationName("3_CreateForeignKeysMigration")]
	private sealed class CreateForeignKeysMigration : Migration
	{
		public override void Up()
		{
			Use.Schema("Test");

			Create.ForeignKey("FK_Table4_Table1")
				.FromTable("Table4")
				.FromColumn("Table1Id")
				.ToTable("Table1")
				.ToColumn("Id")
				.OnDeleteCascade();

			Create.ForeignKey("FK_Table4_Table2")
				.FromTable("Table4")
				.FromColumn("Table2Id")
				.ToTable("Table2")
				.ToColumn("Id")
				.OnDeleteRestrict();

			Create.ForeignKey("FK_Table4_Table3")
				.FromTable("Table4")
				.FromColumn("Table3Id")
				.ToTable("Table3")
				.ToColumn("Id")
				.OnDeleteSetNull();
		}
	}

	[MigrationName("4_CreateIndexesMigration")]
	private sealed class CreateIndexesMigration : Migration
	{
		public override void Up()
		{
			Use.Schema("Test");

			Create.UniqueIndex("UIX_Table1_Guid")
				.OnTable("Table1")
				.OnColumn("Guid").Ascending();

			Create.Index("IX_Table2_Date_Time")
				.OnTable("Table2")
				.OnColumn("Date").Ascending()
				.OnColumn("Time").Ascending();
		}
	}

	[MigrationName("5_AlterMigration")]
	private sealed class AlterMigration : Migration
	{
		public override void Up()
		{
			Alter.Table("Table5")
				.InSchema("Funky")
				.AddColumn("Description").AsString().MaximumLength(500).Null();

			Alter.Table("Table2")
				.InSchema("Test")
				.AddColumn("Code").AsString().FixedLength(10).NotNull();

			Alter.Table("Table1")
				.InSchema("Test")
				.AlterColumn("Bool").AsBool().Null();

			Alter.Table("Table2")
				.InSchema("Test")
				.AlterColumn("Offset").AsDateTimeOffset().NotNull();

			Alter.Table("Table3")
				.InSchema("Test")
				.AlterColumn("MaxString").AsString().FixedLength(5).NotNull();

			Alter.Table("Table3")
				.InSchema("Test")
				.DropColumn("FixedString");
		}
	}

	[MigrationName("6_RenameMigration")]
	private sealed class RenameMigration : Migration
	{
		public override void Up()
		{
			Rename.Index("UIX_Table1_Guid")
				.InTable("Table1")
				.InSchema("Test")
				.To("UIX_Table1_Guid_Renamed");

			Rename.Column("Name")
				.InTable("Table5")
				.InSchema("Funky")
				.To("Code");

			Rename.Table("Table5")
				.InSchema("Funky")
				.To("Table6");
		}
	}

	[MigrationName("7_SqlMigration")]
	private sealed class SqlMigration : Migration
	{
		public override void Up()
		{
			Sql("""
				CREATE TABLE [dbo].[Table5] (
					[Id] INT NOT NULL,
					[Name] NVARCHAR(25) NOT NULL
				);
				""");
		}
	}

	[MigrationName("8_DropIndexesMigration")]
	private sealed class DropIndexesMigration : Migration
	{
		public override void Up()
		{
			Drop.Index("UIX_Table1_Guid_Renamed").FromTable("Table1").InSchema("Test");
			Drop.Index("IX_Table2_Date_Time").FromTable("Table2").InSchema("Test");
		}
	}

	[MigrationName("9_DropForeignKeysMigration")]
	private sealed class DropForeignKeysMigration : Migration
	{
		public override void Up()
		{
			Drop.Constraint("FK_Table4_Table1").FromTable("Table4").InSchema("Test");
			Drop.Constraint("FK_Table4_Table2").FromTable("Table4").InSchema("Test");
			Drop.Constraint("FK_Table4_Table3").FromTable("Table4").InSchema("Test");
		}
	}

	[MigrationName("10_DropPrimaryKeysMigration")]
	private sealed class DropPrimaryKeysMigration : Migration
	{
		public override void Up()
		{
			Drop.Constraint("PK_Table1").FromTable("Table1").InSchema("Test");
			Drop.Constraint("PK_Table2").FromTable("Table2").InSchema("Test");
			Drop.Constraint("PK_Table3").FromTable("Table3").InSchema("Test");
			Drop.Constraint("PK_Table4").FromTable("Table4").InSchema("Test");
		}
	}

	[MigrationName("11_DropTablesMigration")]
	private sealed class DropTablesMigration : Migration
	{
		public override void Up()
		{
			Drop.Table("Table1").InSchema("Test");
			Drop.Table("Table2").InSchema("Test");
			Drop.Table("Table3").InSchema("Test");
			Drop.Table("Table4").InSchema("Test");
			Drop.Table("Table5");
			Drop.Table("Table6").InSchema("Funky");
		}
	}

	private static string GenerateTableExistsSql(string schema, string table)
	{
		return $"""
			IF OBJECT_ID('[{schema}].[{table}]', 'U') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";
	}

	private static string GenerateIndexExistsSql(string schema, string table, string index)
	{
		return $"""
			IF INDEXPROPERTY(OBJECT_ID(N'[{schema}].[{table}]'), N'{index}', N'IndexID') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";
	}

	private static string GenerateForeignKeyExistsSql(string schema, string foreignKey)
	{
		return $"""
			IF OBJECT_ID(N'[{schema}].[{foreignKey}]', N'F') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";
	}

	private static string GeneratePrimaryKeyExistsSql(string schema, string table)
	{
		return $"SELECT OBJECTPROPERTY(OBJECT_ID(N'[{schema}].[{table}]'), N'TableHasPrimaryKey');";
	}

	private static string GenerateColumnExistsSql(string schema, string table, string column)
	{
		return $"""
			IF COL_LENGTH(N'[{schema}].[{table}]', N'{column}') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END;
			""";
	}
}