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

    private async Task<SqliteConnection> GetReadConnectionAsync()
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
                if (_totalConnections < _configuration.MaxPoolSize)
                {
                    connection = CreateNewConnection();
                    _totalConnections++;
                    return connection;
                }
            }

            await Task.Delay(10);
        }
    }

    private async Task<SqliteConnection> GetWriteConnectionAsync()
    {
        await _writeConnectionLock.WaitAsync();
        if (_writeConnection is null || _writeConnection.State != ConnectionState.Open)
            _writeConnection = CreateNewConnection();
        return _writeConnection;
    }

    public async Task<Try<TA>> RunReadOperation<TA>(Continuation<SqliteConnection, TA> continuation)
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

    public async Task<Try<TA>> RunReadOperationAsync<TA>(
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

    public async Task<Try<TA>> RunWriteOperation<TA>(
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

    public async Task<Try<TA>> RunWriteOperationAsync<TA>(
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