using Quark.Abstractions;
using System;
using System.Collections.Generic;

namespace Quark.Elasticsearch;

public class ElasticsearchConfiguration
{
    public string ClusterName { get; set; } = string.Empty;
    public IndexConfiguration Index { get; set; } = new();
    public bool BootstrapMemoryLock { get; set; } = false;

    public DiscoveryConfiguration Discovery { get; set; } = new();

    public PathConfiguration Path { get; set; } = new();

    public ElasticsearchConfiguration SetClusterName(string name)
    {
        return this;
    }

    public ElasticsearchConfiguration GetNodeName(Func<string, string> nodeName)
    {
        return this;
    }

    public record PathConfiguration()
    {
        public string Data;
        public string Logs;
    }

    public class DiscoveryConfiguration
    {
        public ZenConfiguration Zen { get; set; } = new();
        public class ZenConfiguration
        {
            public int MinimumPrimaryNodes { get; set; } = 2;

            public PingConfiguration Ping { get; set; } = new();

            public class PingConfiguration
            {
                public IEnumerable<IQuarkTargetGroup> UnicastHosts { get; set; } = new List<IQuarkTargetGroup>();
            }
        }
    }
}

public class IndexConfiguration
{
    public int NumberOfShards { get; set; } = 1;
    public int NumberOfReplicas { get; set; } = 1;
}
