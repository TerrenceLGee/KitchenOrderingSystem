using Auth.Infrastructure.Persistence;

using Microsoft.Data.Sqlite;

namespace Auth.UnitTests.Application.Helpers;

public sealed class ConnectionHelper(ApplicationDbContext context, SqliteConnection connection) : IAsyncDisposable
{
    public ApplicationDbContext Context { get; } = context;
    public SqliteConnection Connection { get; } = connection;

    public async ValueTask DisposeAsync()
    {
        await Context.DisposeAsync();
        await Connection.DisposeAsync();
    }
}