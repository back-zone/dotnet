using back.zone.core.Monads.TryMonad;
using back.zone.core.Types;
using back.zone.storage.sqlite.Providers;
using Microsoft.Data.Sqlite;

namespace back.zone.storage.sqlite.Services;

public static class SqliteServiceExtensions
{
    /// <summary>
    ///     Executes a synchronous query on the provided SQLite connection using the given continuation function.
    /// </summary>
    /// <typeparam name="TA">The type of the result.</typeparam>
    /// <param name="connection">The Try monad containing the SQLite connection.</param>
    /// <param name="continuation">The continuation function to execute on the connection.</param>
    /// <returns>A Try monad containing the result of the query or an exception if an error occurs.</returns>
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

    /// <summary>
    ///     Executes an asynchronous query on the provided SQLite connection using the given continuation function.
    /// </summary>
    /// <typeparam name="TA">The type of the result.</typeparam>
    /// <param name="connection">The Try monad containing the SQLite connection.</param>
    /// <param name="continuation">The continuation function to execute on the connection.</param>
    /// <returns>
    ///     A Try monad containing the result of the query or an exception if an error occurs.
    ///     The connection is released after the query execution.
    /// </returns>
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

    /// <summary>
    ///     Executes an asynchronous query on the provided SQLite connection using the given continuation function.
    /// </summary>
    /// <typeparam name="TA">The type of the result.</typeparam>
    /// <param name="connectionAsync">
    ///     A task that represents the Try monad containing the SQLite connection.
    ///     This method will await the task before proceeding.
    /// </param>
    /// <param name="continuation">
    ///     The continuation function to execute on the connection.
    ///     This function should return a task representing the result of the query.
    /// </param>
    /// <returns>
    ///     A Try monad containing the result of the query or an exception if an error occurs.
    ///     The connection is released after the query execution.
    /// </returns>
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

    /// <summary>
    ///     Executes an asynchronous query on the provided SQLite connection using the given continuation function.
    /// </summary>
    /// <typeparam name="TA">The type of the result.</typeparam>
    /// <param name="connectionAsync">
    ///     A task that represents the Try monad containing the SQLite connection.
    ///     This method will await the task before proceeding.
    /// </param>
    /// <param name="continuation">
    ///     The continuation function to execute on the connection.
    ///     This function should return a value representing the result of the query.
    /// </param>
    /// <returns>
    ///     A Try monad containing the result of the query or an exception if an error occurs.
    ///     The connection is released after the query execution.
    /// </returns>
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

    /// <summary>
    ///     Executes a synchronous query on the provided SQLite connection using the given continuation function.
    /// </summary>
    /// <typeparam name="TA">The type of the result.</typeparam>
    /// <typeparam name="TP">The type of the parameters.</typeparam>
    /// <param name="tuple">
    ///     A Try monad containing a tuple of parameters and an SQLite connection.
    ///     The function will attempt to extract these values and use them in the continuation function.
    /// </param>
    /// <param name="continuation">
    ///     The continuation function to execute on the connection.
    ///     This function should accept the parameters and connection as input and return a value of type TA.
    /// </param>
    /// <returns>
    ///     A Try monad containing the result of the query or an exception if an error occurs.
    ///     The connection is not released after the query execution.
    /// </returns>
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

    /// <summary>
    ///     Executes an asynchronous query on the provided SQLite connection using the given continuation function.
    /// </summary>
    /// <typeparam name="TA">The type of the result.</typeparam>
    /// <typeparam name="TP">The type of the parameters.</typeparam>
    /// <param name="tuple">
    ///     A Try monad containing a tuple of parameters and an SQLite connection.
    ///     The function will attempt to extract these values and use them in the continuation function.
    /// </param>
    /// <param name="continuation">
    ///     The continuation function to execute on the connection.
    ///     This function should accept the parameters and connection as input and return a task representing the result of
    ///     type TA.
    /// </param>
    /// <returns>
    ///     A Try monad containing the result of the query or an exception if an error occurs.
    ///     The connection is not released after the query execution.
    /// </returns>
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

