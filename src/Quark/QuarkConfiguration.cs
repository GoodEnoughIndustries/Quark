using Quark.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Quark;

public class QuarkConfiguration : IQuarkConfiguration
{
    public QuarkConfiguration(QuarkConfigurationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        this.FileLocations = builder.FileLocations;
        this.QuarkTasks = builder.QuarkTasks;
        this.TargetGroups = builder.TargetGroups;
    }

    public List<DirectoryInfo> FileLocations { get; } = new();
    public List<IQuarkTask> QuarkTasks { get; } = new();
    public List<IQuarkTargetGroup> TargetGroups { get; init; } = new List<IQuarkTargetGroup>();

    public IQuarkConfiguration Build()
    {
        return this;
    }
}
