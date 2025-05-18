using Microsoft.AspNetCore.Mvc;
using System.Threading.Channels;

[ApiController]
[Route("[controller]")]
public class WorkController : ControllerBase
{
    private readonly Channel<MyRequest> _channel;

    public WorkController(Channel<MyRequest> channel)
    {
        _channel = channel;
    }

    [HttpPost]
    public async Task<IActionResult> Enqueue([FromBody] MyRequest req)
    {
        await _channel.Writer.WriteAsync(req);
        return Ok(new { status = "queued" });
    }
}