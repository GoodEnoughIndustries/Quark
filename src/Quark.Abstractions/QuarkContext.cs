using Microsoft.Extensions.Logging;
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
        ILoggerFactory loggerFactory,
        IQuarkFileSystem fileSystem,
        IQuarkExecutor executor)
    {
        this.CredentialProvider = credentialProvider;
        this.SecurityProviders = securityProviders;
        this.Configurations = quarkConfigurations;
        this.ExecutionContext = executionContext;
        this.ExecutingTask = Task.CompletedTask;
        this.ProcessProvider = processProvider;
        this.FactProviders = factProviders;
        this.loggerFactory = loggerFactory;
        this.FileSystem = fileSystem;
        this.Executor = executor;
        this.Logger = logger;
    }

    public IEnumerable<IQuarkSecurityProvider> SecurityProviders { get; init; }
    public IEnumerable<IQuarkConfiguration> Configurations { get; init; }
    public IEnumerable<IQuarkFactProvider> FactProviders { get; init; }
    public IQuarkCredentialProvider CredentialProvider { get; init; }
    public IQuarkExecutionContext ExecutionContext { get; init; }
    public IQuarkProcessProvider ProcessProvider { get; init; }
    public ILogger<QuarkContext> Logger { get; init; }
    public IQuarkFileSystem FileSystem { get; init; }
    public IQuarkExecutor Executor { get; init; }
    public Task ExecutingTask { get; set; }

    public ILogger<TCategory> GetLogger<TCategory>()
        => this.loggerFactory.CreateLogger<TCategory>();
}
