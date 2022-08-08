using Quark.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quark;

public static class QuarkConfigurationBuilderExtensions
{
    public static QuarkConfigurationBuilder WithTargets(
        this QuarkConfigurationBuilder builder,
        IEnumerable<IQuarkTargetGroup> targets,
        Action<IQuarkTargetBuilder> targetBuilder)
        => WithTargets(builder, targets, targetBuilder, string.Empty);

    public static QuarkConfigurationBuilder WithTargets(
        this QuarkConfigurationBuilder builder,
        IEnumerable<IQuarkTargetGroup> targets,
        Action<IQuarkTargetBuilder> targetBuilder,
        params string[] tags)
    {
        foreach (var tg in targets)
        {
            tg.WithTags(tags);
        }

        builder.AddTargetGroups(targets);

        return builder;
    }

    public static QuarkConfigurationBuilder WithTarget(
        this QuarkConfigurationBuilder builder,
        string target,
        QuarkTargetTypes targetType,
        ExecutingRunnerAsync targetManager,
        params object[] tags)
    {
        ArgumentNullException.ThrowIfNull(builder);

        if (string.IsNullOrWhiteSpace(target))
        {
            throw new ArgumentException("message", nameof(target));
        }

        var tg = new QuarkTargetGroup(target, targetType, targetManager, tags);
        builder.AddTargetGroup(tg);

        return builder;
    }

    public static QuarkConfigurationBuilder WithQuarkFiles(
        this QuarkConfigurationBuilder builder,
        string path)
    {
        ArgumentNullException.ThrowIfNull(builder);

        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("message", nameof(path));
        }

        builder.AddFileLocation(path);

        return builder;
    }
}
