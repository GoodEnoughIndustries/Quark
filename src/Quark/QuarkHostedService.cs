using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quark.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Quark;

public class QuarkHostedService : BackgroundService, IQuarkExecutorHostedService
{
    private readonly ILogger<QuarkHostedService> logger;
    private readonly IHostApplicationLifetime appLifetime;
    private PeriodicTimer executionTimer = default!;

    public QuarkHostedService(
        QuarkContext context,
        ILogger<QuarkHostedService> logger,
        IHostApplicationLifetime applicationLifetime)
    {
        this.Context = context;
        this.logger = logger;
        this.appLifetime = applicationLifetime;

        this.executionTimer = new PeriodicTimer(TimeSpan.FromSeconds(1.0));
    }

    public QuarkContext Context { get; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var results = new List<IQuarkResult>();

        foreach (var configuration in this.Context.Configurations)
        {
            IQuarkExecutionContext executionContext = new QuarkExecutionContext(configuration);
            await executionContext.BuildAllAsync(this.Context, stoppingToken);

            await executionContext.ExecuteTasksAsync(this.Context, stoppingToken).ConfigureAwait(false);

            results.AddRange(executionContext.Results);
        }

        this.appLifetime.StopApplication();
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        this.logger.LogInformation("done");
        await base.StopAsync(cancellationToken);
    }
}
