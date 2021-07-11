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
        public static QuarkConfigurationBuilder WithTargets(
            this QuarkConfigurationBuilder builder,
            string pattern,
            NodeTags tags)
            => throw new NotImplementedException();

        public static QuarkConfigurationBuilder WithElasticsearch(
            this QuarkConfigurationBuilder builder,
            ElasticsearchVersion version)
        {

            return builder;
        }

        public static QuarkConfigurationBuilder WithElasticsearchConfiguration(
            this QuarkConfigurationBuilder builder,
            string remotePath,
            Action<QuarkConfiguration, ElasticsearchConfiguration> esConfig)
        {

            return builder;
        }

        public static IQuarkTargetGroup WithTag(this IQuarkTargetGroup targetGroup, NodeTags tags)
        {
            return targetGroup;
        }
    }
}
