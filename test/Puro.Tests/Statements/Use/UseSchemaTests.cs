using Puro.Exceptions;
using Xunit;

namespace Puro.Tests.Statements.Use;

public class UseSchemaTests
{
	[Fact]
	public void NullSchemaThrows()
	{
		var migration = new NullSchemaMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class NullSchemaMigration : UpMigration
	{
		public override void Up()
		{
			Use.Schema(null!);
		}
	}

	[Fact]
	public void EmptySchemaThrows()
	{
		var migration = new EmptySchemaMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class EmptySchemaMigration : UpMigration
	{
		public override void Up()
		{
			Use.Schema(string.Empty);
		}
	}

	[Fact]
	public void WhiteSpaceSchemaThrows()
	{
		var migration = new WhiteSpaceSchemaMigration();

		Assert.Throws<ArgumentNullException>(migration.Up);
	}

	private sealed class WhiteSpaceSchemaMigration : UpMigration
	{
		public override void Up()
		{
			Use.Schema("     ");
		}
	}

	[Fact]
	public void MultipleUseSchemaStatementsThrows()
	{
		var migration = new MultipleUseSchemaMigration();

		Assert.Throws<InvalidUseSchemaStatementException>(migration.Up);
	}

	private sealed class MultipleUseSchemaMigration : UpMigration
	{
		public override void Up()
		{
			Use.Schema("Transportation");
			Use.Schema("Transportation");
		}
	}

	[Fact]
	public void UseSchemaAfterOtherStatementThrows()
	{
		var migration = new UseSchemaAfterOtherStatementMigration();

		Assert.Throws<InvalidUseSchemaStatementException>(migration.Up);
	}

	private sealed class UseSchemaAfterOtherStatementMigration : UpMigration
	{
		public override void Up()
		{
			Drop.Table("table").InSchema("schema");

			Use.Schema("Transportation");
		}
	}

	[Fact]
	public void UseSchemaSetsSchema()
	{
		var migration = new UseSchemaMigration();
		migration.Up();

		Assert.Equal("Transportation", migration.Schema);
	}

	private sealed class UseSchemaMigration : UpMigration
	{
		public override void Up()
		{
			Use.Schema("Transportation");
		}
	}

	[Fact]
	public void UseSchemaBeforeOtherStatementsSetsSchema()
	{
		var migration = new UseSchemaBeforeOtherStatementsMigration();
		migration.Up();

		Assert.Equal("Accounting", migration.Schema);
	}

	private sealed class UseSchemaBeforeOtherStatementsMigration : UpMigration
	{
		public override void Up()
		{
			Use.Schema("Accounting");

			Create.Table("Customer").InSchema("Accounting")
				.WithColumn("Id").AsInt().Identity()
				.WithColumn("Name").AsString().MaximumLength(50).NotNull();

			Create.PrimaryKey("PK_Customer")
				.OnTable("Customer").InSchema("Accounting").WithColumn("Id");
		}
	}
}
