using Shared;
using System.Collections.Concurrent;
using System.Threading.Channels;

namespace Backend.LiveTelemetry
{
    public sealed class StreamChannelManager
    {
        private static readonly ConcurrentDictionary<string, Channel<AirplaneTelemetry>> Channels = new();

        public bool AddChannel(string connectionId, Channel<AirplaneTelemetry> channel)
        {
            return Channels.TryAdd(connectionId, channel);
        }

        public bool RemoveChannel(string connectionId)
        {
            var result = Channels.TryRemove(connectionId, out var channel);

            if (result)
            {
                channel?.Writer.Complete();
            }

            return result;
        }

        public bool TryGetChannel(string connectionId, out Channel<AirplaneTelemetry>? channel)
        {
            return Channels.TryGetValue(connectionId, out channel);
        }

        public IReadOnlyCollection<Channel<AirplaneTelemetry>> GetChannels()
        {
            return Channels.Values.ToList().AsReadOnly();
        }

        public void StreamTelemetry(AirplaneTelemetry telemetry)
        {
            Parallel.ForEach(Channels.Values, channel =>
            {
                channel.Writer.TryWrite(telemetry);
            });
        }
    }
}
