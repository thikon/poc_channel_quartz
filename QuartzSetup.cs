using Microsoft.Extensions.DependencyInjection;
using Quartz;

public static class QuartzSetup
{
    public static void AddJobs(IServiceCollectionQuartzConfigurator q)
    {
        var jobKey = new JobKey("ProcessJob");
        q.AddJob<ProcessJob>(opts => opts.WithIdentity(jobKey));
        q.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity("ProcessJob-trigger")
            .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(1)
                .RepeatForever()));
    }
}