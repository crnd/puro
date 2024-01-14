using Xunit;

namespace Puro.Tests.Statements.Create;

public class CreateTableTests
{
	[Fact]
	public void NullTableNameThrows()
	{
		var migration = new NullTableNameMigration();

		Assert.Throws<ArgumentNullException>(() => migration.Up());
	}

	private sealed class NullTableNameMigration : UpMigration
	{
		public override void Up()
		{
			Create.Table(null!).InSchema("schema")
				.WithColumn("Id").AsInt().Identity();
		}
	}

	[Fact]
	public void NullSchemaNameThrows()
	{
		var migration = new NullSchemaNameMigration();

		Assert.Throws<ArgumentNullException>(() => migration.Up());
	}

	private sealed class NullSchemaNameMigration : UpMigration
	{
		public override void Up()
		{
			Create.Table("table").InSchema(null!)
				.WithColumn("Id").AsInt().Identity();
		}
	}

	[Fact]
	public void NullColumnNameThrows()
	{
		var migration = new NullColumnNameMigration();

		Assert.Throws<ArgumentNullException>(() => migration.Up());
	}

	private sealed class NullColumnNameMigration : UpMigration
	{
		public override void Up()
		{
			Create.Table("table").InSchema("schema")
				.WithColumn(null!).AsInt().Identity();
		}
	}

	[Fact]
	public void EmptyTableNameThrows()
	{
		var migration = new EmptyTableNameMigration();

		Assert.Throws<ArgumentNullException>(() => migration.Up());
	}

	private sealed class EmptyTableNameMigration : UpMigration
	{
		public override void Up()
		{
			Create.Table(string.Empty).InSchema("schema")
				.WithColumn("Id").AsInt().Identity();
		}
	}

	[Fact]
	public void EmptySchemaNameThrows()
	{
		var migration = new EmptySchemaNameMigration();

		Assert.Throws<ArgumentNullException>(() => migration.Up());
	}

	private sealed class EmptySchemaNameMigration : UpMigration
	{
		public override void Up()
		{
			Create.Table("table").InSchema(string.Empty)
				.WithColumn("Id").AsInt().Identity();
		}
	}

	[Fact]
	public void EmptyColumnNameThrows()
	{
		var migration = new EmptyColumnNameMigration();

		Assert.Throws<ArgumentNullException>(() => migration.Up());
	}

	private sealed class EmptyColumnNameMigration : UpMigration
	{
		public override void Up()
		{
			Create.Table("table").InSchema("schema")
				.WithColumn(string.Empty).AsInt().Identity();
		}
	}

	[Fact]
	public void WhiteSpaceTableNameThrows()
	{
		var migration = new WhiteSpaceTableNameMigration();

		Assert.Throws<ArgumentNullException>(() => migration.Up());
	}

	private sealed class WhiteSpaceTableNameMigration : UpMigration
	{
		public override void Up()
		{
			Create.Table("     ").InSchema("schema")
				.WithColumn("Id").AsInt().Identity();
		}
	}

	[Fact]
	public void WhiteSpaceSchemaNameThrows()
	{
		var migration = new WhiteSpaceSchemaNameMigration();

		Assert.Throws<ArgumentNullException>(() => migration.Up());
	}

	private sealed class WhiteSpaceSchemaNameMigration : UpMigration
	{
		public override void Up()
		{
			Create.Table("table").InSchema("     ")
				.WithColumn("Id").AsInt().Identity();
		}
	}

	[Fact]
	public void WhiteSpaceColumnNameThrows()
	{
		var migration = new WhiteSpaceColumnNameMigration();

		Assert.Throws<ArgumentNullException>(() => migration.Up());
	}

	private sealed class WhiteSpaceColumnNameMigration : UpMigration
	{
		public override void Up()
		{
			Create.Table("table").InSchema("schema")
				.WithColumn("     ").AsInt().Identity();
		}
	}
}
