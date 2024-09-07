using System.Collections.Concurrent;
using System.Data;
using back.zone.core.Monads.TryMonad;
using back.zone.storage.sqlite.Configuration;
using Microsoft.Data.Sqlite;

namespace back.zone.storage.sqlite.Services;

/// <summary>
///     Represents a service for managing SQLite connections.
/// </summary>
public sealed class SqliteService
{
    private readonly SqliteConfiguration _configuration;
    private readonly object _poolLock = new();
    private readonly ConcurrentQueue<SqliteConnection> _readConnections;
    private readonly SemaphoreSlim _writeConnectionLock = new(1, 1);
    private int _totalConnections;
    private SqliteConnection? _writeConnection;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SqliteService" /> class.
    ///     This class provides methods for managing SQLite connections.
    /// </summary>
    /// <param name="configuration">
    ///     The configuration settings for the SQLite service.
    /// </param>
    public SqliteService(SqliteConfiguration configuration)
    {
        _configuration = configuration;
        _readConnections = new ConcurrentQueue<SqliteConnection>();
        _totalConnections = 0;
        PopulateConnections();
    }

    /// <summary>
    ///     Populates the connection pool with initial connections based on the minimum pool size configuration.
    /// </summary>
    private void PopulateConnections()
    {
        // Loop until the total number of connections reaches the minimum pool size
        while (_totalConnections < _configuration.MinPoolSize)
            // Lock the pool to ensure thread safety while adding connections
            lock (_poolLock)
            {
                // Add a new connection to the read connections queue
                _readConnections.Enqueue(CreateNewConnection());
                // Increment the total number of connections
                _totalConnections++;
            }
    }

    /// <summary>
    ///     Creates a new SQLite connection using the provided configuration.
    /// </summary>
    /// <returns>
    ///     A new SQLite connection that is already opened.
    /// </returns>
    private SqliteConnection CreateNewConnection()
    {
        // Create a new SQLite connection using the connection string from the configuration
        var connection = new SqliteConnection(_configuration.ConnectionString);

        // Open the connection
        connection.Open();

        // Return the opened connection
        return connection;
    }

    /// <summary>
    ///     Retrieves a read-only SQLite connection from the connection pool.
    ///     If no available connections are found, a new connection is created and added to the pool.
    /// </summary>
    /// <returns>
    ///     An open SQLite connection that can be used for read operations.
    /// </returns>
    private SqliteConnection GetReadConnection()
    {
        while (true)
        {
            // Try to dequeue a connection from the read connections queue
            if (_readConnections.TryDequeue(out var connection))
            {
                // If the connection is not open, open it
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                // Return the opened connection
                return connection;
            }

            // Lock the pool to ensure thread safety while adding connections
            lock (_poolLock)
            {
                // If the total number of connections has reached the maximum pool size, continue to the next iteration
                if (_totalConnections >= _configuration.MaxPoolSize) continue;

                // Create a new connection, increment the total number of connections, and return the new connection
                connection = CreateNewConnection();
                _totalConnections++;
                return connection;
            }
        }
    }

    /// <summary>
    ///     Acquires a read-only SQLite connection from the connection pool.
    ///     If no available connections are found, a new connection is created and added to the pool.
    /// </summary>
    /// <returns>
    ///     A <see cref="Try{T}" /> containing an open SQLite connection that can be used for read operations.
    ///     If the operation fails, the <see cref="Try{T}" /> will contain a <see cref="Exception" /> describing the error.
    /// </returns>
    public Try<SqliteConnection> AcquireReadConnection()
    {
        return Try.Succeed(GetReadConnection());
    }

    /// <summary>
    ///     Asynchronously acquires a write-only SQLite connection from the connection pool.
    ///     If no available connections are found, a new connection is created and added to the pool.
    /// </summary>
    /// <returns>
    ///     A <see cref="Task{T}" /> that represents the asynchronous operation.
    ///     The task result is an open SQLite connection that can be used for write operations.
    ///     If the operation fails, the task will throw an <see cref="Exception" /> describing the error.
    /// </returns>
    private async Task<SqliteConnection> GetWriteConnectionAsync()
    {
        // Wait for the write connection lock to be released
        await _writeConnectionLock.WaitAsync();

        // If the write connection is null or closed, create a new connection
        if (_writeConnection is null || _writeConnection.State != ConnectionState.Open)
            _writeConnection = CreateNewConnection();

        // Return the acquired write connection
        return _writeConnection;
    }

    /// <summary>
    ///     Asynchronously acquires a write-only SQLite connection from the connection pool.
    ///     If no available connections are found, a new connection is created and added to the pool.
    /// </summary>
    /// <returns>
    ///     A <see cref="Task{T}" /> that represents the asynchronous operation.
    ///     The task result is a <see cref="Try{T}" /> containing an open SQLite connection that can be used for write
    ///     operations.
    ///     If the operation fails, the task will throw an <see cref="Exception" /> describing the error.
    /// </returns>
    public async Task<Try<SqliteConnection>> AcquireWriteConnection()
    {
        return await Try.Async(GetWriteConnectionAsync());
    }

    /// <summary>
    ///     Releases a read-only SQLite connection back to the connection pool.
    ///     If the connection is not open, it will be opened before being returned to the pool.
    /// </summary>
    /// <param name="connection">
    ///     The SQLite connection to be released.
    ///     This connection must have been previously acquired using the <see cref="AcquireReadConnection" /> method.
    /// </param>
    public void ReleaseReadConnection(SqliteConnection connection)
    {
        if (connection.State != ConnectionState.Open)
            connection.Open();
        _readConnections.Enqueue(connection);
    }

    /// <summary>
    ///     Releases a write-only SQLite connection back to the connection pool.
    ///     This method should be called after completing write operations to ensure proper resource management.
    /// </summary>
    public void ReleaseWriteConnection()
    {
        // Release the semaphore lock, allowing other threads to acquire the write connection
        _writeConnectionLock.Release();
    }
}