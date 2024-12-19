using Microsoft.Data.SqlClient;
using Testcontainers.MsSql;
using Xunit;

namespace Puro.SqlServer.Runner.Tests.Integration;

public class ContainerFixture : IAsyncLifetime
{
	public readonly MsSqlContainer container = new MsSqlBuilder().Build();

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

	public Task DisposeAsync()
	{
		return container.DisposeAsync().AsTask();
	}
}
