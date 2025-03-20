using System.Collections.Concurrent;

namespace Backend.TelemetryServer;

public sealed class TelemetryServerUserManager
{
    private static readonly ConcurrentDictionary<string, HashSet<string>> ConnectedUsers = new();

    public IReadOnlyCollection<string> GetConnectedUsers()
    {
        return ConnectedUsers.Keys.ToList().AsReadOnly();
    }

    public bool IsUserConnected(string userId)
    {
        return ConnectedUsers.ContainsKey(userId);
    }

    public void AddUser(string userId, string connectionId)
    {
        if (!ConnectedUsers.TryGetValue(userId, out HashSet<string>? value))
        {
            value = [];
            ConnectedUsers[userId] = value;
        }

        value.Add(connectionId);
    }

    public void RemoveUser(string userId, string connectionId)
    {
        if (ConnectedUsers.TryGetValue(userId, out HashSet<string>? value))
        {
            value.Remove(connectionId);
            if (ConnectedUsers[userId].Count == 0)
            {
                ConnectedUsers.TryRemove(userId, out _);
            }
        }
    }

    public HashSet<string> GetUserConnections(string userId)
    {
        return ConnectedUsers.TryGetValue(userId, out HashSet<string>? value) ? value : new HashSet<string>();
    }
}