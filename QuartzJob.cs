using Quartz;
using System.Threading.Tasks;

public class ProcessJob : IJob
{
    private readonly RedisService _redis;

    public ProcessJob(RedisService redis)
    {
        _redis = redis;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var req = await _redis.PopRequestAsync();
        if (req != null)
        {
            // ประมวลผล req (เช่น บันทึก DB, ส่งต่อ ฯลฯ)
        }
    }
}