    /// <summary>
    ///     Executes an asynchronous query on the provided SQLite connection using the given continuation function.
    /// </summary>
    /// <typeparam name="TA">The type of the result.</typeparam>
    /// <typeparam name="TP">The type of the parameters.</typeparam>
    /// <param name="asyncTuple">
    ///     A task that represents a Try monad containing a tuple of parameters and an SQLite connection.
    ///     The function will attempt to extract these values and use them in the continuation function.
    /// </param>
    /// <param name="continuation">
    ///     The continuation function to execute on the connection.
    ///     This function should accept the parameters and connection as input and return a task representing the result of
    ///     type TA.
    /// </param>
    /// <returns>
    ///     A Try monad containing the result of the query or an exception if an error occurs.
    ///     The connection is not released after the query execution.
    /// </returns>
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

    /// <summary>
    ///     Executes an asynchronous query on the provided SQLite connection using the given continuation function.
    /// </summary>
    /// <typeparam name="TA">The type of the result.</typeparam>
    /// <typeparam name="TP">The type of the parameters.</typeparam>
    /// <param name="asyncTuple">
    ///     A task that represents a Try monad containing a tuple of parameters and an SQLite connection.
    ///     The function will attempt to extract these values and use them in the continuation function.
    /// </param>
    /// <param name="continuation">
    ///     The continuation function to execute on the connection.
    ///     This function should accept the parameters and connection as input and return a value of type TA.
    /// </param>
    /// <returns>
    ///     A Try monad containing the result of the query or an exception if an error occurs.
    ///     The connection is not released after the query execution.
    /// </returns>
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

    /// <summary>
    ///     Releases a read connection from the SQLite service provider and returns the result.
    /// </summary>
    /// <typeparam name="TA">The type of the result.</typeparam>
    /// <param name="tuple">
    ///     A Try monad containing a tuple of an SQLite connection and a value of type TA.
    ///     The function will attempt to extract these values and release the connection.
    /// </param>
    /// <returns>
    ///     A Try monad containing the result of type TA or an exception if an error occurs during the release process.
    ///     The connection is released after the result is returned.
    /// </returns>
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

    /// <summary>
    ///     Asynchronously releases a read connection from the SQLite service provider and returns the result.
    /// </summary>
    /// <typeparam name="TA">The type of the result.</typeparam>
    /// <param name="asyncTuple">
    ///     A task that represents a Try monad containing a tuple of an SQLite connection and a value of type TA.
    ///     The function will attempt to extract these values and release the connection.
    /// </param>
    /// <returns>
    ///     A Try monad containing the result of type TA or an exception if an error occurs during the release process.
    ///     The connection is released after the result is returned.
    /// </returns>
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

    /// <summary>
    ///     Releases a write connection from the SQLite service provider and returns the result.
    /// </summary>
    /// <typeparam name="TA">The type of the result.</typeparam>
    /// <param name="tuple">
    ///     A Try monad containing a tuple of an SQLite connection and a value of type TA.
    ///     The function will attempt to extract these values and release the connection.
    /// </param>
    /// <returns>
    ///     A Try monad containing the result of type TA or an exception if an error occurs during the release process.
    ///     The connection is released after the result is returned.
    /// </returns>
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

    /// <summary>
    ///     Asynchronously releases a write connection from the SQLite service provider and returns the result.
    /// </summary>
    /// <typeparam name="TA">The type of the result.</typeparam>
    /// <param name="asyncTuple">
    ///     A task that represents a Try monad containing a tuple of an SQLite connection and a value of type TA.
    ///     The function will attempt to extract these values and release the connection.
    /// </param>
    /// <returns>
    ///     A Try monad containing the result of type TA or an exception if an error occurs during the release process.
    ///     The connection is released after the result is returned.
    /// </returns>
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