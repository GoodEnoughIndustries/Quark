using Quark.Abstractions;
using System;

namespace Quark.Elasticsearch
{
    public enum ElasticsearchVersion
    {
        v6_8,
        v7_13_3
    }

    [Flags]
    public enum NodeTags
    {
        Unknown = 1 << 0,
        Primary = 1 << 1,
        Ingest = 1 << 2,
        Data = 1 << 3,
        Coordinators = 1 << 4,
    }

    public static class QuarkElasticsearchExtensions
    {
        public static IQuarkTargetBuilder WithElasticsearch(
            this IQuarkTargetBuilder builder,
            ElasticsearchVersion version)
        {

            return builder;
        }

        public static IQuarkTargetBuilder WithElasticsearchConfiguration(
            this IQuarkTargetBuilder builder,
            string remotePath,
            Action<QuarkConfiguration, ElasticsearchConfiguration> esConfig)
        {

            return builder;
        }
    }
}
