using Microsoft.Extensions.Hosting;
using Quark;
using Quark.Abstractions;
using Quark.Elasticsearch;
using Quark.Systems;
using System;
using System.Collections.Generic;
using System.Threading;

PackageDescription winDirStatPackage = new(
    file: "windirstat1_1_2_setup.exe",
    installedLocation: @"C:\Program Files (x86)\WinDirStat\windirstat.exe",
    version: "1.1.2.80");

var esConfig = new ElasticsearchConfiguration
{
    ClusterName = "quark-cluster",
    BootstrapMemoryLock = true,
    Index =
    {
        NumberOfReplicas = 1,
        NumberOfShards = 1,
    },
    Path =
    {
        Data = "/elasticsearch/data",
        Logs = "/elasticsearch/logs",
    },
    Discovery =
    {
        Zen =
        {
            MinimumPrimaryNodes = 2,
            Ping = new(),
        },
    }
};

Action<IQuarkTargetBuilder> esActions = builder =>
{
    builder.ManagePackage(winDirStatPackage, shouldExist: true)
    .WithElasticsearch(ElasticsearchVersion.v7_13_3)
    .WithElasticsearchConfiguration(
        remotePath: "/etc/elasticsearch/elasticsearch.yml",
        (quark, config) =>
        {
            config = esConfig;
            config.Discovery.Zen.Ping.UnicastHosts = builder.TargetGroups.WithTag(NodeTags.Primary);
            config.GetNodeName(node => node.ToString());
        })
    .WithVariable("memory", "16g")
    .WithJinja2Template("jvm.options.j2", "/etc/elasticsearch/jvm.options")
    .WithJinja2Template("elasticsearch.service.j2", "/usr/lib/systemd/system/elasticsearch.service")
    .ConfigureService("elasticsearch.service", SystemdServiceOptions.DaemonReload | SystemdServiceOptions.Enabled);
};

List<IQuarkTargetGroup> elasticSearchNodes = new()
{
    new QuarkTargetGroup("elasticnodes[00:03]", QuarkTargetTypes.Linux, esActions, tags: NodeTags.Primary),
    new QuarkTargetGroup("elasticnodes[04:30]", QuarkTargetTypes.Linux, esActions, tags: NodeTags.Data | NodeTags.Ingest),
};

var configurationBuilder = new QuarkConfigurationBuilder()
    .Configure((b, c) =>
    {
        b.ConfigureServices(services =>
        {
        });
    })
    .WithQuarkFiles(path: @"C:\QuarkFiles")
    .ManagePackage(winDirStatPackage, shouldExist: false)
    .WithTarget(target: "localhost", QuarkTargetTypes.Windows, builder =>
    {
        builder.ManagePackage(winDirStatPackage, shouldExist: true);
    },
    tags: "blah");

IQuarkConfiguration desiredConfiguration = configurationBuilder.Build();

QuarkResult result = await QuarkExecutor.RunAsync(desiredConfiguration, CancellationTokenSource.t);

return 0;
