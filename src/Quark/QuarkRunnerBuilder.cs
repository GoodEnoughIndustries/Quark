using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quark.Abstractions;
using System;
using System.Collections.Generic;

namespace Quark;

public class QuarkRunnerBuilder : IHostBuilder
{
    private readonly IHostBuilder hostBuilder;

    public QuarkRunnerBuilder(string[] args)
    {
        this.hostBuilder = new HostBuilder();

        this
            .ConfigureDefaults(args)
            .ConfigureServices((context, sc) =>
            {
                sc.AddSingleton<IQuarkCredentialProvider, QuarkCredentialProvider>();
                sc.AddSingleton<IQuarkExecutionContext, QuarkExecutionContext>();
                sc.AddSingleton<IQuarkExecutor, QuarkExecutor>();
                sc.AddHostedService<QuarkHostedService>();
                sc.AddSingleton<QuarkContext>();
            });
    }

    public IDictionary<object, object> Properties => this.hostBuilder.Properties;

    public List<IQuarkConfiguration> configurations { get; } = new();

    public IHost Build()
    {
        foreach (var configuration in this.configurations)
        {
            foreach (var targetGroup in configuration.TargetGroups)
            {
                targetGroup.BuildTargets(this);
            }
        }

        var host = this.hostBuilder.Build();

        // Force and exception here in case something is misconfigured
        // or missing in the DI container...
        host.Services.GetRequiredService<QuarkContext>();

        return host;
    }

    public IHostBuilder ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configureDelegate)
    {
        this.hostBuilder.ConfigureAppConfiguration(configureDelegate);
        return this;
    }

    public IHostBuilder ConfigureContainer<TContainerBuilder>(Action<HostBuilderContext, TContainerBuilder> configureDelegate)
    {
        this.hostBuilder.ConfigureContainer(configureDelegate);
        return this;
    }

    public IHostBuilder ConfigureHostConfiguration(Action<IConfigurationBuilder> configureDelegate)
    {
        this.hostBuilder.ConfigureHostConfiguration(configureDelegate);
        return this;
    }

    public IHostBuilder AddQuarkConfiguration(IQuarkConfiguration configuration)
    {
        this.hostBuilder.ConfigureServices(sc => sc.AddSingleton(configuration));
        this.configurations.Add(configuration);

        return this;
    }

    public IHostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
    {
        this.hostBuilder.ConfigureServices(configureDelegate);
        return this;
    }

    public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory)
    {
        this.hostBuilder.UseServiceProviderFactory(factory);
        return this;
    }

    public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(Func<HostBuilderContext, IServiceProviderFactory<TContainerBuilder>> factory)
    {
        ArgumentNullException.ThrowIfNull(factory);

        this.hostBuilder.UseServiceProviderFactory(factory);
        return this;
    }
}
