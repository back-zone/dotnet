using System.Collections.Concurrent;
using System.Data;
using back.zone.core.Monads.TryMonad;
using back.zone.core.Types;
using back.zone.storage.sqlite.Configuration;
using Microsoft.Data.Sqlite;

namespace back.zone.storage.sqlite.Services;

public sealed class SqliteService
{
    private readonly SqliteConfiguration _configuration;
    private readonly object _poolLock = new();
    private readonly ConcurrentQueue<SqliteConnection> _readConnections;
    private readonly SemaphoreSlim _writeConnectionLock = new(1, 1);
    private int _totalConnections;
    private SqliteConnection? _writeConnection;

    public SqliteService(SqliteConfiguration configuration)
    {
        _configuration = configuration;
        _readConnections = new ConcurrentQueue<SqliteConnection>();
        _totalConnections = 0;
    }

    private SqliteConnection CreateNewConnection()
    {
        var connection = new SqliteConnection(_configuration.ConnectionString);
        connection.Open();
        return connection;
    }

    internal async Task<SqliteConnection> GetReadConnectionAsync()
    {
        while (true)
        {
            if (_readConnections.TryDequeue(out var connection))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                return connection;
            }

            lock (_poolLock)
            {
                if (_totalConnections < _configuration.MaxPoolSize && _totalConnections >= _configuration.MinPoolSize)
                {
                    connection = CreateNewConnection();
                    _totalConnections++;
                    return connection;
                }
            }

            await Task.Delay(10);
        }
    }

    public async Task<Try<SqliteConnection>> AcquireReadConnection()
    {
        return await Try.Async(GetReadConnectionAsync());
    }

    internal async Task<SqliteConnection> GetWriteConnectionAsync()
    {
        await _writeConnectionLock.WaitAsync();
        if (_writeConnection is null || _writeConnection.State != ConnectionState.Open)
            _writeConnection = CreateNewConnection();
        return _writeConnection;
    }

    public async Task<Try<SqliteConnection>> AcquireWriteConnection()
    {
        return await Try.Async(GetWriteConnectionAsync());
    }

    public async Task<Try<TA>> RunReadQuery<TA>(
        Continuation<SqliteConnection, TA> continuation
    )
        where TA : notnull
    {
        var connection = await GetReadConnectionAsync().ConfigureAwait(false);

        try
        {
            return continuation(connection);
        }
        catch (Exception e)
        {
            return e;
        }
        finally
        {
            ReleaseReadConnection(connection);
        }
    }

    public async Task<Try<TA>> RunReadQueryAsync<TA>(
        Continuation<SqliteConnection, Task<TA>> continuation
    )
        where TA : notnull
    {
        var connection = await GetReadConnectionAsync().ConfigureAwait(false);

        try
        {
            return await continuation(connection).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
        finally
        {
            ReleaseReadConnection(connection);
        }
    }

    public async Task<Try<TA>> RunWriteQuery<TA>(
        Continuation<SqliteConnection, TA> continuation
    )
        where TA : notnull
    {
        var connection = await GetWriteConnectionAsync().ConfigureAwait(false);

        try
        {
            return continuation(connection);
        }
        catch (Exception e)
        {
            return e;
        }
        finally
        {
            ReleaseWriteConnection();
        }
    }

    public async Task<Try<TA>> RunWriteQueryAsync<TA>(
        Continuation<SqliteConnection, Task<TA>> continuation
    )
        where TA : notnull
    {
        var connection = await GetWriteConnectionAsync();

        try
        {
            return await continuation(connection).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return e;
        }
        finally
        {
            ReleaseWriteConnection();
        }
    }

    public void ReleaseReadConnection(SqliteConnection connection)
    {
        if (connection.State != ConnectionState.Open)
            connection.Open();
        _readConnections.Enqueue(connection);
    }

    public void ReleaseWriteConnection()
    {
        _writeConnectionLock.Release();
    }
}