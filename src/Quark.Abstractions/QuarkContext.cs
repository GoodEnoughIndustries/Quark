using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quark.Abstractions;

public class QuarkContext
{
    private readonly ILoggerFactory loggerFactory;

    public QuarkContext(
        IEnumerable<IQuarkConfiguration> quarkConfigurations,
        IEnumerable<IQuarkSecurityProvider> securityProviders,
        IEnumerable<IQuarkFactProvider> factProviders,
        IQuarkCredentialProvider credentialProvider,
        IQuarkProcessProvider processProvider,
        IQuarkExecutionContext executionContext,
        ILogger<QuarkContext> logger,
        IConfiguration configuration,
        ILoggerFactory loggerFactory,
        IQuarkFileSystem fileSystem,
        IServiceProvider serviceProvider)
    {
        this.CredentialProvider = credentialProvider;
        this.SecurityProviders = securityProviders;
        this.QuarkConfigurations = quarkConfigurations;
        this.ExecutionContext = executionContext;
        this.ExecutingTask = Task.CompletedTask;
        this.ProcessProvider = processProvider;
        this.ServiceProvider = serviceProvider;
        this.Configuration = configuration;
        this.FactProviders = factProviders;
        this.loggerFactory = loggerFactory;
        this.FileSystem = fileSystem;
        this.Logger = logger;
    }

    public IEnumerable<IQuarkSecurityProvider> SecurityProviders { get; init; }
    public IEnumerable<IQuarkConfiguration> QuarkConfigurations { get; init; }
    public IEnumerable<IQuarkFactProvider> FactProviders { get; init; }
    public IQuarkCredentialProvider CredentialProvider { get; init; }
    public IQuarkExecutionContext ExecutionContext { get; init; }
    public List<IQuarkTask> ManagementTasks { get; set; } = new();
    public IQuarkProcessProvider ProcessProvider { get; init; }
    public IServiceProvider ServiceProvider { get; init; }
    public IConfiguration Configuration { get; init; }
    public ILogger<QuarkContext> Logger { get; init; }
    public IQuarkFileSystem FileSystem { get; init; }
    public Task ExecutingTask { get; set; }

    public ILogger<TCategory> GetLogger<TCategory>()
        => this.loggerFactory.CreateLogger<TCategory>();
}
