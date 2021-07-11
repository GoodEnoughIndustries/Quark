﻿using Quark;
using Quark.Abstractions;
using Quark.Elasticsearch;
using Quark.System;
using System.Collections.Generic;
using System.Threading;

List<IQuarkTargetGroup> elasticSearchNodes = new()
{
    new QuarkTargetGroup("elasticnodes[00:03]", QuarkTargetTypes.Linux, tags: NodeTags.Primary),
    new QuarkTargetGroup("elasticnodes[04:30]", QuarkTargetTypes.Linux, tags: NodeTags.Data | NodeTags.Ingest),
};

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

var config = new QuarkConfigurationBuilder()
    .WithQuarkFiles(@"C:\QuarkFiles\Elasticsearch\")
    .WithTargets(elasticSearchNodes)
    .WithElasticsearch(ElasticsearchVersion.v7_13_3)
    .WithElasticsearchConfiguration(
        remotePath: "/etc/elasticsearch/elasticsearch.yml",
        (quark, config) =>
        {
            config = esConfig;
            config.Discovery.Zen.Ping.UnicastHosts = quark.Targets.WithTag(NodeTags.Primary);
            config.GetNodeName(node => node.ToString());
        })
    .WithVariable("memory", "16g")
    .WithJinja2Template("jvm.options.j2", "/etc/elasticsearch/jvm.options")
    .WithJinja2Template("elasticsearch.service.j2", "/usr/lib/systemd/system/elasticsearch.service")
    .ConfigureService("elasticsearch.service", SystemdServiceOptions.DaemonReload | SystemdServiceOptions.Enabled);

var result = config
    .ConfigureTelegraf(elasticSearchNodes)
    .ConfigureSnmp(elasticSearchNodes)
    .Build()
    .Run(CancellationToken.None);

PackageDescription winDirStatPackage = new(
    file: "windirstat1_1_2_setup.exe",
    installedLocation: @"C:\Program Files (x86)\WinDirStat\windirstat.exe");

var configurationBuilder = new QuarkConfigurationBuilder()
    .WithQuarkFiles(path: @"C:\QuarkFiles")
    .WithTarget(target: "localhost")
    .ManagePackage(
        file: "windirstat1_1_2_setup.exe",
        installedLocation: @"C:\Program Files (x86)\WinDirStat\windirstat.exe",
        shouldExist: true)
    .ManagePackage(winDirStatPackage, shouldExist: false);

IQuarkConfiguration desiredConfiguration = configurationBuilder.Build();

//QuarkResult result = await QuarkExecutor.RunAsync(desiredConfiguration, CancellationToken.None);

return 0;// await desiredConfiguration.RunAsync();
