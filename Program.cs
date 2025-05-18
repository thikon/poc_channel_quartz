using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        Description = "My API Description"
    });
});

builder.Services.AddHostedService<ChannelWorker>();
builder.Services.AddSingleton(ChannelFactory.Create<MyRequest>()); // Channel แบบ Singleton
builder.Services.AddSingleton<RedisService>();
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    QuartzSetup.AddJobs(q);
});
builder.Services.AddQuartzHostedService();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => 
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    // ตั้งค่า route prefix เป็นค่าว่างเพื่อให้สามารถเข้าถึง swagger ที่ root path ได้
    c.RoutePrefix = "swagger";
});


app.MapControllers();
app.Run();