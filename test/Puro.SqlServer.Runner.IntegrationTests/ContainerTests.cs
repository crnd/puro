using Microsoft.Data.SqlClient;
using Puro.SqlServer.Runner.Generators;
using Testcontainers.MsSql;

namespace Puro.SqlServer.Runner.IntegrationTests;

public abstract class ContainerTests : IAsyncLifetime
{
	private readonly MsSqlContainer container;

	public ContainerTests(MsSqlContainer container)
	{
		this.container = container;
	}

	public async Task InitializeAsync()
	{
		await container.StartAsync();

		using var connection = new SqlConnection(container.GetConnectionString());
		connection.Open();

		using var command = new SqlCommand("CREATE SCHEMA [Test];", connection);
		command.ExecuteNonQuery();

		command.CommandText = "CREATE SCHEMA [Funky];";
		command.ExecuteNonQuery();
	}

	public Task DisposeAsync() => container.DisposeAsync().AsTask();

	[Fact]
	public void MigrationsTableCreated()
	{
		var migrations = MigrationsProcessor.Prepare([typeof(SqlMigration)], null, null);
		var migrationSql = MigrationSqlGenerator.Generate(migrations);

		using var connection = new SqlConnection(container.GetConnectionString());
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
	public void MigrationNamesInsertedInOrder()
	{
		var migrations = MigrationsProcessor.Prepare([typeof(CreateMigration), typeof(AlterMigration)], null, null);
		var migrationSql = MigrationSqlGenerator.Generate(migrations);

		using var connection = new SqlConnection(container.GetConnectionString());
		connection.Open();

		using var command = new SqlCommand(migrationSql, connection);
		command.ExecuteNonQuery();

		const string migrationsNamesSql = """
			SELECT [MigrationName]
			FROM [dbo].[__PuroMigrationsHistory]
			ORDER BY [AppliedOn] ASC;
			""";

		command.CommandText = migrationsNamesSql;
		var reader = command.ExecuteReader();
		var names = new List<string>(2);
		while (reader.Read())
		{
			names.Add(reader.GetFieldValue<string>(0));
		}

		Assert.StrictEqual(2, names.Count);
		Assert.Equal("1_CreateMigration", names[0]);
		Assert.Equal("2_AlterMigration", names[1]);
	}

	[Fact]
	public void StatementsCreateValidSql()
	{
		var migrations = MigrationsProcessor.Prepare(
			[typeof(CreateMigration), typeof(AlterMigration), typeof(RenameMigration), typeof(SqlMigration), typeof(DropMigration)],
			null,
			null);
		var sql = MigrationSqlGenerator.Generate(migrations);

		using var connection = new SqlConnection(container.GetConnectionString());
		using var command = new SqlCommand(sql, connection);

		connection.Open();
		command.ExecuteNonQuery();
	}

	public sealed class MsSqlDefaultConfiguration : ContainerTests
	{
		public MsSqlDefaultConfiguration()
			: base(new MsSqlBuilder().Build())
		{
		}
	}

	[MigrationName("1_CreateMigration")]
	private sealed class CreateMigration : UpMigration
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

			Create.PrimaryKey("PK_Table1")
				.OnTable("Table1")
				.WithColumn("Id");

			Create.UniqueIndex("UIX_Table1_Guid")
				.OnTable("Table1")
				.OnColumn("Guid").Ascending();

			Create.Table("Table2")
				.WithColumn("Id").AsInt().Identity()
				.WithColumn("Date").AsDate().Null()
				.WithColumn("Time").AsTime().Null()
				.WithColumn("DateTime").AsDateTime().Null()
				.WithColumn("Offset").AsDateTimeOffset().Null();

			Create.PrimaryKey("PK_Table2")
				.OnTable("Table2")
				.WithColumn("Id");

			Create.Index("IX_Table2_Date_Time")
				.OnTable("Table2")
				.OnColumn("Date").Ascending()
				.OnColumn("Time").Ascending();

			Create.Table("Table3")
				.WithColumn("Id").AsLong().Identity()
				.WithColumn("String").AsString().Null()
				.WithColumn("MaxString").AsString().MaximumLength(150).NotNull()
				.WithColumn("FixedString").AsString().FixedLength(10).Null();

			Create.PrimaryKey("PK_Table3")
				.OnTable("Table3")
				.WithColumn("Id");

			Create.Table("Table4")
				.WithColumn("Table1Id").AsShort().NotNull()
				.WithColumn("Table2Id").AsInt().NotNull()
				.WithColumn("Table3Id").AsLong().Null();

			Create.PrimaryKey("PK_Table4")
				.OnTable("Table4")
				.WithColumn("Table1Id")
				.WithColumn("Table2Id");

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

			Create.Table("Table5")
				.InSchema("Funky")
				.WithColumn("Id").AsInt().Identity()
				.WithColumn("Name").AsString().MaximumLength(25).NotNull();
		}
	}

	[MigrationName("2_AlterMigration")]
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

	[MigrationName("3_RenameMigration")]
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

	[MigrationName("4_SqlMigration")]
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

	[MigrationName("5_DropMigration")]
	private sealed class DropMigration : UpMigration
	{
		public override void Up()
		{
			Drop.Index("UIX_Table1_Guid_Renamed").FromTable("Table1").InSchema("Test");
			Drop.Index("IX_Table2_Date_Time").FromTable("Table2").InSchema("Test");

			Drop.Constraint("FK_Table4_Table1").FromTable("Table4").InSchema("Test");
			Drop.Constraint("FK_Table4_Table2").FromTable("Table4").InSchema("Test");
			Drop.Constraint("FK_Table4_Table3").FromTable("Table4").InSchema("Test");

			Drop.Constraint("PK_Table1").FromTable("Table1").InSchema("Test");
			Drop.Constraint("PK_Table2").FromTable("Table2").InSchema("Test");
			Drop.Constraint("PK_Table3").FromTable("Table3").InSchema("Test");
			Drop.Constraint("PK_Table4").FromTable("Table4").InSchema("Test");

			Drop.Table("Table1").InSchema("Test");
			Drop.Table("Table2").InSchema("Test");
			Drop.Table("Table3").InSchema("Test");
			Drop.Table("Table4").InSchema("Test");
			Drop.Table("Table5");
			Drop.Table("Table6").InSchema("Funky");
		}
	}
}