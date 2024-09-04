using back.zone.core.Monads.TryMonad;
using back.zone.core.Types;
using Microsoft.Data.Sqlite;

namespace back.zone.storage.sqlite.Services;

public static class SqliteServiceExtensions
{
    public static Try<(SqliteConnection, TA)> RunQuery<TA, TP>(
        this Try<(TP parameters, SqliteConnection connection)> tuple,
        Zipper<TP, SqliteConnection, TA> continuation
    )
        where TA : notnull
        where TP : notnull
    {
        try
        {
            return tuple.TryGetValue(out var flattenedTuple)
                ? (flattenedTuple.connection,
                    continuation(
                        flattenedTuple.parameters,
                        flattenedTuple.connection
                    )
                )
                : tuple.TryGetException(out var exception)
                    ? exception
                    : new Exception("#failed_to_acquire_connection#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<(SqliteConnection, TA)>> RunQueryAsync<TA, TP>(
        this Task<Try<(TP parameters, SqliteConnection connection)>> asyncTuple,
        Zipper<TP, SqliteConnection, Task<TA>> continuation
    )
        where TA : notnull
        where TP : notnull
    {
        try
        {
            var tuple = await asyncTuple.ConfigureAwait(false);

            return tuple.TryGetValue(out var flattenedTuple)
                ? (flattenedTuple.connection,
                    await continuation(
                        flattenedTuple.parameters,
                        flattenedTuple.connection))
                : tuple.TryGetException(out var exception)
                    ? exception
                    : new Exception("#failed_to_acquire_connection#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static Try<TA> ReleaseReadConnection<TA>(
        this Try<(SqliteConnection connection, TA value)> tuple,
        SqliteService sqliteService
    )
        where TA : notnull
    {
        try
        {
            return tuple.TryGetValue(out var flattenedTuple)
                ? flattenedTuple.value
                : tuple.TryGetException(out var exception)
                    ? exception
                    : new Exception("#failed_to_release_connection#");
        }
        catch (Exception e)
        {
            return e;
        }
        finally
        {
            if (tuple.TryGetValue(out var flattenedTuple))
                sqliteService.ReleaseReadConnection(flattenedTuple.connection);
        }
    }

    public static async Task<Try<TA>> ReleaseReadConnectionAsync<TA>(
        this Task<Try<(SqliteConnection connection, TA value)>> asyncTuple,
        SqliteService sqliteService
    )
        where TA : notnull
    {
        try
        {
            var tuple = await asyncTuple.ConfigureAwait(false);
            return tuple.TryGetValue(out var flattenedTuple)
                ? flattenedTuple.value
                : tuple.TryGetException(out var exception)
                    ? exception
                    : new Exception("#failed_to_release_connection#");
        }
        catch (Exception e)
        {
            return e;
        }
        finally
        {
            if ((await asyncTuple.ConfigureAwait(false)).TryGetValue(out var flattenedTuple))
                sqliteService.ReleaseReadConnection(flattenedTuple.connection);
        }
    }

    public static Try<TA> ReleaseWriteConnection<TA>(
        this Try<(SqliteConnection connection, TA value)> tuple,
        SqliteService sqliteService
    )
        where TA : notnull
    {
        try
        {
            return tuple.TryGetValue(out var flattenedTuple)
                ? flattenedTuple.value
                : tuple.TryGetException(out var exception)
                    ? exception
                    : new Exception("#failed_to_release_connection#");
        }
        catch (Exception e)
        {
            return e;
        }
        finally
        {
            sqliteService.ReleaseWriteConnection();
        }
    }

    public static async Task<Try<TA>> ReleaseWriteConnectionAsync<TA>(
        this Task<Try<(SqliteConnection connection, TA value)>> asyncTuple,
        SqliteService sqliteService
    )
        where TA : notnull
    {
        try
        {
            var tuple = await asyncTuple.ConfigureAwait(false);

            return tuple.TryGetValue(out var flattenedTuple)
                ? flattenedTuple.value
                : tuple.TryGetException(out var exception)
                    ? exception
                    : new Exception("#failed_to_release_connection#");
        }
        catch (Exception e)
        {
            return e;
        }
        finally
        {
            sqliteService.ReleaseWriteConnection();
        }
    }
}