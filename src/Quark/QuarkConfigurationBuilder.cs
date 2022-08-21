using Microsoft.Extensions.Logging;
using Quark.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Quark;

public class QuarkConfigurationBuilder : QuarkTargetRunner
{
    public QuarkConfigurationBuilder()
        :base()
    {
    }

    public QuarkConfigurationBuilder(ILogger<QuarkConfigurationBuilder> logger, QuarkContext context, IQuarkTarget target)
        : base(logger, context, target)
    {
    }

    public List<DirectoryInfo> FileLocations { get; } = new();

    public IQuarkConfiguration Build()
    {
        var config = new QuarkConfiguration(this);
        config.Build();

        return config;
    }
    public QuarkConfigurationBuilder AddTargetGroups(IEnumerable<IQuarkTargetGroup> targetGroups)
    {
        this.TargetGroups.AddRange(targetGroups);

        return this;
    }

    public QuarkConfigurationBuilder AddTargetGroup(IQuarkTargetGroup targetGroup)
    {
        this.TargetGroups.Add(targetGroup);

        return this;
    }

    public QuarkConfigurationBuilder AddFileLocation(string folderPath)
    {
        if (string.IsNullOrWhiteSpace(folderPath))
        {
            throw new ArgumentNullException(nameof(folderPath));
        }

        this.FileLocations.Add(new DirectoryInfo(folderPath));

        return this;
    }
}
