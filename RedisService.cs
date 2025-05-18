using StackExchange.Redis;
using System.Text.Json;
using System.Threading.Tasks;

public class RedisService
{
    private readonly ConnectionMultiplexer _redis;
    private readonly IDatabase _db;

    public RedisService()
    {
        _redis = ConnectionMultiplexer.Connect("localhost");
        _db = _redis.GetDatabase();
    }

    public async Task SaveRequestAsync(MyRequest req)
    {
        string json = JsonSerializer.Serialize(req);
        await _db.KeyExpireAsync("requests", TimeSpan.FromMinutes(5));
        await _db.ListLeftPushAsync("requests", json);
           
    }

    // ดึง request ออกไป process ต่อ (optional)
    public async Task<MyRequest?> PopRequestAsync()
    {
        var value = await _db.ListRightPopAsync("requests");
        if (value.IsNullOrEmpty) return null;
        return JsonSerializer.Deserialize<MyRequest>(value);
    }
}