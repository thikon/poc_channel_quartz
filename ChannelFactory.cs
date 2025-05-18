using System.Threading.Channels;

public static class ChannelFactory
{
    public static Channel<T> Create<T>(int capacity = 10000)
    {
        return Channel.CreateBounded<T>(new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait,
            SingleReader = false,
            SingleWriter = false
        });
    }
}