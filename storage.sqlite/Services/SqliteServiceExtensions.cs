using back.zone.core.Monads.TryMonad;
using back.zone.core.Types;
using back.zone.storage.sqlite.Providers;
using Microsoft.Data.Sqlite;

namespace back.zone.storage.sqlite.Services;

public static class SqliteServiceExtensions
{
    public static Try<TA> RunQuery<TA>(
        this Try<SqliteConnection> connection,
        Continuation<SqliteConnection, TA> continuation)
        where TA : notnull
    {
        try
        {
            return connection.TryGetValue(out var conn)
                ? continuation(conn)
                : connection.TryGetException(out var exception)
                    ? exception
                    : new Exception("#failed_to_acquire_connection#");
        }
        catch (Exception e)
        {
            return e;
        }
        finally
        {
            if (connection.TryGetValue(out var conn)) SqliteServiceProvider.Get().ReleaseReadConnection(conn);
        }
    }

    public static async Task<Try<TA>> RunQueryAsync<TA>(
        this Try<SqliteConnection> connection,
        Continuation<SqliteConnection, Task<TA>> continuation
    )
        where TA : notnull
    {
        try
        {
            return connection.TryGetValue(out var conn)
                ? await continuation(conn).ConfigureAwait(false)
                : connection.TryGetException(out var exception)
                    ? exception
                    : new Exception("#failed_to_acquire_connection#");
        }
        catch (Exception e)
        {
            return e;
        }
        finally
        {
            if (connection.TryGetValue(out var conn)) SqliteServiceProvider.Get().ReleaseReadConnection(conn);
        }
    }

    public static async Task<Try<TA>> RunQueryAsync<TA>(
        this Task<Try<SqliteConnection>> connectionAsync,
        Continuation<SqliteConnection, Task<TA>> continuation
    )
        where TA : notnull
    {
        try
        {
            var connection = await connectionAsync.ConfigureAwait(false);
            return connection.TryGetValue(out var conn)
                ? await continuation(conn).ConfigureAwait(false)
                : connection.TryGetException(out var exception)
                    ? exception
                    : new Exception("#failed_to_acquire_connection#");
        }
        catch (Exception e)
        {
            return e;
        }
        finally
        {
            if ((await connectionAsync.ConfigureAwait(false)).TryGetValue(out var conn))
                SqliteServiceProvider.Get().ReleaseReadConnection(conn);
        }
    }

    public static async Task<Try<TA>> RunQueryAsync<TA>(
        this Task<Try<SqliteConnection>> connectionAsync,
        Continuation<SqliteConnection, TA> continuation
    )
        where TA : notnull
    {
        try
        {
            var connection = await connectionAsync.ConfigureAwait(false);
            return connection.TryGetValue(out var conn)
                ? continuation(conn)
                : connection.TryGetException(out var exception)
                    ? exception
                    : new Exception("#failed_to_acquire_connection#");
        }
        catch (Exception e)
        {
            return e;
        }
        finally
        {
            if ((await connectionAsync.ConfigureAwait(false)).TryGetValue(out var conn))
                SqliteServiceProvider.Get().ReleaseReadConnection(conn);
        }
    }

    public static Try<TA> RunQuery<TA, TP>(
        this Try<(TP parameters, SqliteConnection connection)> tuple,
        Zipper<TP, SqliteConnection, TA> continuation
    )
        where TA : notnull
        where TP : notnull
    {
        try
        {
            return tuple.TryGetValue(out var flattenedTuple)
                ? continuation(
                    flattenedTuple.parameters,
                    flattenedTuple.connection
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

    public static async Task<Try<TA>> RunQueryAsync<TA, TP>(
        this Try<(TP parameters, SqliteConnection connection)> tuple,
        Zipper<TP, SqliteConnection, Task<TA>> continuation
    )
        where TA : notnull
        where TP : notnull
    {
        try
        {
            return tuple.TryGetValue(out var flattenedTuple)
                ? await continuation(
                    flattenedTuple.parameters,
                    flattenedTuple.connection
                ).ConfigureAwait(false)
                : tuple.TryGetException(out var exception)
                    ? exception
                    : new Exception("#failed_to_acquire_connection#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TA>> RunQueryAsync<TA, TP>(
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
                ? await continuation(
                    flattenedTuple.parameters,
                    flattenedTuple.connection
                ).ConfigureAwait(false)
                : tuple.TryGetException(out var exception)
                    ? exception
                    : new Exception("#failed_to_acquire_connection#");
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<Try<TA>> RunQueryAsync<TA, TP>(
        this Task<Try<(TP parameters, SqliteConnection connection)>> asyncTuple,
        Zipper<TP, SqliteConnection, TA> continuation
    )
        where TA : notnull
        where TP : notnull
    {
        try
        {
            var tuple = await asyncTuple.ConfigureAwait(false);

            return tuple.TryGetValue(out var flattenedTuple)
                ? continuation(
                    flattenedTuple.parameters,
                    flattenedTuple.connection
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

    public static Try<TA> ReleaseReadConnection<TA>(
        this Try<(SqliteConnection connection, TA value)> tuple
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
                SqliteServiceProvider.Get().ReleaseReadConnection(flattenedTuple.connection);
        }
    }

    public static async Task<Try<TA>> ReleaseReadConnectionAsync<TA>(
        this Task<Try<(SqliteConnection connection, TA value)>> asyncTuple
    )
        where TA : notnull
    {
        try
        {
            var tuple = await asyncTuple.ConfigureAwait(false);
            return tuple.TryGetValue(out var flattenedTuple)
                ? Try.Succeed(flattenedTuple.value)
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
                SqliteServiceProvider.Get().ReleaseReadConnection(flattenedTuple.connection);
        }
    }

    public static Try<TA> ReleaseWriteConnection<TA>(
        this Try<(SqliteConnection connection, TA value)> tuple
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
            SqliteServiceProvider.Get().ReleaseWriteConnection();
        }
    }

    public static async Task<Try<TA>> ReleaseWriteConnectionAsync<TA>(
        this Task<Try<(SqliteConnection connection, TA value)>> asyncTuple
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
            SqliteServiceProvider.Get().ReleaseWriteConnection();
        }
    }
}