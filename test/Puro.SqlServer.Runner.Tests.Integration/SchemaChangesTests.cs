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

	public SchemaChangesTests(ContainerFixture containerFixture)
	{
		connectionString = containerFixture.container.GetConnectionString();
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

		const string tableExistenceSql = """
			IF OBJECT_ID('[dbo].[__PuroMigrationsHistory]', 'U') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";

		command.CommandText = tableExistenceSql;
		var exists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(exists);
	}

	[Fact]
	[TestPriority(2)]
	public void TablesCreated()
	{
		const string table1CreatedSql = """
			IF OBJECT_ID('[Test].[Table1]', 'U') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";

		using var connection = new SqlConnection(connectionString);
		connection.Open();

		using var command = new SqlCommand(table1CreatedSql, connection);
		command.ExecuteNonQuery();

		var tableCreated = Convert.ToBoolean(command.ExecuteScalar());
		Assert.True(tableCreated);

		const string table2CreatedSql = """
			IF OBJECT_ID('[Test].[Table2]', 'U') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";

		command.CommandText = table2CreatedSql;
		tableCreated = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(tableCreated);

		const string table3CreatedSql = """
			IF OBJECT_ID('[Test].[Table3]', 'U') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";

		command.CommandText = table3CreatedSql;
		tableCreated = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(tableCreated);

		const string table4CreatedSql = """
			IF OBJECT_ID('[Test].[Table4]', 'U') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";

		command.CommandText = table4CreatedSql;
		tableCreated = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(tableCreated);

		const string table5CreatedSql = """
			IF OBJECT_ID('[Funky].[Table5]', 'U') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";

		command.CommandText = table5CreatedSql;
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

		const string table1PrimaryKeyCreatedSql = "SELECT OBJECTPROPERTY(OBJECT_ID(N'[Test].[Table1]'),'TableHasPrimaryKey');";

		command.CommandText = table1PrimaryKeyCreatedSql;
		var primaryKeyCreated = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(primaryKeyCreated);

		const string table2PrimaryKeyCreatedSql = "SELECT OBJECTPROPERTY(OBJECT_ID(N'[Test].[Table2]'),'TableHasPrimaryKey');";

		command.CommandText = table2PrimaryKeyCreatedSql;
		primaryKeyCreated = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(primaryKeyCreated);

		const string table3PrimaryKeyCreatedSql = "SELECT OBJECTPROPERTY(OBJECT_ID(N'[Test].[Table3]'),'TableHasPrimaryKey');";

		command.CommandText = table3PrimaryKeyCreatedSql;
		primaryKeyCreated = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(primaryKeyCreated);

		const string table4PrimaryKeyCreatedSql = "SELECT OBJECTPROPERTY(OBJECT_ID(N'[Test].[Table4]'),'TableHasPrimaryKey');";

		command.CommandText = table4PrimaryKeyCreatedSql;
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

		const string table1ForeignKeyCreatedSql = """
			IF OBJECT_ID(N'[Test].[FK_Table4_Table1]', 'F') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";

		command.CommandText = table1ForeignKeyCreatedSql;
		var foreignKeyCreated = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(foreignKeyCreated);

		const string table2ForeignKeyCreatedSql = """
			IF OBJECT_ID(N'[Test].[FK_Table4_Table2]', 'F') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";

		command.CommandText = table2ForeignKeyCreatedSql;
		foreignKeyCreated = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(foreignKeyCreated);

		const string table3ForeignKeyCreatedSql = """
			IF OBJECT_ID(N'[Test].[FK_Table4_Table3]', 'F') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";

		command.CommandText = table3ForeignKeyCreatedSql;
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

		const string table1IndexCreatedSql = """
			IF INDEXPROPERTY(OBJECT_ID(N'[Test].[Table1]'), N'UIX_Table1_Guid', N'IndexID') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";

		command.CommandText = table1IndexCreatedSql;
		var indexCreated = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(indexCreated);

		const string table2IndexCreatedSql = """
			IF INDEXPROPERTY(OBJECT_ID(N'[Test].[Table2]'), N'IX_Table2_Date_Time', N'IndexID') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";

		command.CommandText = table2IndexCreatedSql;
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

		const string table5ColumnAddedSql = """
			IF COL_LENGTH(N'[Funky].[Table5]', N'Description') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END;
			""";

		command.CommandText = table5ColumnAddedSql;
		var columnAdded = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(columnAdded);

		const string table2ColumnAddedSql = """
			IF COL_LENGTH(N'[Test].[Table2]', N'Code') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END;
			""";

		command.CommandText = table2ColumnAddedSql;
		columnAdded = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(columnAdded);

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

		const string table3ColumnDeletedSql = """
			IF COL_LENGTH(N'[Test].[Table3]', N'FixedString') IS NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END;
			""";

		command.CommandText = table3ColumnDeletedSql;
		var columnDeleted = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(columnAdded);
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

		const string tableCreatedSql = """
			IF OBJECT_ID('[dbo].[Table5]', 'U') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";

		command.CommandText = tableCreatedSql;
		var tableCreated = Convert.ToBoolean(command.ExecuteScalar());

		Assert.True(tableCreated);
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

		const string table1IndexExistsSql = """
			IF INDEXPROPERTY(OBJECT_ID(N'[Test].[Table1]'), N'UIX_Table1_Guid_Renamed', N'IndexID') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";

		command.CommandText = table1IndexExistsSql;
		var indexExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(indexExists);

		const string table2IndexExistsSql = """
			IF INDEXPROPERTY(OBJECT_ID(N'[Test].[Table2]'), N'IX_Table2_Date_Time', N'IndexID') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";

		command.CommandText = table2IndexExistsSql;
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

		const string table1ForeignKeyExistsSql = """
			IF OBJECT_ID(N'[Test].[FK_Table4_Table1]', 'F') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";

		command.CommandText = table1ForeignKeyExistsSql;
		var foreignKeyExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(foreignKeyExists);

		const string table2ForeignKeyExistsSql = """
			IF OBJECT_ID(N'[Test].[FK_Table4_Table2]', 'F') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";

		command.CommandText = table2ForeignKeyExistsSql;
		foreignKeyExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(foreignKeyExists);

		const string table3ForeignKeyExistsSql = """
			IF OBJECT_ID(N'[Test].[FK_Table4_Table3]', 'F') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";

		command.CommandText = table3ForeignKeyExistsSql;
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

		const string table1PrimaryKeyExistsSql = "SELECT OBJECTPROPERTY(OBJECT_ID(N'[Test].[Table1]'),'TableHasPrimaryKey');";

		command.CommandText = table1PrimaryKeyExistsSql;
		var primaryKeyExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(primaryKeyExists);

		const string table2PrimaryKeyExistsSql = "SELECT OBJECTPROPERTY(OBJECT_ID(N'[Test].[Table2]'),'TableHasPrimaryKey');";

		command.CommandText = table2PrimaryKeyExistsSql;
		primaryKeyExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(primaryKeyExists);

		const string table3PrimaryKeyExistsSql = "SELECT OBJECTPROPERTY(OBJECT_ID(N'[Test].[Table3]'),'TableHasPrimaryKey');";

		command.CommandText = table3PrimaryKeyExistsSql;
		primaryKeyExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(primaryKeyExists);

		const string table4PrimaryKeyExistsSql = "SELECT OBJECTPROPERTY(OBJECT_ID(N'[Test].[Table4]'),'TableHasPrimaryKey');";

		command.CommandText = table4PrimaryKeyExistsSql;
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

		const string table1ExistsSql = """
			IF OBJECT_ID('[Test].[Table1]', 'U') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";

		command.CommandText = table1ExistsSql;
		var tableExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(tableExists);

		const string table2ExistsSql = """
			IF OBJECT_ID('[Test].[Table2]', 'U') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";

		command.CommandText = table2ExistsSql;
		tableExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(tableExists);

		const string table3ExistsSql = """
			IF OBJECT_ID('[Test].[Table3]', 'U') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";

		command.CommandText = table3ExistsSql;
		tableExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(tableExists);

		const string table4ExistsSql = """
			IF OBJECT_ID('[Test].[Table4]', 'U') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";

		command.CommandText = table4ExistsSql;
		tableExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(tableExists);

		const string table5ExistsSql = """
			IF OBJECT_ID('[dbo].[Table5]', 'U') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";

		command.CommandText = table5ExistsSql;
		tableExists = Convert.ToBoolean(command.ExecuteScalar());

		Assert.False(tableExists);

		const string table6ExistsSql = """
			IF OBJECT_ID('[Funky].[Table6]', 'U') IS NOT NULL
			BEGIN
				SELECT 1;
			END
			ELSE
			BEGIN
				SELECT 0;
			END
			""";

		command.CommandText = table6ExistsSql;
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
	private sealed class CreateTablesMigration : UpMigration
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
	private sealed class CreatePrimaryKeysMigration : UpMigration
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
	private sealed class CreateForeignKeysMigration : UpMigration
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
	private sealed class CreateIndexesMigration : UpMigration
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
	private sealed class AlterMigration : UpMigration
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
	private sealed class RenameMigration : UpMigration
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
	private sealed class SqlMigration : UpMigration
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
	private sealed class DropIndexesMigration : UpMigration
	{
		public override void Up()
		{
			Drop.Index("UIX_Table1_Guid_Renamed").FromTable("Table1").InSchema("Test");
			Drop.Index("IX_Table2_Date_Time").FromTable("Table2").InSchema("Test");
		}
	}

	[MigrationName("9_DropForeignKeysMigration")]
	private sealed class DropForeignKeysMigration : UpMigration
	{
		public override void Up()
		{
			Drop.Constraint("FK_Table4_Table1").FromTable("Table4").InSchema("Test");
			Drop.Constraint("FK_Table4_Table2").FromTable("Table4").InSchema("Test");
			Drop.Constraint("FK_Table4_Table3").FromTable("Table4").InSchema("Test");
		}
	}

	[MigrationName("10_DropPrimaryKeysMigration")]
	private sealed class DropPrimaryKeysMigration : UpMigration
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
	private sealed class DropTablesMigration : UpMigration
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
}