using Microsoft.Extensions.Hosting;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Threading;

public class ChannelWorker : BackgroundService
{
    private readonly Channel<MyRequest> _channel;
    private readonly RedisService _redis;

    public ChannelWorker(Channel<MyRequest> channel, RedisService redis)
    {
        _channel = channel;
        _redis = redis;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _channel.Reader.WaitToReadAsync(stoppingToken))
        {
            while (_channel.Reader.TryRead(out var req))
            {
                await _redis.SaveRequestAsync(req);
                // หรือจะ push ไปทำงานต่อ เช่น Quartz job
            }
        }
    }
}