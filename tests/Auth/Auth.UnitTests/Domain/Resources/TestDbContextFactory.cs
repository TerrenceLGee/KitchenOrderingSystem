using Auth.Infrastructure.Persistence;
using Auth.UnitTests.Application.Helpers;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Auth.UnitTests.Domain.Resources;

public static class TestDbContextFactory
{
    public static ConnectionHelper Create()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(connection)
            .Options;

        var context = new ApplicationDbContext(options);

        context.Database.EnsureCreated();

        return new ConnectionHelper(context, connection);
    }
}