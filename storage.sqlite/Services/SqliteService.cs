using System.Collections.Concurrent;
using System.Data;
using back.zone.core.Monads.TryMonad;
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
        PopulateConnections();
    }

    private void PopulateConnections()
    {
        while (_totalConnections < _configuration.MinPoolSize)
            lock (_poolLock)
            {
                _readConnections.Enqueue(CreateNewConnection());
                _totalConnections++;
            }
    }

    private SqliteConnection CreateNewConnection()
    {
        var connection = new SqliteConnection(_configuration.ConnectionString);
        connection.Open();
        return connection;
    }

    private SqliteConnection GetReadConnection()
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
                if (_totalConnections >= _configuration.MaxPoolSize) continue;
                connection = CreateNewConnection();
                _totalConnections++;
                return connection;
            }
        }
    }

    public Try<SqliteConnection> AcquireReadConnection()
    {
        return Try.Succeed(GetReadConnection());
    }

    private async Task<SqliteConnection> GetWriteConnectionAsync()
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